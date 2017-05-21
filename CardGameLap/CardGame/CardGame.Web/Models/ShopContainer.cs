using CardGame.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CardGame.Web.Models
{
    public class ShopContainer
    {
        public Shop Shop { get; set; }

        public List<Card> GeneratedCards { get; set; }
    }
}