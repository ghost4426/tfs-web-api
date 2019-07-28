using DTO.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Models
{
    public class FoodRespone
    {

        public class FoodsRespone
        {
            public IList<Food> Foods { get; set; }
        }

        public class TreatmentReponse
        {
            public int TreatmentId { get; set; }
            public string Name { get; set; }
            public int TreatmentParentId { get; set; }
        }
    }
}
