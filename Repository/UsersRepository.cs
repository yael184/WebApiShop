using Entities;

using System.Text.Json;
using WebApiShop;

namespace Repository
{
    public class UsersRepository : IUsersRepository
    {
        private string _filePath = @"../Users.txt";

        //public IEnumerable<User> GetUsers()
        //{

        //}

        public User? GetUserById(int id)
        {
            using (StreamReader reader = System.IO.File.OpenText(_filePath))
            {
                string? currentUserInFile;
                while ((currentUserInFile = reader.ReadLine()) != null)
                {
                    User? user = JsonSerializer.Deserialize<User>(currentUserInFile);
                    if (user?.Id == id)
                        return user;
                }
            }
            return null;
        }

        public User CreateUser(User user)
        {
            int numberOfUsers = System.IO.File.ReadLines(_filePath).Count();
            user.Id = numberOfUsers + 1;
            string userJson = JsonSerializer.Serialize(user);
            System.IO.File.AppendAllText(_filePath, userJson + Environment.NewLine);
            return user;
        }

        public User? Login(User loggedUser)
        {

            using (StreamReader reader = System.IO.File.OpenText(_filePath))
            {
                string? currentUserInFile;
                while ((currentUserInFile = reader.ReadLine()) != null)
                {
                    User? user = JsonSerializer.Deserialize<User>(currentUserInFile);
                    if (user?.UserName == loggedUser.UserName && user.Password == loggedUser.Password)
                        return user;
                }
            }
            return null;
        }

        public void UpdateUser(int id, User loggedUser)
        {
            string textToReplace = string.Empty;
            using (StreamReader reader = System.IO.File.OpenText(_filePath))
            {
                string currentUserInFile;
                while ((currentUserInFile = reader.ReadLine()) != null)
                {

                    User? user = JsonSerializer.Deserialize<User>(currentUserInFile);
                    if (user?.Id == id)
                        textToReplace = currentUserInFile;
                }
            }

            if (textToReplace != string.Empty)
            {
                string text = System.IO.File.ReadAllText(_filePath);
                text = text.Replace(textToReplace, JsonSerializer.Serialize(loggedUser));
                System.IO.File.WriteAllText(_filePath, text);
            }
        }
    }
}
