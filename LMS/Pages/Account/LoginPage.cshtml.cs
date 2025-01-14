using LMS.Models;
using LMS.Repositories;
using LMS.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace LMS.Pages.Account
{
    public class LoginPageModel : PageModel
    {
        private readonly UserRepository _userService;
        [BindProperty]
        public LoginPageVM LoginVM { get; set; }
        public string ErrorMessage { get; set; }

        public LoginPageModel(UserRepository userService)
        {
            _userService = userService;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            var user = await _userService.GetUserByCredentials(LoginVM.userID, LoginVM.password);

            if(user != null)
            {
                //HttpContext.Session.SetInt32("userId", user.Id);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.FirstName),
                    new Claim(ClaimTypes.Role, user.UserRole.ToString()),
                    new Claim(ClaimTypes.Sid, user.Id.ToString())
                };

                ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToPage("/Index");
            }       
            else
            {
                ErrorMessage = "მომხმარებელი ან პაროლი არასწორია!";
                return Page();
            }
        }
    }
}
