using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DTO.Entities
{
    public class Food
    {
        [Key]
        public int FoodId { get; set; }

        public string Breed { get; set; }

        public int CategoriesId { get; set; }

        public virtual Categories Categories { get; set; }

        public int FarmerId { get; set; }

        public virtual Premises Farmer { get; set; }

        public int? ProviderId { get; set; }

        public virtual Premises Provider { get; set; }

        public DistributorFood DistributorFood { get; set; }

        public int? TreatmentId { get; set; }

        public Treatment Treatment { get; set; }

        public bool IsFeeding { get; set; } = false;

        public bool IsVaccination { get; set; } = false;

        public bool IsCertification { get; set; } = false;

        public bool IsTreatment { get; set; } = false;

        public bool IsPackaging { get; set; } = false;

        public DateTime CreatedDate { get; set; }
    }
}
