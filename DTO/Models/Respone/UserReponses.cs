using DTO.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Models
{
    public class UserLoginReponse
    {
        public User User { get; set; }
    }

    public class CreateUserReponse
    {
        public int UserId { get; set; }
    }
}
