using System.Collections.Generic;
using System.Linq;
using CardGame.DAL.Model;
using System;
using System.Web;
using System.Data.Entity;
using System.Diagnostics;

namespace CardGame.DAL.Logic
{
    public class CardManager
    {
        public static readonly Dictionary<int, string> CardTypes;


        /// <summary>
        /// 
        /// </summary>
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


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

       
        /// Karten IM Deck
        public static List<Card> GetAllCardsFromDeck(int id)
        {
            List<Card> ReturnList = null;
            using (var db = new ClonestoneFSEntities())
            {
                var deck = db.AllDecks.Find(id);
                ReturnList = new List<Card>();
                foreach (var item in deck.AllDeckcards)
                {
                    ReturnList.Add(item.Card);
                }
            }

            return ReturnList;
        }


        /// Karten FÜR das Deck
        public static List<Card> GetAllCardsForDeck(string email, int idDeck)
        {
            List<PersonCard> personCards = null;

            try
            {
                using (var context = new ClonestoneFSEntities())
                {
                    var person = context.AllPersons
                                    .Include(x => x.AllPersonCards)
                                    .Include(x => x.AllPersonCards.Select(y => y.Card))
                                    .FirstOrDefault(x => x.Email.Equals(email));

                    if (person == null)
                        throw new ArgumentException("Invalid username");

                    /// get ALL cards assigned to this user
                    personCards = person.AllPersonCards.ToList();

                    /// get deck for given idDeck
                    Deck deck = context.AllDecks.FirstOrDefault(x => x.ID == idDeck);

                    if (deck == null)
                        throw new ArgumentException("Invalid idDeck");

                    /// iterate over all userCards
                    foreach (var userCard in personCards)
                    {
                        /// check if current userCard is already present in current deck
                        DeckCard deckCard = deck.AllDeckcards.FirstOrDefault(x => x.ID_Card == userCard.ID_Card);

                        /// if card is already in deck
                        if (deckCard != null)
                            /// decrease number of cards available
                            userCard.NumberOfCards -= deckCard.NumCards;
                    }

                    /// return only those cards, whose number is greater than 0
                    personCards = personCards.Where(x => x.NumberOfCards > 0).ToList();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                Debugger.Break();
                throw ex;
            }

            return personCards.Select(x => x.Card).ToList();
        }


    }
}

