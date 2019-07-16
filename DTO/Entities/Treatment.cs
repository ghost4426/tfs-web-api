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

        public DateTime CreatedDate { get; set; }

        public int CreatedById { get; set; }

        public Premises Premises { get; set; }
        public Treatment TreatmentParent { get; set; }
        public User CreatedBy { get; set; }

        public ICollection<Food> Foods { get; set; }
        public ICollection<Treatment> Treatments { get; set; }
    }
}
