using LMS.Models;
using LMS.Repositories;
using LMS.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace LMS.Pages
{
    [Authorize(Roles = "Student")]
    public class EnrollmentPageModel : PageModel
    {
        private readonly CourseRepository _courseRepository;
        private readonly UserRepository _userRepository;
        [BindProperty]
        public EnrollmentPageVM _enrollmentPage {  get; set; }

        [BindProperty(SupportsGet = true)]
        public int FacultyId { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? SearchWord { get; set; }
        private int UserId => int.Parse(HttpContext.User.FindFirst(ClaimTypes.Sid)?.Value);
        public EnrollmentPageModel(CourseRepository courseRepository, UserRepository userRepository)
        {
            _courseRepository = courseRepository;
            _userRepository = userRepository;
            _enrollmentPage = new EnrollmentPageVM();           
        }

        public async Task OnGet()
        {          
            _enrollmentPage.Faculties = await _courseRepository.getAllFaculties();
            _enrollmentPage.Enrollments = await _userRepository.GetEnrollmentsByStudentId(UserId);
        }

        public async Task OnPostSearch()
        {
            _enrollmentPage.Courses = await _courseRepository.GetAvailableCoursesByFacultyId(FacultyId, UserId);
            if(!string.IsNullOrWhiteSpace(SearchWord))
            {
                _enrollmentPage.Courses = _enrollmentPage.Courses
                    .Where(c => c.CourseName.Contains(SearchWord, StringComparison.OrdinalIgnoreCase) || 
                                c.Description.Contains(SearchWord, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
            await OnGet();
        }

        public async Task<IActionResult> OnPostSelectCourse(int courseId)
        {
            var course = await _courseRepository.GetById(courseId);
            if(course != null)
            {
                _enrollmentPage.Courses = await _courseRepository.GetAvailableCoursesByFacultyId(course.FacultyId, UserId);
                _enrollmentPage.Courses.Remove(course);
            }     

            await _userRepository.AddEnrollment(UserId, courseId);
            _enrollmentPage.Enrollments = await _userRepository.GetEnrollmentsByStudentId(UserId);

            return Partial("_EnrollmentPagePartial", _enrollmentPage);
        }

        public async Task<IActionResult> OnPostDelete(int courseId)
        {
            var course = await _courseRepository.GetById(courseId);
            await _userRepository.DeleteEnrollment(UserId, courseId);
            _enrollmentPage.Courses = await _courseRepository.GetAvailableCoursesByFacultyId(course.FacultyId, UserId);
            _enrollmentPage.Enrollments = await _userRepository.GetEnrollmentsByStudentId(UserId);

            return Partial("_EnrollmentPagePartial", _enrollmentPage);
        }
    }
}
