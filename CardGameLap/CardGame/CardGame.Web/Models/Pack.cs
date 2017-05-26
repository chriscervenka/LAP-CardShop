using CardGame.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace CardGame.Web.Models
{
    public class Pack
    {
        
        public int ID { get; set; }

        public string Packname { get; set; }

        public int CardQuantity { get; set; }

        public decimal Packprice { get; set; }

        public bool IsMoney { get; set; }

        public int DiamondValue { get; set; }

        //public List<Packages> CardPackages { get; set; }
    }
}