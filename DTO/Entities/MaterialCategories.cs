﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DTO.Entities
{
    public class MaterialCategories
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
