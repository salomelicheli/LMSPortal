using LMS.Repositories;
using LMS.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LMS.Pages.Account
{
    public class ProfileDetailsPageModel : PageModel
    {
        private readonly UserRepository _userRepository;
        [BindProperty(SupportsGet = true)]
        public AdminProfileVM _adminProfile { get; set; }
        public ProfileDetailsPageModel(UserRepository userRepository)
        {
            _userRepository = userRepository;
            _adminProfile = new AdminProfileVM();
        }

        public async Task OnGet()
        {
            _adminProfile.User = await _userRepository.GetStudentsByPersonalId(_adminProfile.SearchId);
        }
    }
}
