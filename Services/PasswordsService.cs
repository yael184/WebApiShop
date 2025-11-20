using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Zxcvbn;

namespace Services
{
    public class PasswordsService : IPasswordsService
    {
        public int passwordValidation(string password)
        {
            Password _password = new Password();
            _password.Name = password;
            _password.Level = Zxcvbn.Core.EvaluatePassword(password).Score;
            return _password.Level;
        }
    }
}
