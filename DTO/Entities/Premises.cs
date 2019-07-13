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

        public DateTime CreatedDate { get; set; }

        public virtual PremisesType PremisesType { get; set; }

        public virtual ICollection<DistributorFood> DistributorFoods { get; set; }
        public virtual ICollection<Food> ProviderFoods { get; set; }
        public virtual ICollection<Food> FarmFoods { get; set; }
        public virtual ICollection<Transaction> ProviderTransactions { get; set; }
        public virtual ICollection<Transaction> FarmTransactions { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
