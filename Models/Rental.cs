using System.ComponentModel.DataAnnotations;

namespace CarRentalAPI.Models
{
    public class Rental
    {
        public int RentalID { get; set; }
        public int CarID { get; set; }
        [Required, StringLength(50)]
        public string CustomerName { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public decimal? TotalCost { get; set; }

        public Car Car { get; set; }
    }
}
