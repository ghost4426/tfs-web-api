using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DTO.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Fullname { get; set; }

        [Required]
        public string Password { get; set; }

        public DateTime? Dob { get; set; }

        [Required]
        public string Salt { get; set; }

        [Required]
        public string Email { get; set; }

        public string ActivationCode { get; set; }

        public bool? IsConfirmEmail { get; set; }

        public string PhoneNo { get; set; }

        public string Image { get; set; }

        [ForeignKey("Role")]
        public int RoleId { get; set; }

        [ForeignKey("Premises")]
        public int? PremisesId { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreateDate { get; set; }

        public Role Role { get; set; }
        public Premises Premises { get; set; }

        public ICollection<FoodDetail> UserCreateFoodDetails { get; set; }
        public ICollection<Transaction> UserCreateTransactions { get; set; }
        public ICollection<Transaction> VeterinaryTransactions { get; set; }
        public ICollection<Transaction> RejectByTransactions { get; set; }
        public ICollection<Food> UserCreatedFoods { get; set; }
        public ICollection<Treatment> UserCreatedTreatments { get; set; }
        public ICollection<Feeding> UserCreatedFeedings { get; set; }
        public ICollection<Vaccin> UserCreatedVaccins { get; set; }
        public ICollection<Treatment> UserUpdateTreatments { get; set; }
        public ICollection<Feeding> UserUpdateFeedings { get; set; }
        public ICollection<Vaccin> UserUpdateVaccins { get; set; }
    }
}