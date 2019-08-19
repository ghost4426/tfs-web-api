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

        [ForeignKey("Sender")]
        public int SenderId { get; set; }

        [ForeignKey("Receiver")]
        public int ReceiverId { get; set; }
        
        [ForeignKey("Food")]
        public int FoodId { get; set; }

        [ForeignKey("Veterinary")]
        public int? VeterinaryId { get; set; }

        [ForeignKey("TransactionStatus")]
        public int StatusId { get; set; }
       
        [ForeignKey("CreateBy")]
        public int CreateById { get; set; }

        public string CertificationNumber { get; set; }

        [ForeignKey("RejectBy")]
        public int? RejectById { get; set; }

        public string RejectReason { get; set; }

        public string VeterinaryComment { get; set; }

        public string ReceiverComment { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? VerifyDate { get; set; }

        public DateTime? ConfirmDate { get; set; }

        public Premises Sender { get; set; }
        public Premises Receiver { get; set; }
        public User Veterinary { get; set; }
        public Food Food { get; set; }
        public User CreateBy { get; set; }
        public User RejectBy { get; set; }
        public TransactionStatus TransactionStatus { get; set; }
    }
}
