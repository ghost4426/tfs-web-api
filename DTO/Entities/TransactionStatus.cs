using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DTO.Entities
{
   public class TransactionStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StatusId { get; set; }

        [Required]
        public string Status { get; set; }

        public ICollection<Transaction> Transactions { get; set; }

    }
}
