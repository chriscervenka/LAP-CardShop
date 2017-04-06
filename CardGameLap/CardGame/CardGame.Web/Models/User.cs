using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CardGame.Web.Models
{
    public class User
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Your Firstname is required")]
        [StringLength(50)]
        [DisplayName("First Name")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Your Firstname is required")]
        [StringLength(50)]
        [DisplayName("First Name")]
        public string Lastname { get; set; }

        [Required]

        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [MaxLength(8, ErrorMessage = "Gamertag must be 10 characters or less"), MinLength(3)]
        public string Gamertag { get; set; }

        [Required]
        public string Salt { get; set; }

        [Required]
        public string Role { get; set; }
    }
}