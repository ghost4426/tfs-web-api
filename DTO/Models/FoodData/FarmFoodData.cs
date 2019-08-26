﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Models.FoodData
{
    class FarmFoodData
    {
        public int FarmId { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public List<string> Feedings { get; set; }

        public List<VaccineData> Vaccinations { get; set; }

        public DateTime CertificationDate { get; set; }

        public string CertificationNumber { get; set; }

        public DateTime FoodSentDate { get; set; }
    }
}
