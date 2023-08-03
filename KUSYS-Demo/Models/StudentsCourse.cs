using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KUSYS_Demo.Models
{
    public class StudentsCourse: BaseEntity
    {
        public Guid StudentId { get; set; }
        [ForeignKey("StudentId")]
        public virtual Student Student { get; set; }
        public Guid CourseId { get; set; }      
        [ForeignKey("CourseId")]
        public virtual Course Course { get; set; }
    }
}
