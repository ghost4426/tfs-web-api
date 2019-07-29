using DTO.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Models
{
    public class UserLoginReponse
    {
        public UserData User { get; set; }
        public string Token { get; set; }
    }
    
    public class UserData
    {
        public string Username { get; set; }
        public string Fullname { get; set; }
        public string Role { get; set; }
        public string Premises { get; set; }
        public string Image { get; set; }
    }

    public class CreateUserReponse
    {
        public int UserId { get; set; }
    }
    public class GetUserResponse
    {
        public IList<User> User { get; set; }
    }
    public class UpdateUserReponse
    {
        public int UserId { get; set; }
    }
}
