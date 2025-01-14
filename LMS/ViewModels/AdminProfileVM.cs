using LMS.Models;
using System.ComponentModel.DataAnnotations;

namespace LMS.ViewModels
{
    public class AdminProfileVM
    {
        public string? Name { get; set; }
        //[MaxLength(500)]
        //public string? Message { get; set; }
        //public List<User>? Students { get; set; }
        public User? User { get; set; }
        public string? SearchId { get; set; }
    }
}
