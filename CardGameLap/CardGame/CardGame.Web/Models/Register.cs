using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CardGame.Web.Models
{
    public class Register : Login
    {
        [Required(ErrorMessage = "Your Firstname is required")]
        [StringLength(50)]
        [DisplayName("First Name")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Your Lastname is required")]
        [StringLength(50)]
        [DisplayName("Last Name")]
        public string Lastname { get; set; }

        [Required]
        [MaxLength(10, ErrorMessage = "Gamertag must be 10 characters or less ..."), MinLength(2)]
        public string Gamertag { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="Please confirm your password ...")]
        public string ConfirmPassword { get; set; }
    }
}