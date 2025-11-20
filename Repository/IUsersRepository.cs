using WebApiShop;

namespace Repository
{
    public interface IUsersRepository
    {
        User CreateUser(User user);
        User? GetUserById(int id);
        User? Login(User loggedUser);
        void UpdateUser(int id, User loggedUser);
    }
}