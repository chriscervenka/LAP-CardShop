using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CardGame.Web.Models
{
    public class Cart
    {
        public int ID { get; set; }

        public string PackName { get; set; }

        public int CardQuantity { get; set; }

        public decimal PackPrice { get; set; }

        public List<Packages> CardPacks { get; set; }
    }
}