using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DTO.Entities
{
   public class Treatment
    {
        [Key]
        public int TreatmentId { get; set; }

        [Required]
        public string Name { get; set; }

        [ForeignKey("TreatmentParent")]
        public int? TreatmentParentId { get; set; }

        [ForeignKey("Premises")]
        public int PremisesId { get; set; }

        public virtual Premises Premises { get; set; }
        public virtual Treatment TreatmentParent { get; set; }

        public virtual ICollection<Food> Foods { get; set; }
        public virtual ICollection<Treatment> Treatments { get; set; }
    }
}
