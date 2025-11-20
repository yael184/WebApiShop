using WebApiShop;

namespace Services
{
    public interface IUsersService
    {
        User? CreateUser(User user);
        User? GetUserById(int id);
        User? Login(User loggedUser);
        void UpdateUser(int id, User user);
    }
}