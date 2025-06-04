using System.Data.Entity;
using CarRentalAPI.Data;
using CarRentalAPI.DTO;
using CarRentalAPI.Models;
using CarRentalAPI.Services.Interfaces;

namespace CarRentalAPI.Services
{
    public class RentalService : IRentalService
    {
        private readonly AppDbContext _context;

        public RentalService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Rental>> GetAllRentalsAsync() => await _context.Rentals.Include(r => r.Car).ToListAsync();

        public async Task<Rental> AddRentalAsync(RentalDTO rentalDto)
        {
            var car = await _context.Cars.FindAsync(rentalDto.CarID);
            if (car == null || car.Status != "Available") return null;

            var rental = new Rental
            {
                CarID = rentalDto.CarID,
                CustomerName = rentalDto.CustomerName,
                RentalDate = rentalDto.RentalDate,
                ReturnDate = rentalDto.ReturnDate
            };

            car.Status = "Rented";
            _context.Rentals.Add(rental);
            await _context.SaveChangesAsync();
            return rental;
        }

        public async Task<bool> ReturnCarAsync(int rentalId, DateTime returnDate)
        {
            var rental = await _context.Rentals.Include(r => r.Car).FirstOrDefaultAsync(r => r.RentalID == rentalId);
            if (rental == null || rental.ReturnDate != null) return false;

            rental.ReturnDate = returnDate;
            var totalDays = (returnDate - rental.RentalDate).Days;
            rental.TotalCost = totalDays * rental.Car.DailyRate;
            rental.Car.Status = "Available";

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
