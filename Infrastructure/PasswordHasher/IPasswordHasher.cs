using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.PasswordHasher
{
    public interface IPasswordHasher
    {
        string GetHashPassword(string password);
        bool VerifyPassword(User user,string password);
    }
}
