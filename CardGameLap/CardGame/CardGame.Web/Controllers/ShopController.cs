using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CardGame.DAL.Logic;
using CardGame.Web.Models;
using CardGame.DAL.Model;


namespace CardGame.Web.Controllers
{
    public class ShopController : Controller
    {
        // GET: Shop

        /// <summary>
        /// Methode AllCardPacks() gibt mir alle in die Datenbank eingetragene PACKS zurück 
        /// </summary>
        /// <returns>return View("ShopStart", shop)</returns>
        [HttpGet]
        [Authorize]
        public ActionResult ShopStart()
        {
            Shop shop = new Shop();
            shop.CardPacks = new List<Packages>();

            var dbCardPacks = ShopManager.AllCardPacks();

            foreach (var dbCp in dbCardPacks)
            {
                Packages cardPack = new Packages();
                cardPack.Idpack = dbCp.idpack;
                cardPack.Packname = dbCp.packname;
                //GetValueOrDefault METHODE zur Konvertierung eingefügt wegen DATENTYP decimal
                cardPack.CardQuantity = dbCp.cardquantity.GetValueOrDefault();
                cardPack.Packprice = dbCp.packprice.GetValueOrDefault();

                shop.CardPacks.Add(cardPack);
            }

            return View("ShopStart", shop);
            //return View(shop);
        }


        [HttpGet]
        [Authorize]
        public ActionResult Shop()
        {
            ShopContainer sc = new ShopContainer();

            sc.shop = new Shop();
            sc.shop.CardPacks = new List<Packages>();
            sc.shop.Order = new Order();
            sc.shop.Order.UserBalance = UserManager.GetCurrencyBalanceByEmail(User.Identity.Name);

            var dbCardPacks = ShopManager.AllCardPacks();

            foreach (var dbCp in dbCardPacks)
            {
                Packages cardPack = new Packages();
                cardPack.Idpack = dbCp.idpack;
                cardPack.Packname = dbCp.packname;

                //GetValueOrDefault METHODE zur Konvertierung eingefügt wegen DATENTYP decimal
                cardPack.CardQuantity = dbCp.cardquantity.GetValueOrDefault();
                cardPack.Packprice = dbCp.packprice.GetValueOrDefault();

                sc.shop.CardPacks.Add(cardPack);
            }

            sc.generatedCards = this.GeneratedCards();

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
            var dbtblpack = ShopManager.GetCardPackById(id);

            Packages cardPack = new Packages();
            cardPack.Idpack = dbtblpack.idpack;
            cardPack.Packname = dbtblpack.packname;
            //GetValueOrDefault eingefügt wegen DATENTYP decimal
            cardPack.Packprice = dbtblpack.packprice.GetValueOrDefault();
            cardPack.CardQuantity = dbtblpack.cardquantity.GetValueOrDefault();

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
            Order o = new Order();
            var dbPackages = ShopManager.GetCardPackById(id);

            Packages cardPack = new Packages();
            cardPack.Idpack = dbPackages.idpack;
            cardPack.Packname = dbPackages.packname;
            //GetValueOrDefault eingefügt wegen DATENTYP decimal => Konvertierung da NULLABLE
            cardPack.CardQuantity = dbPackages.cardquantity.GetValueOrDefault();
            cardPack.Packprice = dbPackages.packprice.GetValueOrDefault();

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
            Order o = (Order)TempData["Order"];

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
            //var updatedCards = UserManager.AddCardsToCollectionByEmail(User.Identity.Name, orderedCards);

            TempData["OrderedCards"] = orderedCards;
            return RedirectToAction("GeneratedCards");

            //return RedirectToAction("Error", "Error");
        }



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public ActionResult OrderDetails()
        {
            Order o = (Order)TempData["Order"];
            TempData["Order"] = o;   
            return View(o);
        }



        /// <summary>
        /// Gibt in einer Liste von CARDS alle 
        /// </summary>
        /// <returns>View(cards)</returns>
        //[HttpGet]
        //[Authorize(Roles = "player")]
        //public ActionResult GeneratedCards()
        //{
        //    var orderedCards = (List<tblcard>)TempData["OrderedCards"];
        //    var cards = new List<Card>();

        //    foreach (var c in orderedCards)
        //    {
        //        Card card = new Card();
        //        card.ID = c.idcard;
        //        card.Name = c.cardname;
        //        card.Type = c.tbltype.typename;
        //        card.Mana = c.mana;
        //        card.Attack = c.attack;
        //        card.Life = c.life;
        //        card.Pic = c.pic;
        //        cards.Add(card);
        //    }
        //    return View(cards);
        //}[HttpGet]
        
        
        
        [HttpGet]
        private List<tblcard> GeneratedCards()
        {
            List<tblcard> orderedCards = (List<tblcard>)TempData["OrderedCards"];
            var cards = new List<Card>();

            foreach (var c in orderedCards)
            {
                Card card = new Card();
                card.ID = c.idcard;
                card.Name = c.cardname;
                card.Type = c.tbltype.typename;
                card.Mana = c.mana;
                card.Attack = c.attack;
                card.Life = c.life;
                card.Pic = c.pic;
                cards.Add(card);
            }
            return orderedCards;
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