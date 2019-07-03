using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Models
{
   public class CreateFoodRequest
    {
        public int CategoriesId { get; set; }

        public string Breed { get; set; }

        public int FarmerId { get; set; }
    }

    public class PackagingRequest
    {
        public DateTime MFGDate { get; set; }

        public DateTime EXPDate { get; set; }

    }
}
