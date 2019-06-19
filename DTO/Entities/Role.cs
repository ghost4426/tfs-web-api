using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DTO.Entities
{
   public class Role
    {
        [Key]
        public int RoleId { get; set; }

        public string Name { get; set; }
    }
}
