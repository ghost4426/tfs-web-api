using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Models.FoodData
{
   public class Provider
    {
        public int ProviderId { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public DateTime ReceivedDate { get; set; }

        public Treatments Treatment { get; set; }

        public Packaging Packaging { get; set; }

        public DateTime ProvideDate { get; set; }
        public DateTime CertificationDate { get; set; }
        public string CertificationNumber { get; set; }
    }
}
