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

        [Required]
        public string TransactionHash { get; set; }

        [ForeignKey("Food")]
        public int FoodId { get; set; }

        [ForeignKey("CreateBy")]
        public int CreateById { get; set; }

        public DateTime CreateDate { get; set; }

        public FoodDetailType Type { get; set; }
        public Food Food { get; set; }
        public User CreateBy { get; set; }
    }
}
