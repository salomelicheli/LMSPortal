using LMS.Data;
using LMS.Models;
using LMS.Services;
using Microsoft.EntityFrameworkCore;

namespace LMS.Repositories
{
    public class UserRepository : IUserRespository
    {
        private readonly LMSContext _context;

        public UserRepository(LMSContext context)
        {
            _context = context;
        }

        public async Task AddEnrollment(int userId, int courseId)
        {
            Enrollment enrollment = new Enrollment
            {
                CourseID = courseId, 
                StudentID = userId,
                EnrollmentDate = DateTime.UtcNow,
            };

            _context.Enrollments.Add(enrollment);

            await _context.SaveChangesAsync();   
        }


        public async Task DeleteEnrollment(int userId, int courseId)
        {
            Enrollment enrollment = await _context
                .Enrollments.FirstOrDefaultAsync(e => e.StudentID == userId && e.CourseID == courseId);

            if (enrollment != null)
            {
                _context.Enrollments.Remove(enrollment);

                await _context.SaveChangesAsync();
            }         
        }

        public async Task<List<User>> GetStudentsByRole()
        {
            return await _context.Users
                .Where(u => u.UserRole == Role.Student)
                .ToListAsync(); 
        }

        public async Task<User?> GetUserById(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> GetUserByCredentials(string personalID, string password)
        {
           return await _context.Users.FirstOrDefaultAsync(u => 
                             u.PersonalID == personalID && u.Password == password);
        }

        public async Task UpdateUser(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task<Faculty> GetFacultyById(int? id)
        {
            return await _context.Faculties.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User> GetStudentsByPersonalId(string personalId)
        {
            if(string.IsNullOrEmpty(personalId))
            {
                return null;  
            }
            return await _context.Users
              .FirstOrDefaultAsync(u => u.UserRole == Role.Student && u.PersonalID == personalId);
        }

        public async Task<List<Course>> GetEnrollmentsByStudentId(int id)
        {
            var enrollments = await _context.Enrollments.Where(e => e.StudentID == id).Select(e => e.CourseID).ToListAsync();
            var courses = await _context.Courses.Where(c => enrollments.Contains(c.Id)).ToListAsync();

            return courses;
        }

        public async Task<List<Enrollment>> GetEnrollmentsById(int id)
        {
            var enrollments = await _context.Enrollments.Where(e => e.StudentID == id).ToListAsync();

            return enrollments;
        }
    }
}
