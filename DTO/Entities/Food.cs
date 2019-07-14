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

        [ForeignKey("Treatment")]
        public int? TreatmentId { get; set; }

        public bool IsFeeding { get; set; }

        public bool IsVaccination { get; set; }

        public bool IsCertification { get; set; }

        public bool IsTreatment { get; set; }

        public bool IsPackaging { get; set; }

        public DateTime CreatedDate { get; set; }

        [ForeignKey("CreatedBy")]
        public int CreatedById { get; set; }


        public Category Category { get; set; }
        public Premises Farm { get; set; }
        public Treatment Treatment { get; set; }
        public User CreatedBy { get; set; }
        
        public ICollection<DistributorFood> DistributorFoods { get; set; }
        public ICollection<ProviderFood> ProviderFoods { get; set; }
        public ICollection<FoodDetail> FoodDetails { get; set; }
        public ICollection<Transaction> Transactions { get; set; }

    }
}
