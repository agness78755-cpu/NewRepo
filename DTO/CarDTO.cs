using System.ComponentModel.DataAnnotations;

namespace CarRentalAPI.DTO
{
    public class CarDTO
    {
        [StringLength(50)]
        public string Make { get; set; }
        [StringLength(50)]
        public string Model { get; set; }
        public int Year { get; set; }
        [StringLength(50)]
        public string LicensePlate { get; set; }
        public decimal DailyRate { get; set; }
    }
}
