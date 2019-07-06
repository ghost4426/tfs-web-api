using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

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

        [ForeignKey("Provider")]
        public int? ProviderId { get; set; }

        [ForeignKey("Treatment")]
        public int? TreatmentId { get; set; }

        public bool IsFeeding { get; set; }

        public bool IsVaccination { get; set; }

        public bool IsCertification { get; set; }

        public bool IsTreatment { get; set; }

        public bool IsPackaging { get; set; }

        public DateTime CreatedDate { get; set; }

        public virtual Category Category { get; set; }
        public virtual Premises Provider { get; set; }
        public virtual Premises Farm { get; set; }
        public virtual Treatment Treatment { get; set; }
        
        public virtual ICollection<DistributorFood> DistributorFoods { get; set; }
        public virtual ICollection<FoodDetail> FoodDetails { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }

    }
}
