using System.ComponentModel.DataAnnotations;

namespace School_Management_System.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = "";

        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";

        [Required]
        public int Age { get; set; }

        [Required]
        public string Course { get; set; } = "";

        [Required]
        public string Mobile { get; set; } = "";
    }
}