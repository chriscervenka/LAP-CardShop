using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CardGame.DAL.Logic;
using CardGame.DAL.Model;
using CardGame.Web.Models;

namespace CardGame.Web.Controllers
{
    public class DeckbuilderController : Controller
    {
        // GET: Deckbuilder
        public ActionResult Deckbuilder()

        {
            List<Deckbuilder> CardList = new List<Deckbuilder>();

            var dbCardlist = CardManager.GetAllCardsFromDeck(1);

            foreach (var c in dbCardlist)
            {
                Deckbuilder deck = new Deckbuilder();
                deck.ID = c.idcard;
                deck.Name = c.cardname;
                deck.Life = c.life;
                deck.Mana = c.mana;
                deck.Pic = c.pic;
                deck.Attack = c.attack;


                //card.Type = c.tbltype.typename;
                //card.Type = CardManager.GetCardTypeById(c.fktype);
                //card.Type = CardManager.CardTypes[c.fktype];
                

                CardList.Add(deck);
            }

            return View(CardList);
        }
    }
}