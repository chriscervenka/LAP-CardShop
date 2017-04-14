using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CardGame.DAL.Logic;
using CardGame.Web.Models;

namespace CardGame.Web.Controllers
{
    public class ShopController : Controller
    {
        // GET: Shop
        public ActionResult ShopStart()
        {
            //List<Card> PackList = new List<Card>();

            //var dbCardlist = CardManager.GetCardsFromPack();

            //foreach (var c in dbPackList)
            //{
            //    Card card = new Card();
            //    card.ID = c.idcard;
            //    card.Name = c.cardname;
            //    card.Mana = c.mana;
            //    card.Attack = c.attack;
            //    card.Life = c.life;
            //    card.Pic = c.pic;
            //    //card.Type = c.tbltype.typename;
            //    //card.Type = CardManager.GetCardTypeById(c.fktype);
            //    card.Type = CardManager.CardTypes[c.fktype];

            //    PackList.Add(card);
            //}

            return View();
        }

        public ActionResult Shop()
        {
            return View();
        }
    }
}