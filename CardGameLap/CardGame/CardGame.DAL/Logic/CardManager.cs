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
            List<tbltype> cardTypeList = null;

            using (var db = new ClonestoneFSEntities())
            {
                cardTypeList = db.tbltype.ToList();
            }

            foreach (var type in cardTypeList)
            {
                CardTypes.Add(type.idtype, type.typename);
            }

            CardTypes.Add(0, "n/a");
        }

        public static List<tblcard> GetAllCards()
        {
            List<tblcard> ReturnList = null;
            using (var db = new ClonestoneFSEntities())
            {
                //ReturnList = db.tblcard.Include(t => t.tbltype).ToList();
                ReturnList = db.tblcard.ToList();
            }
            return ReturnList;

        }

        //Theoretisch überflüssig
        public static string GetCardTypeById(int? id)
        {
            string TypeName = "n/a";

            using (var db = new ClonestoneFSEntities())
            {
                TypeName = db.tbltype.Find(id).typename;
            }
            return TypeName;
        }

        public static tblcard GetCardById(int id)
        {
            tblcard card = null;

            using (var db = new ClonestoneFSEntities())
            {
                //Extention Method
                card = db.tblcard.Where(c => c.idcard == id).FirstOrDefault();

                //Klassisch LINQ
                //card = (from c in db.tblcard
                //        where c.idcard == id
                //        select c).FirstOrDefault();
            }

            return card;
        }


        // TODO
        // New Method GetAllCardsFromDeck()
        public static List<tblcard> GetAllCardsFromDeck(int id)
        {
            List<tblcard> ReturnList = null;
            using (var db = new ClonestoneFSEntities())
            {
                var deck = db.tbldeck.Find(id);
                ReturnList = new List<tblcard>();
                foreach (var item in deck.tbldeckcard)
                {
                    ReturnList.Add(item.tblcard);
                }
                
                //ReturnList = db.tblcard.Include(t => t.tbltype).ToList();
                //ReturnList = db.tblcard.Include(t => t.tbldeckcard).ToList();
                //ReturnList = db.tbldeck.ToList();

                //var Deck = db.tbldeck.
                //    Join(db.tbldeckcard, u => u.iddeck, uir => uir.fkdeck,(u, uir) => new { u, uir }).
                //    Join(db.tblcard, r => r.uir.fkcard, ro => ro.idcard, (r, ro) => new { r, ro })
                //    .Where(m => m.r.u.iddeck == 1);

                //var result = from c in db.tbldeck
                //             join o in db.tblcard
                //             on c equals o.tbldeckcard
                //             select new
                //             {
                //                 deckid = c.iddeck,
                //                 name = c.deckname,                                

                //             };
            }
            return ReturnList;



        }
    }
}
