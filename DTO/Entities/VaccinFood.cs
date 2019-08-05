using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DTO.Entities
{
   public class VaccinFood
    {
        [ForeignKey("Vaccin")]
        public int VaccinId { get; set; }

        [ForeignKey("Food")]
        public int FoodId { get; set; }

        public DateTime CreateDate { get; set; }

        public Vaccin Vaccin { get; set; }
        public Food Food { get; set; }
    }
}
