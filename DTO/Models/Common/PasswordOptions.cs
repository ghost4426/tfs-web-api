using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Models
{
   public class PasswordOptions
    {
        public int RequiredLength { get; set; }

        public bool RequireNonAlphanumeric { get; set; }

        public bool RequireDigit { get; set; }

        public bool RequireLowercase { get; set; }

        public bool RequireUppercase { get; set; }
    }
}
