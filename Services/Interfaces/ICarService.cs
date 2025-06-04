using CarRentalAPI.DTO;
using CarRentalAPI.Models;

namespace CarRentalAPI.Services.Interfaces
{
    public interface ICarService
    {
        Task<IEnumerable<Car>> GetAllCarsAsync();
        Task<Car> GetCarByIdAsync(int id);
        Task<Car> AddCarAsync(CarDTO carDto);
        Task<bool> DeleteCarAsync(int id);
    }
}
