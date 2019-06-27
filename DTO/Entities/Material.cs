using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DTO.Entities
{
    public class Material
    {
        [Key]
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public virtual MaterialCategories Categories { get; set; }

        [ForeignKey("Farmer")]
        public int FarmerId { get; set; }
        public User Farmer { get; set; }

        [ForeignKey("Provider")]
        public int? ProviderId { get; set; }

        public User Provider { get; set; }

        [ForeignKey("CreatedBy")]
        public int CreatedById { get; set; }

        public User CreatedBy { get; set; }
        public string MaterialName { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
