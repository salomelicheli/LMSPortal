using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Models
{
    public class Course
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string CourseName { get; set; }  
        [MaxLength(500)]
        public string Description { get; set; }
        public ICollection<Enrollment>? Students { get; set; }
        [ForeignKey("Faculty")]
        public int FacultyId { get; set; }
        public int Credit {  get; set; }
        public Faculty? Faculty { get; set; }
    }
}
