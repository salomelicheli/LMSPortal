using LMS.Data;
using LMS.Models;
using LMS.Services;
using Microsoft.EntityFrameworkCore;

namespace LMS.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly LMSContext _context;

        public CourseRepository(LMSContext context)
        {
            _context = context;
        }

        public async Task Delete(Course course)
        {
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();  
        }

        public async Task<List<Course>?> GetAll()
        {
            return await _context.Courses.ToListAsync();
        }

        public async Task<Course?> GetById(int id)
        {
            return await _context.Courses.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Insert(Course course)
        {
            _context.Courses.Add(course);   
            await _context.SaveChangesAsync();
        }

        public async Task Update(Course course)
        {
            _context.Courses.Update(course);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Faculty>?> getAllFaculties()
        {
            return await _context.Faculties.ToListAsync();
        }

        public async Task<List<Course>?> GetCoursesByFacultyId(int? facultyId)
        {
            return await _context.Courses
                .Where(c => c.FacultyId == facultyId)
                .ToListAsync();
        }

        public async Task<List<Course>?> GetAvailableCoursesByFacultyId(int facultyId, int studentId)
        {
            var allCourses = await _context.Courses.Where(c => c.FacultyId == facultyId).ToListAsync();
            var selectedCoursesByUser = await _context.Enrollments.Where(e => e.StudentID == studentId)
                .Select(e => e.CourseID)
                .ToListAsync();

            return allCourses.Where(c => !selectedCoursesByUser.Contains(c.Id)).ToList();
        }
    }
}
