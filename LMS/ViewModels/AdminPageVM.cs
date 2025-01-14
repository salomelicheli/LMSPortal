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
        public List<Course>? Courses { get; set; }  = new List<Course>();
        [Required(ErrorMessage = "გთხოვთ აირჩიოთ ფაკულტეტი.")]
        public int SelectedFacultyId { get; set; }
        public int? FilterId { get; set; }   
        public List<Faculty>? Faculties { get; set;} 
    }
}
