using DTO.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Models
{
    public class Food
    {
        public string Name { get; set; }

        public int CategoriesId { get; set; }

        public int FamerId { get; set; }
    }
}
