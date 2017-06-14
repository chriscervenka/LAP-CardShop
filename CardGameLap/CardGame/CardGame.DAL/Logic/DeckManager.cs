using CardGame.DAL.Model;
using CardGame.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.DAL.Logic
{
    public class DeckManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Deck GetDeckById(int id)
        {
            Deck deck = null;
            try
            {
                using (var db = new ClonestoneFSEntities())
                {
                    deck = db.AllDecks.Find(id);
                    if (deck == null)
                    {
                        throw new Exception("DeckNotFound");
                    }
                    return deck;
                }
            }
            catch (Exception e)
            {
                Writer.LogError(e);
                return null;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int GetNumDeckCardsById(int id)
        {
            Deck deck = null;
            int numCards = -1;
            try
            {
                using (var db = new ClonestoneFSEntities())
                {
                    deck = db.AllDecks.Find(id);
                    if (deck == null)
                    {
                        throw new Exception("DeckNotFound");
                    }
                    numCards = deck.AllDeckcards.Count;

                    return numCards;
                }
            }
            catch (Exception e)
            {
                Writer.LogError(e);
                return numCards;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="idDeck"></param>
        /// <returns></returns>
        public static List<Card> GetDeckCardsById(int idDeck)
        {
            var deckCards = new List<Card>();

            try
            {
                using (var db = new ClonestoneFSEntities())
                {
                    //var dbDeck = db.Decks.Where(d => d.ID_Person == idPerson).FirstOrDefault();
                    //if (dbDeck == null)
                    //{
                    //    throw new Exception("UserDoesNotExist");
                    //}
                    //var dbDeckCollection = dbDeck.AllDeckcards.ToList();
                    var dbDeckCards = db.AllDeckcards.Where(d => d.ID_Deck == idDeck);
                    if (dbDeckCards == null)
                    {
                        throw new Exception("CardCollectionNotFound");
                    }
                    foreach (var dc in dbDeckCards)
                    {
                         deckCards.Add(dc.Card);
                    }

                    return deckCards;
                }
            }
            catch (Exception e)
            {
                Writer.LogError(e);
                return null;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deckCards"></param>
        /// <returns></returns>
        public static bool UpdateDeckById(int id, List<DeckCard> deckCards)
        {
            Deck deck = null;
            try
            {
                using (var db = new ClonestoneFSEntities())
                {
                    deck = db.AllDecks.Find(id);
                    if (deck == null)
                    {
                        throw new Exception("DeckNotFound");
                    }

                    var existingDeckList = deck.AllDeckcards.ToList();

                    foreach (var dc in existingDeckList)
                    {
                        deck.AllDeckcards.Remove(dc);
                    }
                    db.SaveChanges();

                    foreach (var dc in deckCards)
                    {
                        var dbDeckCard = new DeckCard();
                        dbDeckCard.NumCards = dc.NumCards;
                        dbDeckCard.Deck = db.AllDecks.Find(id);
                        dbDeckCard.Card = db.AllCards.Find(dc.Card.ID);
                        db.AllDeckcards.Add(dbDeckCard);
                    }

                    db.SaveChanges();

                    return true;
                }
            }
            catch (Exception e)
            {
                Writer.LogError(e);
                return false;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool AddDeckByUserId(int id, string name)
        {
            Person user = null;
            try
            {
                using (var db = new ClonestoneFSEntities())
                {
                    user = db.AllPersons.Find(id);
                    Deck deck = new Deck();
                    deck.Name = name;
                    deck.Person = user;

                    db.AllDecks.Add(deck);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                Writer.LogError(e);
                return false;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool AddDefaultDecksByUserId(int id)
        {
            Person user = null;
            try
            {
                using (var db = new ClonestoneFSEntities())
                {
                    user = db.AllPersons.Find(id);
                    bool addedAll = false;
                    if (user == null)
                        throw new Exception("UserNotFound");

                    for (int i = 1; i <= 3; ++i)
                    {
                        //addedAll = AddDeckByUserId(id, user.Email + i.ToString());
                        addedAll = AddDeckByUserId(id, user.Gamertag + " Deck " + i.ToString());
                    }
                    return addedAll;
                }
            }
            catch (Exception e)
            {
                Writer.LogError(e);
                return false;
            }
        }
    }
}
