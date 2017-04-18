using System;
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
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<tblperson> GetAllUser()
        {
            List<tblperson> ReturnList = null;
            using (var db = new ClonestoneFSEntities())
            {
                // TODO - Include
                // .Include(t => t.tabelle) um einen Join zu machen !
                ReturnList = db.tblperson.ToList();
            }
            return ReturnList;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static tblperson GetPersonByEmail(string email)
        {
            tblperson dbUser = null;

            try
            {
                using (var db = new ClonestoneFSEntities())
                {
                    dbUser = db.tblperson.Where(u => u.email == email).FirstOrDefault();
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
                    var dbUser = db.tblperson.Where(u => u.email == email).FirstOrDefault();
                    if (dbUser == null)
                    {
                        throw new Exception("User Does Not Exist");
                    }
                    role = dbUser.userrole;
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
            dbUser.currencybalance = balanceNew;
            {
                using (var db = new ClonestoneFSEntities())
                {
                    db.Entry(dbUser).State = EntityState.Modified;
                    db.SaveChanges();
                    return true;
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static int GetCurrencyBalanceByEmail(string email)
        {
            return GetPersonByEmail(email).currencybalance.GetValueOrDefault();
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static List<tbldeck> GetAllDecksByEmail(string email)
        {
            try
            {
                using (var db = new ClonestoneFSEntities())
                {
                    var dbUser = db.tblperson.Where(u => u.email == email).FirstOrDefault();
                    if (dbUser == null)
                    {
                        throw new Exception("UserDoesNotExist");
                    }
                    var dbDecks = dbUser.tbldeck.ToList();
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
    }
}
