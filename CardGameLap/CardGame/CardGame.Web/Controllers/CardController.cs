using System.Collections.Generic;
using System.Web.Mvc;
using CardGame.Web.Models;
using CardGame.DAL.Logic;
using CardGame.DAL.Model;
using System.Linq;

namespace CardGame.Web.Controllers
{
    public class CardController : Controller
    {
        // GET: Card
        public ActionResult Overview()
        {
            List<Models.Card> CardList = new List<Models.Card>();

            var dbCardlist = CardManager.GetAllCards();

            foreach (var c in dbCardlist)
            {
                Models.Card card = new Models.Card();
                card.ID = c.ID;
                card.Name = c.Name;
                card.Mana = c.Mana;
                card.Attack = c.Attack;
                card.Life = c.Life;
                card.Pic = c.Pic;
                //card.Type = c.tbltype.typename;
                //card.Type = CardManager.GetCardTypeById(c.fktype);
                card.Type = CardManager.CardTypes[c.ID_Type];

                CardList.Add(card);
            }

            return View(CardList);
        }

        public ActionResult Details(int id)
        {
            DAL.Model.Card dbcard = null;

            dbcard = CardManager.GetCardById(id);

            Models.Card card = new Models.Card();
            card.ID = dbcard.ID;
            card.Name = dbcard.Name;
            card.Mana = dbcard.Mana;
            card.Attack = dbcard.Attack;
            card.Life = dbcard.Life;
            card.Type = CardManager.CardTypes[dbcard.ID_Type];

            return View(card);
        }
    }
}
