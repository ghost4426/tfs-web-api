using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DTO.Entities
{
   public class TransactionStatus
    {
        [Key]
        public int StatusId { get; set; }

        public string Status { get; set; }

    }
}
