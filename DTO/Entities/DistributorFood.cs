using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DTO.Entities
{
   public class DistributorFood
    {
        [ForeignKey("Premises")]
        public int PremisesId { get; set; }

        [ForeignKey("Food")]
        public int FoodId { get; set; }

        public DateTime CreateDate { get; set; }

        public Premises Premises { get; set; }
        public Food Food { get; set; }
    }
}
