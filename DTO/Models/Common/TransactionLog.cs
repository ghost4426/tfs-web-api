using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Models
{
   public class TransactionLog
    {
        public int FoodId { get; set; }

        public string TransactionHash { get; set; }

        public string Type { get; set; }

        public string CreateBy { get; set; }

        public DateTime CreateDate { get; set; }

    }
}
