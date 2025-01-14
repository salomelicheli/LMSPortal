using LMS.Models;
using System.Security.Claims;

namespace LMS.ViewModels
{
    public class EnrollmentPageVM
    {
        public List<Faculty> Faculties { get; set; } = new List<Faculty>();
        public List<Course>? Courses { get; set; } = new List<Course>();
        public List<Course>? Enrollments { get; set; } = new List<Course>();
    }
}
