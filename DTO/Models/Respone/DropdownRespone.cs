using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Models
{
   public class DropdownRespone
    {
        public IList<Option> Results { get; set; }
    }

    public class Option
    {
        public int id { get; set; }
        public string text { get; set; }
    }
}
