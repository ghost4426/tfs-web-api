using DTO.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Models
{
    public class ProductReponse
    {
        public class ProductGetAllReponse{
            public IList<Product> ListProduct { get; set; }
        }

        public class ProductGetByProviderReponse
        {
            public IList<Product> ListProduct { get; set; }
        }

        public class CreateProductReponse
        {
            public int ProductId { get; set; }
        }
    }
}
