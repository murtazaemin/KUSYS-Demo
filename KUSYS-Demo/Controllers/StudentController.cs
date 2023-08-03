using KUSYS_Demo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Specialized;
using System.Security.Claims;

namespace KUSYS_Demo.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StudentController : Controller
    {
        Context _context;
        public StudentController(Context context)
        {
            _context = context;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetStudentList() 
        {
            // datatable verileri
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();

            int pagesize = length != null ? int.Parse(length) : 0;
            int skip = start != null ? int.Parse(start) : 0;

            // toplam öğrenci sayısı
            int totalCount = _context.Students.Count();

            //pagesize a göre filtrenmiş öğreci listesi
            List<Student> data = _context.Students.OrderByDescending(x=>x.CreateDate).Skip(skip).Take(pagesize).ToList();


            JsonResult result = Json(new 
            { 
                draw = draw,
                recordsFiltered = totalCount,
                recordsTotal = totalCount,
                data = data.Select(x => new { 
                    StudentId = x.StudentId,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    BirthDay = x.BirthDate,
                    CreateDate = x.CreateDate
                })
            });

            return Json(result.Value);
        }

        [HttpGet]
        public JsonResult GetStudentById(Guid id)
        {
            // Seçilen id li öğrenci
            Student student = _context.Students.FirstOrDefault(x => x.StudentId == id);

            if (student != null)
            {
                return Json(new
                {
                    student.FirstName,
                    student.LastName,
                    student.BirthDate
                });
            }
            else
            {
                return Json("0");
            }
            
        }


        [HttpPost]
        public JsonResult AddStudent(Student student)
        {
            _context.Students.Add(student);

            // Eklenen Student için bir User oluşturuldu.

            User user = new User()
            {
                UserName = student.FirstName,
                Password = "123",
                StudentId = student.StudentId,
                Role = "User"
            };
            _context.Users.Add(user);

            var Result = _context.SaveChanges();

            if (Result > 0)
                // başarılı kayıt
                return Json("1");
            else
                // başarısız kayıt
                return Json("0");
        }


        [HttpPost]
        public JsonResult DeleteStudent([FromForm]Guid id)
        {
            // Silinmek istenen öğrencinin bulunması
            Student student = _context.Students.Find(id);

            if (student != null)
            {
                _context.Students.Remove(student);
            }
            

            var Result = _context.SaveChanges();
            if (Result > 0)
                // başarılı kayıt
                return Json("1");
            else
                // başarısız kayıt
                return Json("0");
        }

        [HttpPost]
        public JsonResult EditStudent(Student student)
        {
            // Düzenlenmek istenen öğrencinin bulunması
            Student editStudent = _context.Students.FirstOrDefault(x => x.StudentId == student.StudentId);

            editStudent.FirstName = student.FirstName;
            editStudent.LastName = student.LastName;
            editStudent.BirthDate = student.BirthDate;


            var Result = _context.SaveChanges();
            if (Result > 0)
                // başarılı kayıt
                return Json("1");
            else
                // başarısız kayıt
                return Json("0");
        }


        [HttpGet]
        [AllowAnonymous]
        public JsonResult GetStudentListForDropdown()
        {
            // Kullanıcının rolü
            var Role = User.FindFirstValue(ClaimTypes.Role);

            List<Student> data = new List<Student>();

            // Kullanıcı Admin ise tüm öğrenciler değilse sadece kendi kaydı 
            if (Role == "Admin")
            {
                data = _context.Students.OrderByDescending(x => x.CreateDate).ToList();
            }
            else
            {
                // User kullanıcının StudentId sinin bulunması
                Guid StudentId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
                data = _context.Students.Where(x => x.StudentId == StudentId).OrderByDescending(x => x.CreateDate).ToList();  
            }

            JsonResult result = Json(new
            {
                data = data.Select(x => new {
                   StudentId=x.StudentId,
                   StudentName = x.FirstName +" "+ x.LastName
                })
            });
            return Json(result.Value);
        }

    }
}
