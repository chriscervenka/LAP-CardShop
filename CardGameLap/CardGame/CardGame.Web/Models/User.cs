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


        public string Salt { get; set; }

        
        public string Role { get; set; }
    }
}