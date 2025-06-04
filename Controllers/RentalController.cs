using CarRentalAPI.DTO;
using CarRentalAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RentalController : ControllerBase
    {
        private readonly IRentalService _rentalService;

        public RentalController(IRentalService rentalService)
        {
            _rentalService = rentalService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRentals() => Ok(await _rentalService.GetAllRentalsAsync());

        [HttpPost]
        public async Task<IActionResult> AddRental(RentalDTO rentalDto)
        {
            var rental = await _rentalService.AddRentalAsync(rentalDto);
            if (rental == null) return BadRequest("Car is not available.");
            return CreatedAtAction(nameof(GetAllRentals), new { id = rental.RentalID }, rental);
        }

        [HttpPost("return/{id}")]
        public async Task<IActionResult> ReturnCar(int id, [FromBody] DateTime returnDate)
        {
            var result = await _rentalService.ReturnCarAsync(id, returnDate);
            return result ? Ok("Car returned successfully.") : BadRequest("Invalid return.");
        }
    }

}
