using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DTO.Entities
{
   public class VaccineFood
    {
        [Key]
        public int VaccineFoodId { get; set; }

        [ForeignKey("Vaccine")]
        public int VaccineId { get; set; }

        [ForeignKey("Food")]
        public int FoodId { get; set; }

        public DateTime VaccineDate { get; set; }

        public DateTime CreateDate { get; set; }

        public Vaccine Vaccine { get; set; }
        public Food Food { get; set; }
    }
}
