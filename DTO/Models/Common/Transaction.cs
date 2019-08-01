using DTO.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Models { 
    public class Transaction
    {
        public int TransactionId { get; set; }
        public Premises Farm { get; set; }
        public Premises Provider { get; set; }
        public Food Food { get; set; }
        public int FoodId { get; set; }
        public int ProviderId { get; set; }
        public int FarmId { get; set; }
    }
}
