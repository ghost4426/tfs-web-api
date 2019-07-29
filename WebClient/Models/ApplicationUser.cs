using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Models
{
    public class User
    {

        public string Username { get; set; }

        public string Fullname { get; set; }

        public string Role { get; set; }
            
        public string Premises { get; set; }
        public string Image { get; set; }   
    }

    public class UserLoginReponse
    {
        public User User { get; set; }
        public string Token { get; set; }
    }

    public class LoginInfo
    {
        public UserLoginReponse Data { get; set; }
        public string Message { get; set; }
    }
}
