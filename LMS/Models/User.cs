using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Models
{
    public enum Role
    {
        Admin,
        Student
    }
    public class User
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string PersonalID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string? Email { get; set; }
        public string Password { get; set; }
        public Role UserRole { get; set; }
        public ICollection<Enrollment> Courses { get; set; }
        [ForeignKey("Faculty")]
        public int? FacultyId { get; set; }
        public Faculty? Faculty { get; set; }
    }
}
