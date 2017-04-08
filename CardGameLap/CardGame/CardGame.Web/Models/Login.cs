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
        [Required(ErrorMessage = "E-Mail is required")]
        [RegularExpression(@"^([0-9a-zA-Z]([\+\-_\.][0-9a-zA-Z]+)*)+@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,3})$", ErrorMessage = "Please enter a valid email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }



        //  TODO PASSWORD Validation funktioniert noch nicht
        [Required(ErrorMessage = "Please provide Password", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Password must be min 6 char long")]
        public string Password { get; set; }
    }
}