using DTO.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Common.Utils
{
    public static class PasswordHasher
    {
        public static HashPassword GetHashPassword(string password)
        {

            string salt = CreateSalt();

            string hashedPassword = EncryptPassword(salt, password);
            Console.WriteLine(salt);
            //Console.WriteLine(Convert.ToBase64String(Encoding.UTF8.GetBytes(salt)));
            return new HashPassword() { Salt = salt, HashedPassword = hashedPassword };
        }

        public static bool CheckHashedPassword(HashPassword hashPassword)
        {
            return EncryptPassword(hashPassword.Salt, hashPassword.Password) == hashPassword.HashedPassword;
        }

        private static string EncryptPassword(string salt, string password)
        {
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: Encoding.UTF8.GetBytes(salt),
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            return hashed;

        }

        private static string CreateSalt()
        {
            byte[] randomBytes = new byte[128 / 8];
            using (var generator = RandomNumberGenerator.Create())
            {
                generator.GetBytes(randomBytes);
                return Convert.ToBase64String(randomBytes);
            }
        }
    }
}
