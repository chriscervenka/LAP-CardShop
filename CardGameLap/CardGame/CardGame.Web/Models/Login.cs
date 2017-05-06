using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Security;
using Resources;

namespace CardGame.Web.Models
{
    public class Login : User
    {
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = Constants.Validation.REQUIRED)]
        [StringLength(50, ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = Constants.Validation.MAX_LENGTH)]
        [Display(Name = Constants.Labels.EMAIL, ResourceType = typeof(Labels))]
        [RegularExpression(".+@.+\\..+", ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = Constants.Validation.EMAIL)]
        public new string Email { get; set; }



        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = Constants.Validation.REQUIRED)]
        [StringLength(maximumLength: 20, MinimumLength = 6, ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = Constants.Validation.LENGTH)]
        [Display(Name = Constants.Labels.PASSWORD, ResourceType = typeof(Labels))]
        public new string Password { get; set; }
    }
}