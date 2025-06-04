using System.ComponentModel.DataAnnotations;

namespace CarRentalAPI.Models
{
    public class Car
    {
        public int CarID { get; set; }
        [Required, StringLength(50)]
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string LicensePlate { get; set; }
        [Required, StringLength(50)]
        public string Status { get; set; }
        public decimal DailyRate { get; set; }
    }
}
