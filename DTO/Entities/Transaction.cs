using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DTO.Entities
{
   public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }

        public int FarmerId { get; set; }

        public Premises Farmer { get; set; }

        public int ProviderId { get; set; }

        public Premises Provider { get; set; }

        public int FoodId { get; set; }

        public Food Food { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ConfirmDate { get; set; }

        public int StatusId { get; set; }

        public TransactionStatus TransactionStatus { get; set; }
    }
}
