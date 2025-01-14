using LMS.Models;
using Microsoft.EntityFrameworkCore;

namespace LMS.Data
{
    public class LMSContext : DbContext
    {
        public LMSContext(DbContextOptions<LMSContext> options) : base(options) 
        {
            
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Faculty> Faculties { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Enrollment>().HasKey(e => new { e.StudentID, e.CourseID});
            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Course)
                .WithMany(c => c.Students)
                .HasForeignKey(e => e.CourseID);

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.User)
                .WithMany(c => c.Courses)
                .HasForeignKey(e => e.StudentID);

            modelBuilder.Entity<Course>()
                .HasOne(f => f.Faculty)
                .WithMany(c => c.Courses)
                .HasForeignKey(f => f.FacultyId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasOne(f => f.Faculty)
                .WithMany(u => u.Users)
                .HasForeignKey(f => f.FacultyId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
