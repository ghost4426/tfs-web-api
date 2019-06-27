using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Models
{
    public class CreateUserRequest
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public int RoleId { get; set; }
    }

}
