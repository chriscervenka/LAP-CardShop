using CardGame.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CardGame.Web.Models
{
    public class ShopContainer
    {
        public Shop shop { get; set; }

        public List<Card> generatedCards { get; set; }
    }
}