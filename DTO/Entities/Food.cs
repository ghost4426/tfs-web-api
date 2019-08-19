﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DTO.Entities
{
    public class Food
    {
        [Key]
        public int FoodId { get; set; }

        [Required]
        public string Breed { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        [ForeignKey("Farm")]
        public int FarmId { get; set; }

        public DateTime CreateDate { get; set; }

        [ForeignKey("CreateBy")]
        public int CreateById { get; set; }

        public bool IsReadyForSale { get; set; }

        public bool IsSoldOut { get; set; }

        public Category Category { get; set; }
        public Premises Farm { get; set; }
        public User CreateBy { get; set; }

        public ICollection<DistributorFood> DistributorFoods { get; set; }
        public ICollection<ProviderFood> ProviderFoods { get; set; }
        public ICollection<FoodDetail> FoodDetails { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
        public ICollection<FeedingFood> FeedingFoods { get; set; }
        public ICollection<VaccineFood> VaccineFoods { get; set; }

    }
}
