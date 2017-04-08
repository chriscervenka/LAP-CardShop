using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Security;

namespace CardGame.Web.Models
{
    public class Login : User
    {
        [Required(ErrorMessage = "E - Mail is required")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Please enter a valid email")]
        public string Email { get; set; }



        //  TODO PASSWORD Validation funktioniert noch nicht
        [Required(ErrorMessage = "Please enter your password")]
        [DataType(DataType.Password)]
        [StringLength(10, MinimumLength = 6, ErrorMessage = "Password must be min 6 char long")]
        public string Password { get; set; }
    }
}