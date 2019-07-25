using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Models
{
    public class TransactionReponse
    {
        public class CreateTransactionReponse
        {
            public int TransactionId { get; set; }
        }

        public class FarmGetTransaction
        {
            public int TransactionId { get; set; }
            public string Provider { get; set; }
            public int FoodId { get; set; }
            public string FoodName { get; set; }
            public string FoodBreed { get; set; }
            public DateTime CreatedDate { get; set; }
            public int StatusId { get; set; }
            public string Status { get; set; }
            public string RejectedReason { get; set; }
        }

        public class ProviderGetTransaction
        {
            public int TransactionId { get; set; }
            public string Farm { get; set; }
            public int FoodId { get; set; }
            public string FoodName { get; set; }
            public string FoodBreed { get; set; }
            public DateTime CreatedDate { get; set; }
            public int StatusId { get; set; }
            public string Status { get; set; }
            public string RejectedReason { get; set; }
        }
    }
}
