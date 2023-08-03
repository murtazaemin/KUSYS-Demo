using KUSYS_Demo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace KUSYS_Demo.Controllers
{
    [Authorize]
    public class StudentsCourseController : Controller
    {
        Context _context;
        public StudentsCourseController(Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetStudentsCourseList()
        {
            // datatable verileri
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();

            int pagesize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;

            List<StudentsCourse> data = new List<StudentsCourse>();

            // Kullanıcının rolü
            var Role = User.FindFirstValue(ClaimTypes.Role);


            var filtredCount = 0;

            // Toplam öğrenci kurs eşleşme sayısı
            var totalCount = _context.StudentsCourses.Count();

            // Kullanıcı Admin ise tüm öğrenciler kurs eşleşmeleri değilse sadece kendi eşleşmeleri 
            if (Role == "Admin")
            {
                data = _context.StudentsCourses.Include(x => x.Student).Include(x => x.Course).OrderByDescending(x => x.CreateDate).Skip(skip).Take(pagesize).ToList();
                filtredCount = totalCount;
            }
            else 
            {
                // User kullanıcının StudentId sinin bulunması
                Guid StudentId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));

                filtredCount = _context.StudentsCourses.Include(x => x.Student).Where(x => x.StudentId == StudentId).Count();

                data = _context.StudentsCourses.Include(x => x.Student).Include(x => x.Course).Where(x => x.StudentId == StudentId).OrderByDescending(x => x.CreateDate).Skip(skip).Take(pagesize).ToList();
            
            }


            JsonResult result = Json(new
            {
                draw = draw,
                recordsFiltered = filtredCount,
                recordsTotal = totalCount,
                data = data.Select(x => new {
                    StudentFullName = x.Student.FirstName+ " " + x.Student?.LastName ,
                    CourseName = x.Course?.CourseName,
                    CourseCode = x.Course?.CourseCode,
                    CreateDate = x.CreateDate
                })
            });

            return Json(result.Value);
        }

        [HttpPost]
        public JsonResult AddStudentsCourse(StudentsCourse studentCourse)
        {
            // öğrencinin daha önce seçilen kurs için kaydı var mı
            var currentStudentsCourse = _context.StudentsCourses.FirstOrDefault(x => x.StudentId == studentCourse.StudentId && x.CourseId == studentCourse.CourseId);

            if (currentStudentsCourse != null)
            {
                // başarısız öğrenci kursu daha önce seçmiş
                return Json("2");
            }
            else
            {
                _context.StudentsCourses.Add(studentCourse);
            }

            var Result = _context.SaveChanges();

            if (Result > 0)
                // başarılı kayıt
                return Json("1");
            else
                // başarısız kayıt
                return Json("0");
        }
    }
}
