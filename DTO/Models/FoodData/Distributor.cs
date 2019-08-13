using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Models.FoodData
{
   public class Distributor
    {
        public int DistributorId { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public DateTime ReceivedDate { get; set; }
    }
}
