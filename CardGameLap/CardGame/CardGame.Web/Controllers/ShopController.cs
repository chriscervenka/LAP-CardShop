using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CardGame.DAL.Logic;
using CardGame.Web.Models;
using CardGame.DAL.Model;
using CardGame.Log;
using System.Diagnostics;

namespace CardGame.Web.Controllers
{
    public class ShopController : Controller
    {
        /// <summary>
        /// Methode AllCardPacks() gibt mir alle in die Datenbank eingetragene PACKS zurück 
        /// </summary>
        /// <returns>return View(vom ShopContainer.Model)</returns>
        //[HttpGet]
        [Authorize]
        public ActionResult Shop()
        {
            Debug.WriteLine("GET - Shop - Packs");
            //ShopContainer model = new ShopContainer();
            PackOverviewModel model = new PackOverviewModel();

            List<Web.Models.Pack> packages = new List<Web.Models.Pack>();
            Models.Pack pack = new Models.Pack();

            var dbCardPacks = ShopManager.AllCardPacks();

            foreach (var p in dbCardPacks)
            {
                Models.Pack cardPack = new Models.Pack();
                cardPack.ID = p.ID;
                cardPack.Packname = p.Name;
                //GetValueOrDefault METHODE zur Konvertierung eingefügt wegen DATENTYP decimal
                cardPack.CardQuantity = p.Cardquantity.GetValueOrDefault();
                cardPack.Packprice = p.Packprice.GetValueOrDefault();
                packages.Add(cardPack);
            }
            // Liste PACKAGES wird auf model.CardPacks gespeichert und model wird in View übergeben !!!!!!!
            model.CardPacks = packages;
            return View(model);
        }
        
        //[HttpPost]
        //[Authorize]
        //public ActionResult Packs()
        //{
        //    Pack model = null;
        //    try
        //    {
        //        List<Pack> packs = ShopManager.AllCardPacks();
        //        Person currentPerson = UserManager.GetAllUser(packs);

        //        model = new Cart()
        //        {
        //            Money = currentPerson.Mo
        //        };

        //        List<Packages> packagesModel = new List<Packages>();

        //        foreach (var pack in packs)
        //        {
        //            packagesModel.Add(new Packages()
        //            {
        //                Packname = pack.Name,
        //                CardQuantity = pack.Cardquantity.GetValueOrDefault(),
        //                Packprice = pack.Packprice.GetValueOrDefault(),
        //                ID = pack.ID
        //            });
        //        }

        //        model.AllOrders = packagesModel; 
        //    }


        //    catch (Exception)
        //    {

        //        throw;
        //    }

        //    return View(model);
        //}
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //[HttpGet]
        //[Authorize]
        //public ActionResult Shop()
        //{
        //    Debug.WriteLine("GET - Shop - Shop");
        //    ShopContainer sc = new ShopContainer();

        //    sc.Shop = new Shop();
        //    sc.Shop.Order = new List<Models.Pack>();
        //    sc.Shop.Order = new Models.Order();
        //    sc.Shop.Order.UserBalance = UserManager.GetCurrencyBalanceByEmail(User.Identity.Name);

        //    var dbCardPacks = ShopManager.AllCardPacks();

        //    foreach (var dbCp in dbCardPacks)
        //    {
        //        Models.Pack cardPack = new Models.Pack();
        //        cardPack.ID = dbCp.ID;
        //        cardPack.Packname = dbCp.Name;
        //        //GetValueOrDefault METHODE zur Konvertierung eingefügt wegen DATENTYP decimal
        //        cardPack.CardQuantity = dbCp.Cardquantity.GetValueOrDefault();
        //        cardPack.Packprice = dbCp.Packprice.GetValueOrDefault();
        //        sc.Shop.CardPacks.Add(cardPack);
        //    }

        //    sc.GeneratedCards = GeneratedCards();

        //    return View("Shop", sc);
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public ActionResult BuyCardPackages(int id)
        {
            Debug.WriteLine("GET - Shop - BuyCardPackages(id)");
            var dbPack = ShopManager.GetCardPackById(id);

            Models.Pack cardPack = new Models.Pack();
            cardPack.ID = dbPack.ID;
            cardPack.Packname = dbPack.Name;
            //GetValueOrDefault eingefügt wegen DATENTYP decimal
            cardPack.Packprice = dbPack.Packprice.GetValueOrDefault();
            cardPack.CardQuantity = dbPack.Cardquantity.GetValueOrDefault();

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
        public ActionResult BuyPack(int id, int numberOfPacks)
        {
            Debug.WriteLine("POST - Shop - BuyCardPackages(id, numberOfPacks)");
            Writer.LogInfo("id: " + id.ToString());
            Writer.LogInfo("numPacks: " + numberOfPacks.ToString());

            Models.Order o = new Models.Order();
            var dbPackages = ShopManager.GetCardPackById(id);

            Models.Pack cardPack = new Models.Pack();
            cardPack.ID = dbPackages.ID;
            cardPack.Packname = dbPackages.Name;
            //GetValueOrDefault eingefügt wegen DATENTYP decimal => Konvertierung da NULLABLE
            cardPack.CardQuantity = dbPackages.Cardquantity.GetValueOrDefault();
            cardPack.Packprice = dbPackages.Packprice.GetValueOrDefault();

            //o.CardPacks = ;
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
        //[HttpPost]
        //[Authorize]
        //[ActionName("OrderDetails")]
        //public ActionResult Order()
        //{
        //    Debug.WriteLine("POST - Shop - Order");
        //    Models.Order o = (Models.Order)TempData["Order"];

        //    try
        //    {
        //        var totalOrder = ShopManager.TotalCost(o.List<Models.Pack>, o.PackQuantity);
        //        if (totalOrder > o.UserBalance)
        //        {
        //            return RedirectToAction("_NotEnoughBalance");
        //        }
        //        var balanceNew = o.UserBalance - totalOrder;

        //        var updated = UserManager.BalanceUpdateByEmail(User.Identity.Name, balanceNew);
        //        if (!updated)
        //        {
        //            return RedirectToAction("UpdateError");
        //        }

        //        var orderedCards = ShopManager.OrderPack(o.Pack.ID, o.PackQuantity);

        //        //Methode 'AddCardsToCollectionByEmail' in USERMANAGER noch schreiben
        //        var updatedCards = UserManager.AddCardsToCollectionByEmail(User.Identity.Name, orderedCards);

        //        TempData["OrderedCards"] = orderedCards;
        //        return RedirectToAction("GeneratedCards");
        //    }
        //    catch (Exception e)
        //    {
        //        Writer.LogError(e);
        //        return RedirectToAction("Error", "Error");
        //    }
        //}



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public ActionResult OrderDetails()
        {
            Debug.WriteLine("POST - Shop - OrderDetails");
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
            Debug.WriteLine("GET - Shop - GeneratedCards");
            List<DAL.Model.Card> orderedCards = (List<DAL.Model.Card>)TempData["OrderedCards"];
            var cards = new List<Models.Card>();

            foreach (var c in orderedCards)
            {
                Models.Card card = new Models.Card();
                card.ID = c.ID;
                card.Name = c.Name;
                card.Type = c.Type.Name; ///TODO prüfen
                card.Mana = c.Mana;
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
        /// <returns>return View()</returns>
        [HttpGet]
        [Authorize]
        public ActionResult NotEnoughBalance()
        {
            Debug.WriteLine("GET - Shop - _NotEnoughBalance");
            return View();
        }
    }
}