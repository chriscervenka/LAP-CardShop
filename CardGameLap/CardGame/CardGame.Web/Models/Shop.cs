using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CardGame.Web.Models
{
    public class Shop
    {
        public List<Packages> cardPacks { get; set; }

        public Order order { get; set; }
    }
}