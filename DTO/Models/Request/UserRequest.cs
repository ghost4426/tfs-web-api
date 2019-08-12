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
        public int UserId { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        
    }
    public class ChangePasswordUserRequest
    {
        public string newPass { get; set; }
        public string oldPass { get; set; }
    }
    public class RoleUserRequest
    {
        public int RoleId { get; set; }
    }

    public class CreateUserPremises
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Fullname { get; set; }
    }

}
