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
        [Required(ErrorMessage = "Your Firstname is required", AllowEmptyStrings = false)]
        [StringLength(50)]
        [DisplayName("First Name")]
        public new string Firstname { get; set; }

        [Required(ErrorMessage = "Your Lastname is required", AllowEmptyStrings = false)]
        [StringLength(50)]
        [DisplayName("Last Name")]
        public new string Lastname { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Gamertag can be 20 characters or less ..."), MinLength(2)]
        public new string Gamertag { get; set; }

        
        [Compare("Password", ErrorMessage ="Please confirm your password ...")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}