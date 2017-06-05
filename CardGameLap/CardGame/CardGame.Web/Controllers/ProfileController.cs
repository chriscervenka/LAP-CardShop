using CardGame.DAL.Logic;
using CardGame.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CardGame.Web.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile
        [HttpGet]
        [Authorize(Roles = "player,admin")]
        public ActionResult Index()
        {
            UserProfile profile = new UserProfile();

            var dbUser = UserManager.GetPersonByEmail(User.Identity.Name);

            profile.Currency = dbUser.Currencybalance ?? 0;
            profile.Email = dbUser.Email;
            profile.Firstname = dbUser.Firstname;
            profile.Lastname = dbUser.Lastname;

            profile.NumDistinctCardsOwned = UserManager.GetNumDistinctCardsOwnedByEmail(User.Identity.Name);
            profile.NumDecksOwned = UserManager.GetNumDecksOwnedByEmail(User.Identity.Name);

            profile.NumTotalCardsOwned = UserManager.GetNumTotalCardsOwnedByEmail(User.Identity.Name);

            return View(profile);
        }


        [HttpGet]
        [Authorize(Roles = "player,admin")]
        public ActionResult CardCollection()
        {
            var cardCollection = new List<Card>();

            var dbCardList = UserManager.GetAllCardsByEmail(User.Identity.Name);

            foreach (var cc in dbCardList)
            {
                Card card = new Card();
                card.ID = cc.ID;
                card.Attack = cc.Attack;
                card.Name = cc.Name;
                card.Life = cc.Life;
                card.Mana = cc.Mana;
                //card.Type = UserManager.CardTypeNames[cc.fkCardType ?? 0];

                cardCollection.Add(card);
            }

            return View(cardCollection);
        }

        [HttpGet]
        [Authorize(Roles = "player,admin")]
        public ActionResult DeckOverview()
        {
            var decks = new List<Deck>();

            var dbDecks = UserManager.GetAllDecksByEmail(User.Identity.Name);

            foreach (var d in dbDecks)
            {
                Deck deck = new Deck();
                deck.ID = d.ID;
                deck.Name = d.Name;
                decks.Add(deck);
            }

            return View(decks);
        }

        [HttpGet]
        [Authorize(Roles = "player,admin")]
        public ActionResult DeckDetails(int id)
        {
            var deckCards = new List<Card>();

            var dbDeckCards = DeckManager.GetDeckCardsById(id);

            foreach (var cc in dbDeckCards)
            {
                Card card = new Card();
                card.ID = cc.ID;
                card.Attack = cc.Attack;
                card.Name = cc.Name;
                card.Life = cc.Life;
                card.Mana = cc.Mana;
                //card.Type = UserManager.CardTypeNames[cc.fkCardType ?? 0];

                deckCards.Add(card);
            }

            return View(deckCards);
        }
    }
}