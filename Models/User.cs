using System.ComponentModel.DataAnnotations;

namespace CarRentalAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required, StringLength(50)]
        public string firstname { get; set; }
        [Required, StringLength(50)]
        public string lastname { get; set; }
        [Required, StringLength(100)]
        public string Email { get; set; }
        [Required, StringLength(50)]
        public string Username { get; set; }
        [Required, StringLength(50)]
        public string Role { get; set; }
        [Required]
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
