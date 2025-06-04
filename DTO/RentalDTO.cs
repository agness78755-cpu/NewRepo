using System.ComponentModel.DataAnnotations;

namespace CarRentalAPI.DTO
{
    public class RentalDTO
    {
        public int CarID { get; set; }
        [StringLength(50)]
        public string CustomerName { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
