using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DTO.Entities
{
    public class Premises
    {
        [Key]
        public int PremisesId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [ForeignKey("PremisesType")]
        public int TypeId { get; set; }

        public DateTime CreateDate { get; set; }

        public PremisesType PremisesType { get; set; }

        public ICollection<DistributorFood> DistributorFoods { get; set; }
        public ICollection<ProviderFood> ProviderFoods { get; set; }
        public ICollection<Food> FarmFoods { get; set; }
        public ICollection<Transaction> ProviderTransactions { get; set; }
        public ICollection<Transaction> FarmTransactions { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
