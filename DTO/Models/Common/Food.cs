using DTO.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Models
{
    public class Food
    {
        public int CategoryId { get; set; }

        public string Breed { get; set; }

        public int FarmerId { get; set; }
    }

    public class FoodFarm
    {
        public int FoodId { get; set; }
        public string CategoryName { get; set; }
        public string Breed { get; set; }
        public int CategoryId { get; set; }
        public int FarmId { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public  class FoodProvider
    {
        public int FoodId { get; set; }
        public string CategoryName { get; set; }
        public string Breed { get; set; }
        public int CategoryId { get; set; }
        public int ProviderId { get; set; }
        public int FarmId { get; set; }
        public string FarmName { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
