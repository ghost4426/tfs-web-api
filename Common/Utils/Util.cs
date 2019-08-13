using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using DTO.Models;
using DTO.Models.Common;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Common.Utils
{
   public static class Util
    {

        public static string GeneratePassword(PasswordOptions options)
        {

            int length = options.RequiredLength;
            bool nonAlphanumeric = options.RequireNonAlphanumeric;
            bool digit = options.RequireDigit;
            bool lowercase = options.RequireLowercase;
            bool uppercase = options.RequireUppercase;

            StringBuilder password = new StringBuilder();
            Random random = new Random();

            while (password.Length < length)
            {
                char c = (char)random.Next(33, 126);

                if (char.IsDigit(c) && options.RequireDigit)
                {
                    password.Append(c);
                    digit = false;

                }
                else if (char.IsLower(c) && options.RequireLowercase)
                {
                    password.Append(c);
                    lowercase = false;

                }
                else if (char.IsUpper(c) && options.RequireUppercase)
                {
                    password.Append(c);
                    uppercase = false;

                }
                else if (!char.IsLetterOrDigit(c) && options.RequireNonAlphanumeric)
                {
                    password.Append(c);
                    nonAlphanumeric = false;

                }
                if ((digit || lowercase || uppercase || nonAlphanumeric) && password.Length >= length)
                {
                    password.Clear();
                    nonAlphanumeric = options.RequireNonAlphanumeric;
                    digit = options.RequireDigit;
                    lowercase = options.RequireLowercase;
                    uppercase = options.RequireUppercase;
                }
            }
            return password.ToString();
        }

    }
}
