using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Models
{
    public class ProductReponses
    {

        public class GetAllProduct
        {
            public IList<Products> ListProduct { get; set; }
        }
    }
}
