using DTO.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Models
{
    public class Food
    {
        public int CategoryId { get; set; }

        public string Breed { get; set; }

        public int FarmerId { get; set; }
    }
}
