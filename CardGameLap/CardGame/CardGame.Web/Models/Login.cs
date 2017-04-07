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
        [Required]
        [DataType(DataType.EmailAddress)]
        [StringLength(128)]
        [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)Z", ErrorMessage = "Invalid Email")]
        
        public string Email { get; set; }



        //  TODO PASSWORD Validation funktioniert noch nicht
        [Required]
        [DataType(DataType.Password)]
        [StringLength(10, MinimumLength = 6)]        
        public string Password { get; set; }
    }
}