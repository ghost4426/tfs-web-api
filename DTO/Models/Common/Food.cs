using DTO.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Models
{
    public class Food
    {
        public int FoodId { get; set; }

        public string Breed { get; set; }

        public int CategoryId { get; set; }

        public DateTime CreatedDate { get; set; }

        public Category Category { get; set; }
        //public virtual Premises Provider { get; set; }
        //public virtual Premises Farm { get; set; }
        //public virtual Treatment Treatment { get; set; }

    }
}
