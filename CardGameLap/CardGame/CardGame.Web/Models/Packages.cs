using CardGame.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace CardGame.Web.Models
{
    public class Packages
    {
        [Key]
        public int Idpack { get; set; }

        public string Packname { get; set; }

        public int CardQuantity { get; set; }

        public decimal Packprice { get; set; }

        public List<Card> Packlist { get; set; }
    }
}