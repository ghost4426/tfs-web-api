﻿using DTO.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Models
{
    public class Food
    {
        public int FoodId { get; set; }

        public string Breed { get; set; }

        public int CategoryId { get; set; }

        public DateTime CreateDate { get; set; }

        public Category Category { get; set; }
        //public virtual Premises Provider { get; set; }
        //public virtual Premises Farm { get; set; }
        //public virtual Treatment Treatment { get; set; }

    }

    public class FoodFarm
    {
        public int FoodId { get; set; }
        public string CategoryName { get; set; }
        public string Breed { get; set; }
        public int CategoryId { get; set; }
        public int FarmId { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsSoldOut { get; set; }
        public bool IsReadyForSale { get; set; }
        public string CreateBy { get; set; }
    }

    public class FoodProvider
    {
        public int FoodId { get; set; }
        public int PremisesId { get; set; }
        public DateTime CreateDate { get; set; }
        public Food Food { get; set; }
        public int? TreatmentId {get;set;}
        public bool IsTreatmented { get; set; }
        public bool IsPacked { get; set; }
    }

    public class FoodDistributor
    {
        public int FoodId { get; set; }
        public int ProviderId { get; set; }
        public int DistributorId { get; set; }
        public string FarmName { get; set; }
        public string ProviderName { get; set; }
        public Food Food { get; set; }
    }
}
