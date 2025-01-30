using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.Hashing
{
    public class PasswordHasher
    {
        public static byte[] HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            return sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        public static bool VerifyPassword(byte[] hashedPassword, string inputPassword)
        {
            var hashedInput = HashPassword(inputPassword);
            return hashedPassword.SequenceEqual(hashedInput);
        }
    }
}
