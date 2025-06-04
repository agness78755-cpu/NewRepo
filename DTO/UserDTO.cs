using System.ComponentModel.DataAnnotations;

namespace CarRentalAPI.DTO
{
    public class UserDto
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string? firstname { get; set; }
        [StringLength(50)]
        public string? lastname { get; set; }
        [StringLength(100)]
        public string? Email { get; set; }
        [StringLength(50)]
        public string? Username { get; set; }
        public string? Password { get; set; }
        public DateTime CreatedAt { get; set; }
    }


}
