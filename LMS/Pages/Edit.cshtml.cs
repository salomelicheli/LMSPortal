using LMS.Models;
using LMS.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace LMS.Pages
{
    public class EditModel : PageModel
    {
        private readonly CourseRepository _courseRepository;
        [BindProperty]
        public Course course { get; set; }
        public EditModel(CourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }
        public async Task OnGet(int id)
        {
            course = await _courseRepository.GetById(id); 
        }

        public async Task<IActionResult> OnPost()
        {
            try
            {
                await _courseRepository.Update(course);

                return RedirectToPage("/AdminPage");
            }
            catch(Exception ex) 
            {
                return RedirectToPage("/Error");
            }
        }
    }
}
