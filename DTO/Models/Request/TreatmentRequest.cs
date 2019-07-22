using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Models
{
   public class CreateTreatmentRequest
    {
        public string Name { get; set; }

        public List<string> TreatmentProcess { get; set; }
    }

    public class CreateMoreTreatmentRequest
    {
        public List<string> TreatmentProcess { get; set; }
    }
}
