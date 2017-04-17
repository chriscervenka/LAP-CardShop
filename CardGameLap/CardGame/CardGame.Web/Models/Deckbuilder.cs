using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CardGame.Web.Models
{
    public class Deckbuilder : Card
    {
        [Key]
        public new int ID { get; set; }

        public List<Card> deck { get; set; }

        public List<Card> collection { get; set; }

        public Deckbuilder()
        {
            deck = new List<Card>();
            collection = new List<Card>();
        }
    }
}