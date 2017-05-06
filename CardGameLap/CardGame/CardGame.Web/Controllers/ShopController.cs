using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CardGame.DAL.Logic;
using CardGame.Web.Models;
using CardGame.DAL.Model;
using CardGame.Log;

namespace CardGame.Web.Controllers
{
    public class ShopController : Controller
    {
        /// <summary>
        /// Methode AllCardPacks() gibt mir alle in die Datenbank eingetragene PACKS zurück 
        /// </summary>
        /// <returns>return View("ShopStart", cart)</returns>
        [HttpGet]
        [Authorize]
        public ActionResult ShopStart()
        {
            Cart cart = new Cart();
            cart.CardPacks = new List<Packages>();

            var dbCardPacks = ShopManager.AllCardPacks();

            foreach (var dbCp in dbCardPacks)
            {
                Packages cardPack = new Packages();
                cardPack.Idpack = dbCp.ID;
                cardPack.Packname = dbCp.Name;
                //GetValueOrDefault METHODE zur Konvertierung eingefügt wegen DATENTYP decimal
                cardPack.CardQuantity = dbCp.NumberOfCards.GetValueOrDefault();
                cardPack.Packprice = dbCp.Price.GetValueOrDefault();
                cart.CardPacks.Add(cardPack);
            }

            return View("ShopStart", cart);
        }            


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public ActionResult Shop()
        {
            ShopContainer sc = new ShopContainer();

            sc.shop = new Shop();
            sc.shop.CardPacks = new List<Packages>();
            sc.shop.Order = new Models.Order();
            sc.shop.Order.UserBalance = UserManager.GetCurrencyBalanceByEmail(User.Identity.Name);

            var dbCardPacks = ShopManager.AllCardPacks();

            foreach (var dbCp in dbCardPacks)
            {
                Packages cardPack = new Packages();
                cardPack.Idpack = dbCp.ID;
                cardPack.Packname = dbCp.Name;
                //GetValueOrDefault METHODE zur Konvertierung eingefügt wegen DATENTYP decimal
                cardPack.CardQuantity = dbCp.NumberOfCards.GetValueOrDefault();
                cardPack.Packprice = dbCp.Price.GetValueOrDefault();
                sc.shop.CardPacks.Add(cardPack);
            }

            sc.generatedCards = GeneratedCards();

            return View("Shop", sc);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public ActionResult BuyCardPackages(int id)
        {
            var dbPack = ShopManager.GetCardPackById(id);

            Packages cardPack = new Packages();
            cardPack.Idpack = dbPack.ID;
            cardPack.Packname = dbPack.Name;
            //GetValueOrDefault eingefügt wegen DATENTYP decimal
            cardPack.Packprice = dbPack.Price.GetValueOrDefault();
            cardPack.CardQuantity = dbPack.NumberOfCards.GetValueOrDefault();

            return View("Shop", cardPack);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="numberOfPacks"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public ActionResult BuyCardPackages(int id, int numberOfPacks)
        {
            Writer.LogInfo("id: " + id.ToString());
            Writer.LogInfo("numPacks: " + numberOfPacks.ToString());

            Models.Order o = new Models.Order();
            var dbPackages = ShopManager.GetCardPackById(id);

            Packages cardPack = new Packages();
            cardPack.Idpack = dbPackages.ID;
            cardPack.Packname = dbPackages.Name;
            //GetValueOrDefault eingefügt wegen DATENTYP decimal => Konvertierung da NULLABLE
            cardPack.CardQuantity = dbPackages.NumberOfCards.GetValueOrDefault();
            cardPack.Packprice = dbPackages.Price.GetValueOrDefault();

            o.Pack = cardPack;
            o.OrderDate = DateTime.Now;
            o.PackQuantity = numberOfPacks;
            o.UserBalance = UserManager.GetCurrencyBalanceByEmail(User.Identity.Name);

            TempData["Order"] = o;

            return RedirectToAction("OrderDetails");
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [ActionName("OrderDetails")]
        public ActionResult Order()
        {
            Models.Order o = (Models.Order)TempData["Order"];

            try
            {
                var totalOrder = ShopManager.TotalCost(o.Pack.Idpack, o.PackQuantity);
                if (totalOrder > o.UserBalance)
                {
                    return RedirectToAction("_NotEnoughBalance");
                }
                var balanceNew = o.UserBalance - totalOrder;

                var updated = UserManager.BalanceUpdateByEmail(User.Identity.Name, balanceNew);
                if (!updated)
                {
                    return RedirectToAction("UpdateError");
                }

                var orderedCards = ShopManager.OrderPack(o.Pack.Idpack, o.PackQuantity);

                //Methode 'AddCardsToCollectionByEmail' in USERMANAGER noch schreiben
                var updatedCards = UserManager.AddCardsToCollectionByEmail(User.Identity.Name, orderedCards);

                TempData["OrderedCards"] = orderedCards;
                return RedirectToAction("GeneratedCards");
            }
            catch (Exception e)
            {
                Writer.LogError(e);
                return RedirectToAction("Error", "Error");
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public ActionResult OrderDetails()
        {
            Models.Order o = (Models.Order)TempData["Order"];
            TempData["Order"] = o;   
            return View(o);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        private List<Models.Card> GeneratedCards()
        {
            List<DAL.Model.Card> orderedCards = (List<DAL.Model.Card>)TempData["OrderedCards"];
            var cards = new List<Models.Card>();

            foreach (var c in orderedCards)
            {
                Models.Card card = new Models.Card();
                card.ID = c.ID;
                card.Name = c.Name;
                card.Type = c.Type.Name; ///TODO prüfen
                card.Mana = c.ManaCost;
                card.Attack = c.Attack;
                card.Life = c.Life;
                card.Pic = c.Pic;
                cards.Add(card);
            }

            return cards;
        }



        /// <summary>
        /// Gibt auf Bildschirm View mit Meldung "nicht genug Diamanten" aus
        /// </summary>
        /// <returns>return View() - PartialView _NotEnoughBalance</returns>
        [HttpGet]
        [Authorize]
        public ActionResult _NotEnoughBalance() //PARTIAL VIEW erstellen !!!!!
        {
            return View();
        }
    }
}