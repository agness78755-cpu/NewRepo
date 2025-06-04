using CarRentalAPI.Models;
using CarRentalAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register([FromBody] User registerModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _authService.Register(registerModel);
            return CreatedAtAction(nameof(Login), new { username = user.Username }, user);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var token = await _authService.Login(loginModel);
            return Ok(new { Token = token, username = loginModel.Username, status = 1 });
        }
        [HttpGet("getuser/{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _authService.GetUser(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("getalluser")]
        public async Task<ActionResult<List<User>>> GetAllUser()
        {
            var users = await _authService.GetAllUser();
            return Ok(users);
        }
        [HttpPut("updateuser/{id}")]
        public async Task<ActionResult<User>> UpdateUser(int id, [FromBody] User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedUser = await _authService.UpdateUser(id, user);
            if (updatedUser == null)
                return NotFound();

            return Ok(updatedUser);
        }
        [HttpDelete("deleteuser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _authService.DeleteUser(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }

}
