using LMS.Models;
using LMS.Repositories;
using LMS.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace LMS.Pages.Account
{
    public class StudentProfilePageModel : PageModel
    {
        private readonly UserRepository _userRepository;
        [BindProperty]
        public ProfilePageVM _profilePageVM { get; set; }
        [BindProperty]
        public string? _searchId { get; set; }

        public StudentProfilePageModel(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task OnGet()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.Sid)?.Value;
            if (userId != null)
            {
                User user = await _userRepository.GetUserById(int.Parse(userId));
                var faculty = await _userRepository.GetFacultyById(user.FacultyId);
                _profilePageVM = new ProfilePageVM
                {
                    PersonalId = user.PersonalID,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    FacultyName = faculty?.Name
                };
            }      
        }
    }
}
