namespace LMS.Models
{
    public class Enrollment
    {
        public int CourseID { get; set; }
        public int StudentID { get; set; }
        public Course Course { get; set; }
        public User User { get; set; }
        public DateTime EnrollmentDate { get; set; }
    }
}
