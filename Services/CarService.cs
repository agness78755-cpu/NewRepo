using System.Data.Entity;
using CarRentalAPI.Data;
using CarRentalAPI.DTO;
using CarRentalAPI.Models;
using CarRentalAPI.Services.Interfaces;


namespace CarRentalAPI.Services
{
    public class CarService : ICarService
    {
        private readonly AppDbContext _context;

        public CarService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Car>> GetAllCarsAsync() => await _context.Cars.ToListAsync();

        public async Task<Car> GetCarByIdAsync(int id) => await _context.Cars.FindAsync(id);

        public async Task<Car> AddCarAsync(CarDTO carDto)
        {
            var car = new Car
            {
                Make = carDto.Make,
                Model = carDto.Model,
                Year = carDto.Year,
                LicensePlate = carDto.LicensePlate,
                DailyRate = carDto.DailyRate,
                Status = "Available"
            };
            _context.Cars.Add(car);
            await _context.SaveChangesAsync();
            return car;
        }

        public async Task<bool> DeleteCarAsync(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null) return false;
            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
