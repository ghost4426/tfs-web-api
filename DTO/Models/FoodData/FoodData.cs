using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Models.FoodData
{
    public class FoodData
    {
        public int FoodId { get; set; }

        public string Breed { get; set; }

        //Pork, Chicken, Beef
        public string Category { get; set; }

        public Farm Farm { get; set; }

        public List<Provider> Providers { get; set; }

        public List<Distributor> Distributors { get; set; }

        public DateTime StartedDate { get; set; }

    }
}
