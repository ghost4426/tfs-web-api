using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DTO.Entities
{
   public class DistributorFood
    {
        [ForeignKey("Premises")]
        public int PremisesId { get; set; }

        [ForeignKey("Food")]
        public int FoodId { get; set; }

        public DateTime CreatedDate { get; set; }

        public Premises Premises { get; set; }
        public Food Food { get; set; }
    }
}
