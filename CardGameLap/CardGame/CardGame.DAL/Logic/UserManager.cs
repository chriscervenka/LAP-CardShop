using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardGame.DAL.Model;
using CardGame.Log;
using System.Data.Entity;
using System.Diagnostics;

namespace CardGame.DAL.Logic
{
    public class UserManager
    {
        

        public static int GetNumDistinctCardsOwnedByEmail(string email)
        {
            int numCards = -1;
            using (var db = new ClonestoneFSEntities())
            {
                Person dbUser = db.AllPersons.Where(u => u.Email == email).FirstOrDefault();
                if (dbUser == null)
                {
                    throw new Exception("UserDoesNotExist");
                }
                numCards = dbUser.AllPersonCards.Count;
            }
            return numCards;
        }

        public static int GetNumTotalCardsOwnedByEmail(string email)
        {
            int numCards = -1;
            using (var db = new ClonestoneFSEntities())
            {
                Person dbUser = db.AllPersons.Where(u => u.Email == email).FirstOrDefault();
                if (dbUser == null)
                {
                    throw new Exception("UserDoesNotExist");
                }
                numCards = 0;
                foreach (var c in dbUser.AllPersonCards)
                {
                    numCards += c.NumberOfCards ?? 0;
                }

            }
            return numCards;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<Person> GetAllUser()
        {
            List<Person> personList = null;
            using (var db = new ClonestoneFSEntities())
            {
                // TODO - Include
                // .Include(t => t.tabelle) um einen Join zu machen !
                personList = db.AllPersons.ToList();
            }
            return personList;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static Person GetPersonByEmail(string email)
        {
            Person dbUser = null;

            try
            {
                using (var db = new ClonestoneFSEntities())
                {
                    dbUser = db.AllPersons.Where(u => u.Email == email).FirstOrDefault();
                    if (dbUser == null)
                    {
                        throw new Exception("User Does Not Exist");
                    }
                }
            }
            catch (Exception e)
            {
                Writer.LogError(e);
            }
            return dbUser;
        }


        public static Person GetAllUser(object name)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static string GetRoleNamesByEmail(string email)
        {
            string role = "";

            try
            {
                using (var db = new ClonestoneFSEntities())
                {
                    var dbUser = db.AllPersons.Where(u => u.Email == email).FirstOrDefault();
                    if (dbUser == null)
                    {
                        throw new Exception("User Does Not Exist");
                    }
                    role = dbUser.Role;
                    
                    
                }
            }
            catch (Exception e)
            {
                Writer.LogError(e);
            }
            
            return role;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="balanceNew"></param>
        /// <returns></returns>
        public static bool BalanceUpdateByEmail(string email, int balanceNew)
        {
            var dbUser = GetPersonByEmail(email);
            dbUser.Currencybalance = balanceNew;

            try
            {
                using (var db = new ClonestoneFSEntities())
                {
                    db.Entry(dbUser).State = EntityState.Modified;
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
        /// <param name="email"></param>
        /// <returns></returns>
        public static int GetCurrencyBalanceByEmail(string email)
        {
            return GetPersonByEmail(email).Currencybalance.GetValueOrDefault();
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static List<Deck> GetAllDecksByEmail(string email)
        {
            try
            {
                using (var db = new ClonestoneFSEntities())
                {
                    var dbUser = db.AllPersons.Where(u => u.Email == email).FirstOrDefault();
                    if (dbUser == null)
                    {
                        throw new Exception("UserDoesNotExist");
                    }
                    var dbDecks = dbUser.AllDecks.ToList();
                    if (dbDecks == null)
                    {
                        throw new Exception("NoDecksFound");
                    }

                    return dbDecks;
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
        /// <param name="email"></param>
        /// <param name="cards"></param>
        /// <returns></returns>
        public static bool AddPersonCardsByEmail(string email, List<Card> cards)
        {
            Person person = null;
            try
            {
                using (var db = new ClonestoneFSEntities())
                {
                    person = db.AllPersons.Where(u => u.Email == email).FirstOrDefault();
                    if (person == null)
                    {
                        throw new Exception("UserDoesNotExist");
                    }

                    /// gehe alle Karten durch die dieser person hinzugefügt werden sollen
                    foreach (var card in cards)
                    {
                        /// ermittle ob die Person diese Karte bereits hat
                        var personCard = person.AllPersonCards.Where(x => x.ID_Card == card.ID).FirstOrDefault();

                        /// die person hat diese Karte noch NICHT
                        if (personCard == null)
                        { 
                            personCard = new PersonCard();
                            personCard.Person = person;
                            personCard.NumberOfCards = 1;
                            person.AllPersonCards.Add(personCard);
                            
                        }
                        else /// die person hat diese karte schon einmal
                        {
                            personCard.NumberOfCards += 1;  /// erhöhe die anzahl dieser karte um eins
                        }
                    }

                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                Debugger.Break();
                Writer.LogError(e);
                return false;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static int GetNumDecksOwnedByEmail(string email)
        {
            int numDecks = -1;
            using (var db = new ClonestoneFSEntities())
            {
                Person dbUser = db.AllPersons.Where(u => u.Email == email).FirstOrDefault();
                if (dbUser == null)
                {
                    throw new Exception("User exestiert nicht");
                }
                numDecks = dbUser.AllDecks.Count;
            }
            return numDecks;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static List<Card> GetAllCardsByEmail(string email)
        {
            var cardList = new List<Card>();

            try
            {
                using (var db = new ClonestoneFSEntities())
                {
                    var user = db.AllPersons.Where(u => u.Email == email).FirstOrDefault();

                    if (user == null)
                    {
                        throw new Exception("UserDoesNotExist");
                    }
                    var userPersonCards = user.AllPersonCards.ToList();
                    if (userPersonCards == null)
                    {
                        throw new Exception("CardCollectionNotFound");
                    }
                    //foreach (var cc in  dbCardCollection)
                    //{
                    //    for (int i = 0; i < cc.NumberOfCards; i++)
                    //        cardList.Add(cc.AllCards);
                    //}

                    /// TODO: fix personCard vs. card
                    return cardList;
                }
            }
            catch (Exception e)
            {
                Writer.LogError(e);
                return null;
            }
        }
    }
}
