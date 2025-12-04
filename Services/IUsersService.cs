using Entities;


namespace Services
{
    public interface IUsersService
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User?> CreateUser(User user);
        Task<User?> GetUserById(int id);
        Task<User?> Login(User loggedUser);
        Task UpdateUser(int id, User user);
    }
}