using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Models
{
    public class TransactionRequest
    {
        public int ReceiverId { get; set; }
        public int FoodId { get; set; }
        public string CertificationNumber { get; set; }
    }

    public class TransactionUpdateRequest
    {
        public int StatusId { get; set; }
        public string RejectedReason { get; set; }
        public string ProviderComment { get; set; }
    }

    public class TransactionVerterinaryUpdateRequest
    {
        public int StatusId { get; set; }
        public string RejectedReason { get; set; }
        public int RejectById { get; set; }
    }
    public class TransactionDistributorUpdateRequest
    {
        public int StatusId { get; set; }
        public string RejectedReason { get; set; }
        public int RejectById { get; set; }
    }
}
