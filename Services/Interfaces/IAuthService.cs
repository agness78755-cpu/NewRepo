using CarRentalAPI.Models;

namespace CarRentalAPI.Services.Interfaces
{
    public interface IAuthService
    {
        Task<User> Register(User user);
        Task<User> GetUser(int id);
        Task<List<User>> GetAllUser();
        Task<User> UpdateUser(int id, User user);
        Task<bool> DeleteUser(int id);
        Task<string> Login(LoginModel loginModel);

    }

}
