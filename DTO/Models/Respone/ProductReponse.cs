using DTO.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Models
{
    public class ProductReponse
    {
        public class ProductGetAllReponse{
            public IList<Food> ListProduct { get; set; }
        }

        public class ProductGetByProviderReponse
        {
            public IList<Food> ListProduct { get; set; }
        }

        public class CreateProductReponse
        {
            public int ProductId { get; set; }
        }

        public class FoodGetByFarm
        {
            public IList<Food> ListProduct { get; set; }
        }
    }
}
