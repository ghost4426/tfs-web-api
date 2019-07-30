using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Models
{
   public class User
    {
        public int UserId { get; set; }

        public string Username { get; set; }

        public string Fullname { get; set; }

        public Role Role { get; set; }

        public string Email { get; set; }

        public string PhoneNo { get; set; }

        public bool IsActive { get; set; }

    }
}
