using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DTO.Entities
{
   public class PremisesType
    {
        [Key]
        public int TypeId { get; set; }

        public int Name { get; set; }
    }
}
