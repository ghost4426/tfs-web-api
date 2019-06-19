using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DTO.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int CategoriesId { get; set; }

        public virtual Categories Categories { get; set; }

        public int ProviderUserId { get; set; }

        public virtual User Provider { get; set; }

        public int? DistributorUserId { get; set; }

        public virtual User Distributor { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
