using LMS.Models;

namespace LMS.ViewModels
{
    public class ProfilePageVM
    {
        public string PersonalId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? FacultyName { get; set; }
    }
}
