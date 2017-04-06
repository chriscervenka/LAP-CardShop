using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Resources;
using System.Web;
using System.Web.Security;

namespace CardGame.Web.Models
{
    public class User
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Your Firstname is required")]
        [StringLength(50)]
        [DisplayName("First Name")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Your Lastname is required")]
        [StringLength(50)]
        [DisplayName("Last Name")]
        public string Lastname { get; set; }

        [DataType(DataType.EmailAddress)]
        [StringLength(128)]
        [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)Z", ErrorMessage = "Invalid Email")]
        [Required]
        public string Email { get; set; }



        //  TODO PASSWORD Validation funktioniert nicht
        [DataType(DataType.Password)]
        [StringLength(255, MinimumLength = 8)]
        [Required]
        [MembershipPassword()]
        public string Password { get; set; }

        [Compare("Password")]
        [DataType(DataType.Password)]
        [StringLength(10, MinimumLength = 5)]
        [Required]
        [MembershipPassword]
        public string ConfirmPassword { get; set; }

        [Required]
        [MaxLength(10, ErrorMessage = "Gamertag must be 10 characters or less"), MinLength(2)]
        public string Gamertag { get; set; }

        [Required]
        public string Salt { get; set; }

        [Required]
        public string Role { get; set; }
    }
}