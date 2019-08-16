﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Models
{
    public class CreateRegisterInfoRequest
    {
        public string PremisesName { get; set; }
        public string PremisesAddress { get; set; }
        public int PremisesTypeId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
