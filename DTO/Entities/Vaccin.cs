using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DTO.Entities
{
    public class Vaccin
    {
        [Key]
        public int VaccinId { get; set; }

        [Required]
        public string Name { get; set; }

        [ForeignKey("Premises")]
        public int PremisesId { get; set; }

        public DateTime CreateDate { get; set; }

        [ForeignKey("CreateBy")]
        public int CreateById { get; set; }

        public DateTime UpdateDate { get; set; }

        [ForeignKey("UpdateBy")]
        public int UpdateById { get; set; }

        public Premises Premises { get; set; }
        public User CreateBy { get; set; }
        public User UpdateBy { get; set; }

        public ICollection<VaccinFood> VaccinFoods { get; set; }
    }
}
