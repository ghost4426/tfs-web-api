using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Models.FoodData
{
    public class Treatment
    {
        public DateTime TreatmentDate { get; set; }

        public List<string> TreatmentProcess { get; set; }
    }
}
