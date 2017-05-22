using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CardGame.Web.Models
{
    public class Deckbuilder : Card
    {
        public new int ID { get; set; }

        public List<Card> Deck { get; set; }

        public List<Card> Collection { get; set; }

        public Deckbuilder()
        {
            Deck = new List<Card>();
            Collection = new List<Card>();
        }
    }
}