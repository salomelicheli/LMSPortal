using LMS.Models;
using LMS.Repositories;
using LMS.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LMS.Pages
{
    [Authorize(Roles = "Admin")]
    public class AdminPageModel : PageModel
    {
        private readonly CourseRepository _courseService;

        [BindProperty]
        public AdminPageVM adminVM { get; set; }

        public AdminPageModel(CourseRepository courseService)
        {
            _courseService = courseService;
            adminVM = new AdminPageVM();
        }

        public async Task<IActionResult> OnGet(int? facultyId)
        {
            if(facultyId != null)
            {
                adminVM.Courses = await _courseService.GetCoursesByFacultyId(facultyId);
            }
            else
            {
                adminVM.Courses = await _courseService.GetAll();
            }
            adminVM.Faculties = await _courseService.getAllFaculties();

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            Course course = new Course
            {
                CourseName = adminVM.Name,
                Description = adminVM.Description,
                FacultyId = adminVM.SelectedFacultyId
            };
            await _courseService.Insert(course);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDelete(int courseId)
        {
            Console.WriteLine(courseId);
            var courseToDelete = await _courseService.GetById(courseId);

            if (courseToDelete != null)
            {
                await _courseService.Delete(courseToDelete);
            }

            return RedirectToPage();
        }
    }
}
