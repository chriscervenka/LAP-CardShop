using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardGame.DAL.Model;
using CardGame.Log;

namespace CardGame.DAL.Logic
{
    public class AuthManager
    {
        public static bool Register(Person regUser)
        {
            try
            {
                using (var db = new ClonestoneFSEntities())
                {
                    if (db.AllPeople.Any(n => n.Email == regUser.Email))
                    {
                        throw new Exception("UserAlreadyExists");
                    }
                    //Salt erzeugen
                    string salt = Helper.GenerateSalt();

                    //Passwort Hashen
                    string hashedAndSaltedPassword = Helper.GenerateHash(regUser.Password + salt);

                    regUser.Password = hashedAndSaltedPassword;
                    regUser.Salt = salt;

                    db.AllPeople.Add(regUser);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                
                Writer.LogError(e);            
            }

            return true;
        }

        public static bool AuthUser(string email, string password)
        {
            try
            {
                string dbUserPassword = null;
                string dbUserSalt = null;

                using (var db = new ClonestoneFSEntities())
                {
                    Person dbUser = db.AllPeople.Where(u => u.Email == email).FirstOrDefault();
                    if (dbUser == null)
                    {
                        throw new Exception("UserDoesNotExist");
                    }

                    dbUserPassword = dbUser.Password;
                    dbUserSalt = dbUser.Salt;

                    Writer.LogInfo("Entered Pass = " + password);

                    password = Helper.GenerateHash(password + dbUserSalt);

                    Writer.LogInfo("HashPass = " + password);

                    if (dbUserPassword == password)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

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
