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
    public class UpdateUserRequest
    {
        public string userId { get; set; }
        public string fullName { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
    }
    public class RoleUserRequest
    {
        public int RoleId { get; set; }
    }

}
