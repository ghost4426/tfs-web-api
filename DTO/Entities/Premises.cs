using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Entities
{
    public class Premises
    {
        public int PremisesId { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public int TypeId { get; set; }

        public virtual PremisesType PremisesType { get; set; }

        public DistributorFood DistributorFood { get; set; }
    }
}
