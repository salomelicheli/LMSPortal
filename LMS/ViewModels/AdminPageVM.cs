using LMS.Models;
using System.ComponentModel.DataAnnotations;

namespace LMS.ViewModels
{
    public class AdminPageVM
    {
        [Required]
        public string Name { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        [Range(4, 10, ErrorMessage = "choose valid credit number:")]
        public int Credit { get; set; }
        public List<Course>? Courses { get; set; }  = new List<Course>();
        public int SelectedFacultyId { get; set; }
        public int? FilterId { get; set; }   
        public List<Faculty>? Faculties { get; set;} 
    }
}
