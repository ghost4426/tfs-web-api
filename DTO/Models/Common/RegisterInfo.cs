using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Models
{
    public class RegisterInfo
    {
        public int RegisterId { get; set; }
        public string PremisesName { get; set; }
        public string PremisesAddress { get; set; }
        public PremisesType PremisesType { get; set; }
        public string Username { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool? IsConfirm { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
