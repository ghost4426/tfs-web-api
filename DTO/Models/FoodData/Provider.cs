using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Models.FoodData
{
   public class Provider
    {
        public int ProviderId { get; set; }

        public string Name { get; set; }

        public DateTime ReceivedDate { get; set; }

        public Treatment Treatment { get; set; }

        public DateTime PackagingDate { get; set; }

        public DateTime ProvideDate { get; set; }
    }
}
