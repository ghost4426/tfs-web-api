﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Models.FoodData
{
    public class FoodData
    {
        public int Id { get; set; }

        //Pork, Chicken, Beef
        public string Category { get; set; }

        public Farm Farm { get; set; }

        public Provider Provider { get; set; }

        public Distributor Distributor { get; set; }

       
    }
}
