﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardGame.DAL.Model;
using CardGame.Log;
using System.Data.Entity;

namespace CardGame.DAL.Logic
{
    public class UserManager
    {
        

        public static int GetNumDistinctCardsOwnedByEmail(string email)
        {
            int numCards = -1;
            using (var db = new ClonestoneFSEntities())
            {
                Person dbUser = db.Person.Where(u => u.Email == email).FirstOrDefault();
                if (dbUser == null)
                {
                    throw new Exception("UserDoesNotExist");
                }
                numCards = dbUser.AllCollections.Count;
            }
            return numCards;
        }

        public static int GetNumTotalCardsOwnedByEmail(string email)
        {
            int numCards = -1;
            using (var db = new ClonestoneFSEntities())
            {
                Person dbUser = db.Person.Where(u => u.Email == email).FirstOrDefault();
                if (dbUser == null)
                {
                    throw new Exception("UserDoesNotExist");
                }
                numCards = 0;
                foreach (var c in dbUser.AllCollections)
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
                personList = db.Person.ToList();
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
                    dbUser = db.Person.Where(u => u.Email == email).FirstOrDefault();
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
                    var dbUser = db.Person.Where(u => u.Email == email).FirstOrDefault();
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
                    var dbUser = db.Person.Where(u => u.Email == email).FirstOrDefault();
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
        public static bool AddCardsToCollectionByEmail(string email, List<Card> cards)
        {
            var dbUser = new Person();
            try
            {
                using (var db = new ClonestoneFSEntities())
                {
                    dbUser = db.Person.Where(u => u.Email == email).FirstOrDefault();
                    if (dbUser == null)
                    {
                        throw new Exception("UserDoesNotExist");
                    }

                    foreach (var c in cards)
                    {
                        var userCC = (from coll in db.Collections
                                      where coll.ID_Deckcard == c.ID && coll.ID_Person == dbUser.ID
                                      select coll)
                                     .FirstOrDefault();

                        if (userCC == null) //User does not own card, add to collection
                        {

                            var cc = new Collection();
                            //cc.AllCards = cards.Find(c.ID);
                            cc.Person = dbUser;
                            //cc.AllCards = 1;
                            dbUser.AllCollections.Add(cc);
                            db.SaveChanges();
                        }
                        else //User owns card, add to num
                        {
                            userCC.ID += 1;
                            db.Entry(userCC).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }
                    //db.SaveChanges();
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
        public static int GetNumDecksOwnedByEmail(string email)
        {
            int numDecks = -1;
            using (var db = new ClonestoneFSEntities())
            {
                Person dbUser = db.Person.Where(u => u.Email == email).FirstOrDefault();
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
                    var dbUser = db.Person.Where(u => u.Email == email).FirstOrDefault();

                    if (dbUser == null)
                    {
                        throw new Exception("UserDoesNotExist");
                    }
                    var dbCardCollection = dbUser.AllCollections.ToList();
                    if (dbCardCollection == null)
                    {
                        throw new Exception("CardCollectionNotFound");
                    }
                    //foreach (var cc in  dbCardCollection)
                    //{
                    //    for (int i = 0; i < cc.NumberOfCards; i++)
                    //        cardList.Add(cc.AllCards);
                    //}
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
