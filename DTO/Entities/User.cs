using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DTO.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        public string Username { get; set; }

        public string Fullname { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string PhoneNo { get; set; }

        public int RoleId { get; set; }

        public virtual Role Role { get; set; }

        public bool IsActive { get; set; }

        public string Salt { get; set; }

        public DateTime CreatedDate { get; set; }

        public ICollection<Material> farmerMateials;
        public ICollection<Material> distributeMateials;
        public ICollection<Material> mateialsCreateBy;

    }
}