using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KUSYS_Demo.Models
{
    public class StudentsCourse: BaseEntity
    {
        [Key, Column(Order = 1)]
        public Guid StudentId { get; set; }
        [Key, Column(Order = 2)]
        public Guid CourseId { get; set; }
        public virtual Student? Student { get; set; }
        public virtual Course? Course { get; set; }
    }
}
