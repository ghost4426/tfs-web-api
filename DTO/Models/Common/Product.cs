using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Models.Common
{
    class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int CategoriesId { get; set; }

        public int ProviderUserId { get; set; }
    }
}
