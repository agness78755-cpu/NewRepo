using CarRentalAPI.DTO;
using CarRentalAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCars() => Ok(await _carService.GetAllCarsAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCar(int id)
        {
            var car = await _carService.GetCarByIdAsync(id);
            if (car == null) return NotFound();
            return Ok(car);
        }

       [HttpPost]
public async Task<IActionResult> AddCar(CarDTO carDto)
{
    try
    {
        var newCar = await _carService.AddCarAsync(carDto);
        return CreatedAtAction(nameof(GetCar), new { id = newCar.CarID }, newCar);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"AddCar failed: {ex.Message}");
        return StatusCode(500, new { error = ex.Message });
    }
}


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            var result = await _carService.DeleteCarAsync(id);
            return result ? Ok() : NotFound();
        }
    }

}
