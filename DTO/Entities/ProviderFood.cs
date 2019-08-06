using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DTO.Entities
{
   public class ProviderFood
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
