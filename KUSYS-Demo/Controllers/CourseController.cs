using KUSYS_Demo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KUSYS_Demo.Controllers
{
    [Authorize]
    public class CourseController : Controller
    {
        Context _context;
        public CourseController(Context context)
        {
            _context = context;
        }

        // selectbox için tüm kursların çeken method
        [HttpGet]
        [AllowAnonymous]
        public JsonResult GetCourseListForDropdown()
        {
            List<Course> data = _context.Courses.OrderByDescending(x => x.CreateDate).ToList();

            JsonResult result = Json(new
            {
                data = data.Select(x => new {
                    CourseId = x.CourseId,
                    CourseName = x.CourseName
                })
            });
            return Json(result.Value);
        }
    }
}
