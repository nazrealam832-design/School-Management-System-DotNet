using System.ComponentModel.DataAnnotations;

namespace School_Management_System.Models
{
    public class Teacher
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Subject is required")]
        public string Subject { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mobile is required")]
        public string Mobile { get; set; } = string.Empty;

        public int Experience { get; set; }
    }
}