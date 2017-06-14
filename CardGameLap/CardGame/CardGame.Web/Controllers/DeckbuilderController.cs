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
        [HttpGet]
        [Authorize(Roles = "player,admin")]
        public ActionResult Index()
        {
            /// Decks aus der Datenbank
            var dbDecks = UserManager.GetAllDecksByEmail(User.Identity.Name);

            /// Decks für die View
            var decks = new List<Models.Deck>();
            foreach (var d in dbDecks)
            {
                Models.Deck deck = new Models.Deck();
                deck.ID = d.ID;
                deck.Name = d.Name;
                decks.Add(deck);
            }

            return View(decks);
        }

        //[HttpGet]
        //[Authorize(Roles = "player,admin")]
        //public ActionResult DeckDetails(int id)
        //{
        //    /// Objekt für den Deckbuilder
        //    ///     bestehend aus Cards IM Deck
        //    ///     und aus Cards FÜR das Deck
        //    var model = new Deckbuilder();

        //    /// Karten IM Deck
        //    var dbDeckCards = DeckManager.GetDeckCardsById(id);
        //    if (dbDeckCards != null)
        //    {
        //        foreach (var cc in dbDeckCards)
        //        {
        //            Card card = new Card();
        //            card.ID = cc.ID;
        //            card.Attack = cc.Attack;
        //            card.Name = cc.Name;
        //            card.Life = cc.Life;
        //            card.Mana = cc.Mana;
        //            //card.Type = UserManager.CardTypeNames[cc.fkCardType ?? 0];

        //            model.Deck.Add(card);
        //        }
        //    }

        //    var dbUserCards = CardManager.GetAllCardsForDeck(User.Identity.Name, id);

        //    if (dbUserCards != null)
        //    {
        //        foreach (var cc in dbUserCards)
        //        {
        //            Card card = new Card();
        //            card.ID = cc.ID;
        //            card.Attack = cc.Attack;
        //            card.Name = cc.Name;
        //            card.Life = cc.Life;
        //            card.Mana = cc.Mana;
        //            //card.Type = UserManager.CardTypeNames[cc.fkCardType ?? 0];

        //            model.Collection.Add(card);
        //        }
        //    }

        //    return View(model);
        //}


        /// <summary>
        /// ActionResult EditDeck fürs Editieren meine gewählten Decks 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult EditDeck(int id)
        {
            /// Erstelle ein Objekt das während der Bearbeitung 
            /// die karten beinhaltet
            Deckbuilder db = new Deckbuilder();
            // speichere dort auch die ID des Decks
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
                card.Pic = cc.Pic;
                //card.Type = UserManager.CardTypeNames[cc.fkCardType ?? 0];
                //card.Type = card.Type == "Minion" ? "M" : card.Type == "Spell" ? "S" : "W";

                db.Collection.Add(card);
            }

            foreach (var deckCard in db.Deck)
            {
                int idx = db.Collection.FindIndex(i => i.Name == deckCard.Name);
                db.Collection.RemoveAt(idx);
            }

            TempData["DeckBuilder"] = db;
            return View(db);
        }


        /// <summary>
        /// Verändert das DeckBuilder Objekt im TempData
        /// ACHTUNG es wurde NOCH NICHTS gespeichert
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult AddCardToDeck(int id)
        {
            /// Übergabeparameter id ist eigentlich INDEX!!! Wichtig ......
            
            Web.Models.Deckbuilder db = new Web.Models.Deckbuilder();
            db = (Deckbuilder)TempData["DeckBuilder"];

            Web.Models.Card card = db.Collection[id];
            db.Collection.RemoveAt(id);

            db.Deck.Add(card);          

            TempData["DeckBuilder"] = db;
            return View("EditDeck", db);
        }


        /// <summary>
        /// Verändert das DeckBuilder Objekt im TempData
        /// ACHTUNG es wurde NOCH NICHTS gespeichert
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult RemoveCardFromDeck(int id)
        {
            /// id ist eigentlich INDEX

            Web.Models.Deckbuilder db = new Web.Models.Deckbuilder();
            db = (Deckbuilder)TempData["DeckBuilder"];

            Web.Models.Card card = db.Deck[id];
            db.Deck.RemoveAt(id);

            db.Collection.Add(card);

            TempData["DeckBuilder"] = db;
            return View("EditDeck", db);
        }


        /// <summary>
        /// ActionResult SaveDeck speichert das gebaute DECK in Datenbank !!!!
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult SaveDeck(int id)
        {
            /// hole dir den DeckBuilder aus TempData raus
            Web.Models.Deckbuilder db = (Deckbuilder)TempData["DeckBuilder"];

            /// Ermittle die Karten die endgültig
            /// für dieses Deck gespeichert werden sollen
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
                    dbCard.Pic = c.Pic;
                    //hier einfach nur die ID befuellen (muss DB id sein)
                    //im Manager dann die ID raussuchen und die db hinzufuegen
                    var dbDeckCard = new CardGame.DAL.Model.DeckCard();
                    dbDeckCard.Card = dbCard;
                    dbDeckCard.NumCards = 1;
                    //dbDeckCard.tblDeck = DeckManager.GetDeckById(id);

                    dbDeckList.Add(dbDeckCard);
                }
            }

            /// gehe über DeckManager.UpdateDeckById in die Datenbank und speichere die Karten für dieses Deck
            var result = DeckManager.UpdateDeckById(id, dbDeckList); 

            return RedirectToAction("Index");
        }
    }
}