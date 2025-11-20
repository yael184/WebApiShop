using Repository;
using System.Net.Security;
using WebApiShop;
namespace Services
{
    public class UsersService : IUsersService
    {
        public UsersService(IUsersRepository repository, IPasswordsService passwordsService)
        {
            this.repository = repository;
            this.passwordsService = passwordsService;
        }
        IUsersRepository repository;
        IPasswordsService passwordsService;
        public User? GetUserById(int id)
        {
            return repository.GetUserById(id);
        }

        public User? CreateUser(User user)
        {
            int Level = passwordsService.passwordValidation(user.Password);
            if (Level < 3)
                return null;
            return repository.CreateUser(user);
        }
        public User? Login(User loggedUser)
        {
            return repository.Login(loggedUser);
        }
        public void UpdateUser(int id, User user)
        {
            int Level = passwordsService.passwordValidation(user.Password);
            if (Level < 3)
                throw new("Password is too weak");
            repository.UpdateUser(id, user);
        }
    }
}
