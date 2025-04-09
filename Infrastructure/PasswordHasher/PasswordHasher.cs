using Core.Domain.Entities;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.PasswordHasher
{
    public class PassworHasher : IPasswordHasher
    {
        public string GetHashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);

               
                return Convert.ToBase64String(hashBytes);
            }
        }

        public bool VerifyPassword(User user,string password)
        {
            string hashedInputPassword = GetHashPassword(password);
            return hashedInputPassword == user.PasswordHash;
        }
    }
}
