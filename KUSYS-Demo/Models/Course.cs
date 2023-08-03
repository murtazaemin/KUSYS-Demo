using System.ComponentModel.DataAnnotations;

namespace KUSYS_Demo.Models
{
    public class Course:BaseEntity
    {
        [Key]
        public Guid CourseId { get; set; }
        [Required, StringLength(10)]
        public string CourseCode { get; set; } = string.Empty;
        [Required, StringLength(50)]
        public string CourseName { get; set; } = string.Empty;

        public virtual ICollection<StudentsCourse> StudentsCourses { get; set; }

    }
}
