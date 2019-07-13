using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DTO.Entities
{
   public class FoodDetail
    {
        [Key]
        public int DetailId { get; set; }

        [ForeignKey("Type")]
        public int TypeId { get; set; }

        public int BlockNumber { get; set; }

        [Required]
        public string TransactionHash { get; set; }

        [ForeignKey("Food")]
        public int FoodId { get; set; }

        [ForeignKey("CreatedBy")]
        public int CreatedById { get; set; }

        public DateTime CreatedDate { get; set; }

        public virtual FoodDetailType Type { get; set; }
        public Food Food { get; set; }
        public virtual User CreatedBy { get; set; }
    }
}
