using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using DTO.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Common.Utils
{
   public static class Util
    {

        public static HashPassword GetHashPassword(string password)
        {
            byte[] salts = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salts);
            }

            string salt = Convert.ToBase64String(salts);

            string hashedPassword = EncryptPassword(salt, password);

            return new HashPassword() { Salt = salt, Password = hashedPassword };
        }

        public static bool CheckHashedPassword(HashPassword hashPassword)
        {
            return string.Compare(hashPassword.HashedPassword, EncryptPassword(hashPassword.Salt, hashPassword.Password)) == 0 ?true: false;
        }

        private static string EncryptPassword(string salt, string password)
        {
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: Encoding.ASCII.GetBytes(salt),
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            return hashed;

        }
        
    }
}
