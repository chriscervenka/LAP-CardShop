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

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Gamertag { get; set; }

        public string Salt { get; set; }

        public string Role { get; set; }

        public int CurrencyBalance { get; set; }

        public string Adresse { get; set; }

        public string Ort { get; set; }

        public string PLZ { get; set; }

        //public DateTime Registrierungsdatum { get; set; }
    }
}