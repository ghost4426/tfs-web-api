using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Models
{
   public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class RegisterRequest
    {
        public string PremisesName { get; set; }

        public string PremisesAddress { get; set; }

        public string PremisesTypeId { get; set; }

        public string Username { get; set; }

        public string Fullname { get; set; }

        public string Email { get; set; }

    }
}
