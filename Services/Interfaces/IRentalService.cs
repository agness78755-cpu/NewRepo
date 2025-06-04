using CarRentalAPI.DTO;
using CarRentalAPI.Models;

namespace CarRentalAPI.Services.Interfaces
{
    public interface IRentalService
    {
        Task<IEnumerable<Rental>> GetAllRentalsAsync();
        Task<Rental> AddRentalAsync(RentalDTO rentalDto);
        Task<bool> ReturnCarAsync(int rentalId, DateTime returnDate);
    }
}
