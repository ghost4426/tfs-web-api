using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Models
{
    public class PremisesReponse
    {
        public int PremisesId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string TypeName { get; set; } 
        public DateTime CreateDate { get; set; }
        public bool IsActive { get; set; }
    }
}
