using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DTO.Entities
{
   public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }

        [ForeignKey("Farm")]
        public int FarmId { get; set; }

        [ForeignKey("Provider")]
        public int ProviderId { get; set; }

        [ForeignKey("Food")]
        public int FoodId { get; set; }

        [ForeignKey("TransactionStatus")]
        public int StatusId { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ConfirmDate { get; set; }

        public Premises Farm { get; set; }
        public Premises Provider { get; set; }
        public Food Food { get; set; }
        public TransactionStatus TransactionStatus { get; set; }
    }
}
