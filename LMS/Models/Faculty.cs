using System.ComponentModel.DataAnnotations;

namespace LMS.Models
{
    public class Faculty
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
}
