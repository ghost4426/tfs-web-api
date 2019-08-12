using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DTO.Entities
{
   public class Feeding
    {
        [Key]
        public int FeedingId { get; set; }

        [Required]
        public string Name { get; set; }

        [ForeignKey("Premises")]
        public int PremisesId { get; set; }

        public DateTime CreateDate { get; set; }

        [ForeignKey("CreateBy")]
        public int CreateById { get; set; }

        public DateTime UpdateDate { get; set; }

        [ForeignKey("UpdateBy")]
        public int UpdateById { get; set; }

        public Premises Premises { get; set; }
        public User CreateBy { get; set; }
        public User UpdateBy { get; set; }

        public bool IsDelete { get; set; }

        public ICollection<FeedingFood> FeedingFoods { get; set; }
    }
}
