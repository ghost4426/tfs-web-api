using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public virtual Categories Categories { get; set; }

        public virtual User Provider { get; set; }

        public virtual User Distributor { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
