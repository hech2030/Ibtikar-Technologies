using ECommerceApp.Models;

namespace ECommerceApp.Services.Interfaces
{
    public interface IUserService
    {
        User Register(User value);
        User Login(string email, string password);
    }
}
