using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DTO.Entities
{
   public class PremisesType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TypeId { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Premises> Premises { get; set; }
        //public ICollection<RegisterInfo> RegisterInfos { get; set; }
    }
}
