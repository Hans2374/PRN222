using System.ComponentModel.DataAnnotations;

namespace GameStore.Data.Models
{
    public partial class User
    {
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        [Required]
        public string Role { get; set; } = null!;
    }
}