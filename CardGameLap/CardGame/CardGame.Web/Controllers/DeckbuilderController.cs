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
        //public ActionResult Deckbuilder()

        //{
        //    List<Deckbuilder> CardList = new List<Deckbuilder>();

        //    var dbCardlist = CardManager.GetAllCardsFromDeck(1);

        //    foreach (var c in dbCardlist)
        //    {
        //        Deckbuilder deck = new Deckbuilder();
        //        deck.ID = c.ID;
        //        deck.N = c.Name;
        //        deck.Life = c.Life;
        //        deck.Mana = c.Mana;
        //        deck.Pic = c.Pic;
        //        deck.Attack = c.Attack;
                
        //        CardList.Add(deck);
        //    }
        //    return View(CardList);
        //}


        public ActionResult EditDeck(string Email)
        {
            Deckbuilder db = new Deckbuilder();
            var id = UserManager.GetPersonByEmail(Email).ID;
            db.ID = id;
            var dbDeckCards = DeckManager.GetDeckCardsById(id);

            foreach (var cc in dbDeckCards)
            {
                Web.Models.Card card = new Web.Models.Card();
                card.ID = cc.ID;
                card.Attack = cc.Attack;
                card.Name = cc.Name;
                card.Life = cc.Life;
                card.Mana = cc.Mana;
                card.Pic = cc.Pic;
                //card.Type = UserManager.CardTypeNames[cc.fkCardType ?? 0];
                //card.Type = card.Type == "Minion" ? "M" : card.Type == "Spell" ? "S" : "W";

                db.Deck.Add(card);
            }

            var dbCardList = UserManager.GetAllCardsByEmail(User.Identity.Name);

            foreach (var cc in dbCardList)
            {
                Web.Models.Card card = new Web.Models.Card();
                card.ID = cc.ID;
                card.Attack = cc.Attack;
                card.Name = cc.Name;
                card.Life = cc.Life;
                card.Mana = cc.Mana;
                //card.Type = UserManager.CardTypeNames[cc.fkCardType ?? 0];
                //card.Type = card.Type == "Minion" ? "M" : card.Type == "Spell" ? "S" : "W";

                db.Collection.Add(card);
            }

            foreach (var deckCard in db.Deck)
            {
                int idx = db.Collection.FindIndex(i => i.Name == deckCard.Name);
                db.Collection.RemoveAt(idx);
            }

            db.Collection.Sort();
            db.Deck.Sort();

            TempData["DeckBuilder"] = db;
            return View(db);
        }


        public ActionResult AddCardToDeck(int id)
        {
            Web.Models.Deckbuilder db = new Web.Models.Deckbuilder();
            db = (Deckbuilder)TempData["DeckBuilder"];

            Web.Models.Card card = db.Collection[id];
            db.Collection.RemoveAt(id);
            db.Collection.Sort();

            db.Deck.Add(card);
            db.Deck.Sort();           

            TempData["DeckBuilder"] = db;
            return View("EditDeck", db);
        }


        public ActionResult RemoveCardFromDeck(int id)
        {
            Web.Models.Deckbuilder db = new Web.Models.Deckbuilder();
            db = (Deckbuilder)TempData["DeckBuilder"];

            Web.Models.Card card = db.Deck[id];
            db.Deck.RemoveAt(id);
            db.Deck.Sort();

            db.Collection.Add(card);
            db.Collection.Sort();
            
            TempData["DeckBuilder"] = db;
            return View("EditDeck", db);
        }


        public ActionResult SaveDeck(int id)
        {
            Web.Models.Deckbuilder db = new Web.Models.Deckbuilder();
            db = (Deckbuilder)TempData["DeckBuilder"];

            var dbDeckList = new List<DeckCard>();

            foreach (var c in db.Deck)
            {
                int idx = dbDeckList.FindIndex(i => i.ID == c.ID);

                if (idx >= 0)
                {
                    dbDeckList[idx].NumCards += 1;
                }
                else
                {
                    var dbCard = new CardGame.DAL.Model.Card();
                    dbCard.Attack = c.Attack;
                    dbCard.Name = c.Name;
                    dbCard.ID = c.ID;
                    dbCard.Life = c.Life;
                    dbCard.Mana = c.Mana;
                    //hier einfach nur die ID befuellen (muss DB id sein)
                    //im manager dann die ID raussuchen und die db hinzufuegen
                    var dbDeckCard = new CardGame.DAL.Model.DeckCard();
                    dbDeckCard.Card = dbCard;
                    dbDeckCard.NumCards = 1;
                    //dbDeckCard.tblDeck = DeckManager.GetDeckById(id);

                    dbDeckList.Add(dbDeckCard);
                }
            }

            var result = DeckManager.UpdateDeckById(id, dbDeckList); //Macht irgendwas, aber irgendwas falsches!

            //Neue DeckCollection dem DeckManager geben

            return RedirectToAction("DeckOverview", "Profile");
        }
    }
}