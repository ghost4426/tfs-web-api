using DTO.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Models { 
    public class Transaction
    {
        public int TransactionId { get; set; }
        public Premises Sender { get; set; }
        public Premises Receiver { get; set; }
        public Food Food { get; set; }
        public int FoodId { get; set; }
        public int ReceiverId { get; set; }
        public int SenderId { get; set; }
        public int VeterinaryId { get; set; }
        public int StatusId { get; set; }

    }
}
