using System.ComponentModel.DataAnnotations;

namespace CartNestGaming.Models
{
    public class AppUser
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public String Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public String Password { get; set; }

        public String Role { get; set; } = "User";  
    }
}
