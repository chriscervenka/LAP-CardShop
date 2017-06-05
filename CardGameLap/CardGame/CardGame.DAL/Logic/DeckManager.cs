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
        public static Deck GetDeckById(int id)
        {
            Deck deck = null;
            try
            {
                using (var db = new ClonestoneFSEntities())
                {
                    deck = db.Decks.Find(id);
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


        public static int GetNumDeckCardsById(int id)
        {
            Deck deck = null;
            int numCards = -1;
            try
            {
                using (var db = new ClonestoneFSEntities())
                {
                    deck = db.Decks.Find(id);
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


        public static List<Card> GetDeckCardsById(int id)
        {
            var deckCards = new List<Card>();

            try
            {
                using (var db = new ClonestoneFSEntities())
                {
                    var dbDeck = db.Decks.Where(d => d.ID == id).FirstOrDefault();
                    if (dbDeck == null)
                    {
                        throw new Exception("UserDoesNotExist");
                    }
                    var dbDeckCollection = dbDeck.AllDeckcards.ToList();
                    if (dbDeckCollection == null)
                    {
                        throw new Exception("CardCollectionNotFound");
                    }
                    foreach (var dc in dbDeckCollection)
                    {
                        for (int i = 0; i < dc.Numcards; i++)
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


        public static bool UpdateDeckById(int id, List<Deckcard> deckCards)
        {
            Deck deck = null;
            try
            {
                using (var db = new ClonestoneFSEntities())
                {
                    deck = db.Decks.Find(id);
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
                        var dbDeckCard = new Deckcard();
                        dbDeckCard.Numcards = dc.Numcards;
                        dbDeckCard.Deck = db.Decks.Find(id);
                        dbDeckCard.Card = db.Cards.Find(dc.Card.ID);
                        db.Deckcards.Add(dbDeckCard);
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



        public static bool AddDeckByUserId(int id, string name)
        {
            Person user = null;
            try
            {
                using (var db = new ClonestoneFSEntities())
                {
                    user = db.Person.Find(id);
                    Deck deck = new Deck();
                    deck.Name = name;
                    deck.Person = user;

                    db.Decks.Add(deck);
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


        public static bool AddDefaultDecksByUserId(int id)
        {
            Person user = null;
            try
            {
                using (var db = new ClonestoneFSEntities())
                {
                    user = db.Person.Find(id);
                    bool addedAll = false;
                    if (user == null)
                        throw new Exception("UserNotFound");

                    for (int i = 1; i <= 3; ++i)
                    {
                        addedAll = AddDeckByUserId(id, user.Email + i.ToString());
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
