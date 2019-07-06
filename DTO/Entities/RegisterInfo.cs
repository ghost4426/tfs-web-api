using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DTO.Entities
{
   public class RegisterInfo
    {
        [Key]
        public int RegisterId { get; set; }

        [Required]
        public string PremisesName { get; set; }

        [Required]
        public string PremisesAddress { get; set; }

        [ForeignKey("PremisesType")]
        public int PremisesTypeId { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Fullname { get; set; }

        [Required]
        public string Email { get; set; }

        public string Phone { get; set; }

        public bool? IsConfirm { get; set; }

        public DateTime CreatedDate { get; set; }

        public PremisesType PremisesType { get; set; }
    }
}
