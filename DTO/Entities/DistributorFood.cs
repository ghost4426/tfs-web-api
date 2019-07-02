using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DTO.Entities
{
   public class DistributorFood
    {
        public int PremisesId { get; set; }

        public Premises Premises { get; set; }

        public int FoodId { get; set; }

        public Food Food { get; set; }
    }
}
