using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DTO.Entities
{
   public class ProviderFood
    {
        [ForeignKey("Premises")]
        public int PremisesId { get; set; }

        [ForeignKey("Food")]
        public int FoodId { get; set; }

        [ForeignKey("Treatment")]
        public int? TreatmentId { get; set; }

        public DateTime CreateDate { get; set; }

        public bool IsTreatmented { get; set; }

        public bool IsPacked { get; set; }

        public Premises Premises { get; set; }
        public Food Food { get; set; }
        public Treatment Treatment { get; set; }
    }
}
