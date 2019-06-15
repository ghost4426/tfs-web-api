using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Models
{
   public class CreateNewUserRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
    }
}
