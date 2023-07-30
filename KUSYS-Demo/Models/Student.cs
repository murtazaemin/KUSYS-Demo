using System.ComponentModel.DataAnnotations;

namespace KUSYS_Demo.Models
{
    public class Student: BaseEntity
    {
        [Key]
        public Guid StudentId { get; set; }
        [Required, StringLength(50)]
        public string FirstName { get; set; } = string.Empty;
        [Required, StringLength(50)]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public DateTime BirthDate { get; set; }
    }
}
