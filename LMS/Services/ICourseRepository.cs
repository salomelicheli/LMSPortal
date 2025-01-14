using LMS.Models;

namespace LMS.Services
{
    public interface ICourseRepository
    {
        Task<List<Course>?> GetAll();
        Task<Course?> GetById(int id);
        Task Insert(Course course);
        Task Update(Course course);
        Task Delete(Course course);
        Task<List<Faculty>?> getAllFaculties();
        Task<List<Course>?> GetCoursesByFacultyId(int? facultyId);
        Task<List<Course>?> GetAvailableCoursesByFacultyId(int facultyId, int studentId);
    }
}
