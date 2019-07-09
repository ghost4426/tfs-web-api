using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DTO.Entities
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CategoryId { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Food> Foods { get; set; }
    }
}
