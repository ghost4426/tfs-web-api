using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DTO.Entities
{
   public class FeedingFood
    {
        [ForeignKey("Feeding")]
        public int FeedingId { get; set; }

        [ForeignKey("Food")]
        public int FoodId { get; set; }

        public DateTime CreateDate { get; set; }

        public Feeding Feeding { get; set; }
        public Food Food { get; set; }
    }
}
