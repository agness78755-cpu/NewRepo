using CarRentalAPI.Data;
using CarRentalAPI.Models;
using CarRentalAPI.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CarRentalAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthService> _logger;

        public AuthService(
            AppDbContext context,
            IConfiguration configuration,
            ILogger<AuthService> logger)
        {
            _context = context;
            _configuration = configuration;
            _logger = logger;
        }
        public async Task<User> Register(User registerModel)
        {
            try
            {
                _logger.LogInformation("Registering user {Username}", registerModel.Username);

                if (await _context.Users.AnyAsync(u => u.Username == registerModel.Username))
                    throw new ValidationException("Username already exists");

                if (await _context.Users.AnyAsync(u => u.Email == registerModel.Email))
                    throw new ValidationException("Email already exists");

                var user = new User
                {
                    firstname = registerModel.firstname,
                    lastname = registerModel.lastname,
                    Username = registerModel.Username,
                    Email = registerModel.Email,
                    Role = registerModel.Role,
                    Password = HashPassword(registerModel.Password),
                    CreatedAt = DateTime.Now
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                _logger.LogInformation("User {Username} registered successfully", user.Username);
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error registering user {Username}", registerModel.Username);
                throw;
            }
        }

        public async Task<string> Login(LoginModel loginModel)
        {
            try
            {
                _logger.LogInformation("Login attempt for {Username}", loginModel.Username);

                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Username == loginModel.Username);

                if (user == null || !VerifyPassword(loginModel.Password, user.Password))
                    throw new UnauthorizedAccessException("Invalid credentials");

                var token = GenerateJwtToken(user);

                _logger.LogInformation("User {Username} logged in successfully", user.Username);
                return token;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during login for {Username}", loginModel.Username);
                throw;
            }
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }

        private bool VerifyPassword(string password, string hash)
        {
            var computedHash = HashPassword(password);
            return computedHash == hash;
        }

        private string GenerateJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(ClaimTypes.Name, user.Username), // Username claim
            new Claim(ClaimTypes.Role, user.Role)      // Role claim
        };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<User> GetUser(int id)
        {
            try
            {
                _logger.LogInformation("Fetching user with ID {Id}", id);
                var user = await _context.Users.FindAsync(id);
                if (user == null)
                    throw new KeyNotFoundException("User not found");

                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching user with ID {Id}", id);
                throw;
            }
        }
        public async Task<List<User>> GetAllUser()
        {
            try
            {
                _logger.LogInformation("Fetching all users");
                return await _context.Users.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all users");
                throw;
            }
        }
        public async Task<User> UpdateUser(int id, User user)
        {
            try
            {
                _logger.LogInformation("Updating user with ID {Id}", id);
                var existingUser = await _context.Users.FindAsync(id);
                if (existingUser == null)
                    throw new KeyNotFoundException("User not found");

                existingUser.firstname = user.firstname;
                existingUser.lastname = user.lastname;
                existingUser.Email = user.Email;
                existingUser.Username = user.Username;
                existingUser.Role = user.Role;

                _context.Users.Update(existingUser);
                await _context.SaveChangesAsync();

                return existingUser;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating user with ID {Id}", id);
                throw;
            }
        }
        public async Task<bool> DeleteUser(int id)
        {
            try
            {
                _logger.LogInformation("Deleting user with ID {Id}", id);
                var user = await _context.Users.FindAsync(id);
                if (user == null)
                    throw new KeyNotFoundException("User not found");

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting user with ID {Id}", id);
                throw;
            }
        }
    }
}
