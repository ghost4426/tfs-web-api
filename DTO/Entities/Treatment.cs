using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DTO.Entities
{
   public class Treatment
    {
        [Key]
        public int TreatmentId { get; set; }

        public string Name { get; set; }

        public int? ParentTreatmentId { get; set; }

        public int PremisesId { get; set; }

        public virtual Premises Premises { get; set; }
    }
}
