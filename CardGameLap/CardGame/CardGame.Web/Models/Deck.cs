using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CardGame.Web.Models
{
    public class Deck
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public List<Card> Cards { get; set; }
    }
}