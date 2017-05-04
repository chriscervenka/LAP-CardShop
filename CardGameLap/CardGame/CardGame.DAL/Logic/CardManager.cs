using System.Collections.Generic;
using System.Linq;
using CardGame.DAL.Model;
using System;
using System.Web;

namespace CardGame.DAL.Logic
{
    public class CardManager
    {
        public static readonly Dictionary<int, string> CardTypes;

        static CardManager()
        {
            CardTypes = new Dictionary<int, string>();
            List<Model.Type> cardTypeList = null;

            using (var db = new ClonestoneFSEntities())
            {
                cardTypeList = db.AllTypes.ToList();
            }

            foreach (var type in cardTypeList)
            {
                CardTypes.Add(type.ID, type.Name);
            }

            CardTypes.Add(0, "n/a");
        }



        public static List<Card> GetAllCards()
        {
            List<Card> ReturnList = null;
            using (var db = new ClonestoneFSEntities())
            {
                //ReturnList = db.tblcard.Include(t => t.tbltype).ToList();
                ReturnList = db.AllCards.ToList();
            }
            return ReturnList;

        }

        //Theoretisch überflüssig
        public static string GetCardTypeById(int? id)
        {
            string TypeName = "n/a";

            using (var db = new ClonestoneFSEntities())
            {
                TypeName = db.AllTypes.Find(id).Name;
            }
            return TypeName;
        }

        public static Card GetCardById(int id)
        {
            Card card = null;

            using (var db = new ClonestoneFSEntities())
            {
                //Extention Method
                card = db.AllCards.Where(c => c.ID == id).FirstOrDefault();

                //Klassisch LINQ
                //card = (from c in db.tblcard
                //        where c.idcard == id
                //        select c).FirstOrDefault();
            }

            return card;
        }


        // TODO
        // New Method GetAllCardsFromDeck()
        public static List<Card> GetAllCardsFromDeck(int id)
        {
            List<Card> ReturnList = null;
            using (var db = new ClonestoneFSEntities())
            {
                var deck = db.AllDecks.Find(id);
                ReturnList = new List<Card>();
                foreach (var item in deck.AllDeckCards)
                {
                    ReturnList.Add(item.Card);
                }
            }

            return ReturnList;
        }

        
        }
    }

