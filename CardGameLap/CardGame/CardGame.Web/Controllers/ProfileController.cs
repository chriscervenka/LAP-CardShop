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
                card.Pic = cc.Pic;
                //card.Type = UserManager.CardTypeNames[cc.fkCardType ?? 0];

                cardCollection.Add(card);
            }

            return View(cardCollection);
        }


        /// <summary>
        /// LAP Donate ActionMethod
        /// </summary>
        /// <returns>View</returns>
        [HttpPost]
        [Authorize(Roles = "player,admin")]
        public ActionResult Donate()
        {
            var currencyBalance = UserManager.GetCurrencyBalanceByEmail(User.Identity.Name);
            
            return View("Index");
        }


    }
}