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

        #region ACTIONMETHODE Shop(int? ShopPage)
        /// <summary>
        /// Methode AllCardPacks() gibt mir alle in die Datenbank eingetragene PACKS zurück 
        /// </summary>
        /// <returns></returns>
        //[HttpGet]
        [Authorize]
        public ActionResult Shop(int? ShopPage)
        {
            Debug.WriteLine("GET - Shop - Packs");
            //ShopContainer model = new ShopContainer();
            PackOverviewModel model = new PackOverviewModel();

            List<Web.Models.Pack> packages = new List<Web.Models.Pack>();
            Models.Pack pack = new Models.Pack();

            var currencyBalance = UserManager.GetCurrencyBalanceByEmail(User.Identity.Name);

            var dbCardPacks = ShopManager.AllCardPacks();

            foreach (var p in dbCardPacks)
            {
                Models.Pack cardPack = new Models.Pack();
                cardPack.ID = p.ID;
                cardPack.Packname = p.Name;
                //GetValueOrDefault METHODE zur Konvertierung eingefügt wegen DATENTYP decimal
                cardPack.CardQuantity = p.Cardquantity.GetValueOrDefault();
                cardPack.Packprice = p.Packprice.GetValueOrDefault();
                cardPack.DiamondValue = p.DiamondValue.GetValueOrDefault();
                cardPack.IsMoney = p.IsMoney.GetValueOrDefault();
                packages.Add(cardPack);
            }
            // Liste PACK wird auf model.CardPacks gespeichert und model wird in View übergeben !!!!!!!

            if (ShopPage != null)
            {
                if (ShopPage == 1)
                {
                    packages = packages.Where(c => c.IsMoney == false).ToList();
                }
                else if (ShopPage == 2)
                {
                    packages = packages.Where(c => c.IsMoney == true).ToList();
                }
            }

            model.CardPacks = packages;
            model.AmountMoney = currencyBalance;
            return View(model);
        }
        #endregion


        #region ACTIONMETHODE Packs() nicht verwendet
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
        #endregion


        #region ACTIONMETHODE BuyCardPackages(int id)
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

            //TODO Pilgersdorfer fragen ob in selber VIEW generierbar
            return View("Shop", cardPack);
        }
        #endregion


        #region ACTIONMETHODE BuyPack(int id, string email)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="numberOfPacks"></param>
        /// <returns></returns>
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult BuyPack(int id, string email)
        {
            Debug.WriteLine("POST - Shop - BuyCardPackages(id)");
            Writer.LogInfo("id: " + id.ToString());

            Models.Order o = new Models.Order();
            var dbPackages = ShopManager.GetCardPackById(id);

            //Models.Pack cardPack = new Models.Pack();
            //cardPack.ID = dbPackages.ID;
            //cardPack.Packname = dbPackages.Name;
            ////GetValueOrDefault eingefügt wegen DATENTYP decimal => Konvertierung da NULLABLE
            //cardPack.CardQuantity = dbPackages.Cardquantity.GetValueOrDefault();
            //cardPack.Packprice = dbPackages.Packprice.GetValueOrDefault();

            //o.CardPacks = ;
            o.OrderDate = DateTime.Now;
            o.PackQuantity = 1;
            //o.UserBalance = UserManager.GetCurrencyBalanceByEmail(User.Identity.Name);

            email = User.Identity.Name;

            BuyResult rueck = ShopManager.BuyPack(id, email);
            o.UserBalance = UserManager.GetCurrencyBalanceByEmail(User.Identity.Name);
            TempData["Order"] = o;

            return RedirectToAction("OrderDetails");
        }
        #endregion


        #region ACTIONMETHODE Buy(int id)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Buy(int id)
        {
            ActionResult result = RedirectToAction("Shop", "Shop");

            //if (id <= 0)
            //{
            //    TempData[Constants.MessageType.WARNING] = Messages.INVALID_PACK_NUMBER;
            //}

            string username = User.Identity.Name;
            try
            {
                //switch (ShopAdministration.BuyPack(id, username))
                //{
                //    case BuyResult.Success:
                //        TempData[Constants.MessageType.SUCCESS] = Messages.BUY_PACK_SUCCESS;
                //        break;
                //    case BuyResult.NotEnoughMoney:
                //        TempData[Constants.MessageType.WARNING] = Messages.NOT_ENOUGH_MONEY;
                //        break;
                //    default:
                //        break;
                //}
            }
            catch (Exception)
            {
                //TempData[Constants.MessageType.ERROR] = Messages.ERROR_COMMON;
            }

            return result;
        }
        #endregion


        #region ACTIONMETHODE Order() nicht verwendet
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
        #endregion


        #region ACTIONMETHODE OrderDetails()
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>

        [Authorize]
        public ActionResult OrderDetails()
        {
            Debug.WriteLine("POST - Shop - OrderDetails");
            Models.Order o = (Models.Order)TempData["Order"];
            TempData["Order"] = o;
            return View(o);
        }
        #endregion


        #region ACTIONMETHODE List<Models.Card> GeneratedCards()
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
        #endregion


        #region ACTIONMETHODE  NotEnoughBalance()
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
        #endregion
    }
}