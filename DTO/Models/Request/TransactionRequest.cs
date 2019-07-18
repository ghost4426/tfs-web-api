﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Models
{
    public class TransactionRequest
    {
        public int FarmId { get; set; }
        public int ProviderId { get; set; }
        public int FoodId { get; set; }
    }

    public class TransactionUpdateRequest
    {
        public int StatusId { get; set; }
        public string RejectedReason { get; set; }
    }
}
