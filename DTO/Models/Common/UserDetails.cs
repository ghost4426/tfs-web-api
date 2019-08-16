using System;
using System.Collections.Generic;
using DTO.Entities;
using System.Text;

namespace DTO.Models
{
    public class UserDetails
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Fullname { get; set; }
        public string Role { get; set; }
        public string PremisesName { get; set; }
        public string PremisesType { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
        public string PhoneNo { get; set; }
        public bool IsActive { get; set; }
    }
}
