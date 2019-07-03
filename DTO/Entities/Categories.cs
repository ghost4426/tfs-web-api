using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DTO.Entities
{
    public class Categories
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
