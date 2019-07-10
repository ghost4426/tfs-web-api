using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Models
{
   public class CreateFoodRequest
    {
        public int CategoryId { get; set; }

        public string Breed { get; set; }

    }

    public class PackagingRequest
    {
        public DateTime MFGDate { get; set; }

        public DateTime EXPDate { get; set; }

    }
}
