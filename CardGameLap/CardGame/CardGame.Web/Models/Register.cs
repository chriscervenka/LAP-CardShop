using Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace CardGame.Web.Models
{
    public class Register : Login
    {
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = Constants.Validation.REQUIRED)]
        [StringLength(50, ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = Constants.Validation.MAX_LENGTH)]
        [Display(Name = Constants.Labels.FIRSTNAME, ResourceType = typeof(Labels))]
        //[RegularExpression(@"^([0-9a-zA-Z]([\+\-_\.][0-9a-zA-Z]+)*)+@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,3})$", ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = Constants.Validation.SPECIAL_CHARACTER)]
        public new string Firstname { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = Constants.Validation.REQUIRED)]
        [StringLength(50, ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = Constants.Validation.MAX_LENGTH)]
        [Display(Name = Constants.Labels.LASTNAME, ResourceType = typeof(Labels))]
        //[RegularExpression(@"^([0-9a-zA-Z]([\+\-_\.][0-9a-zA-Z]+)*)+@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,3})$", ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = Constants.Validation.SPECIAL_CHARACTER)]
        public new string Lastname { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = Constants.Validation.REQUIRED)]
        [StringLength(50, ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = Constants.Validation.MAX_LENGTH)]
        [Display(Name = Constants.Labels.GAMERTAG, ResourceType = typeof(Labels))]
        public new string Gamertag { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = Constants.Validation.REQUIRED)]
        [StringLength(maximumLength:20, MinimumLength = 6, ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = Constants.Validation.LENGTH)]
        [Display(Name = Constants.Labels.CONFIRMATION, ResourceType = typeof(Labels))]
        public string ConfirmPassword { get; set; }


        [Required]
        public new string Adresse { get; set; }

        [Required]
        public string Hausnummer { get; set; }

        [Required]
        public new string Ort { get; set; }

        [Required]
        public new string PLZ { get; set; }


        public class RegisterDbContext : DbContext
        {
            public DbSet<User> Person { get; set; }
        }
    }
}