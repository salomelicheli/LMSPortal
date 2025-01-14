using LMS.Models;

namespace LMS.Services
{
    public interface IUserRespository
    {
        Task<List<User>> GetStudentsByRole();
        Task <User?> GetUserById(int id);
        Task AddEnrollment(int userId, int courseId);
        Task DeleteEnrollment(int userId, int courseId);
        Task<User> GetUserByCredentials(string personalID, string password);
        Task UpdateUser(User user);
        Task<Faculty?> GetFacultyById(int? id);
        Task<User> GetStudentsByPersonalId(string personalId);
        Task<List<Course>> GetEnrollmentsByStudentId(int id); 
    }
}
