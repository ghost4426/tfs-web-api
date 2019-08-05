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

        [ForeignKey("CreateBy")]
        public int CreateById { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime UpdateDate { get; set; }

        [ForeignKey("UpdateBy")]
        public int UpdateById { get; set; }

        public Premises Premises { get; set; }
        public Treatment TreatmentParent { get; set; }
        public User CreateBy { get; set; }
        public User UpdateBy { get; set; }

        public ICollection<ProviderFood> ProviderFoods { get; set; }
        public ICollection<Treatment> Treatments { get; set; }
    }
}
