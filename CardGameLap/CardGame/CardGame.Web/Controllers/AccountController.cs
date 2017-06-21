using System;
using System.Web;
using System.Web.Mvc;
using CardGame.Web.Models;
using CardGame.DAL.Logic;
using CardGame.DAL.Model;
using System.Web.Security;
using System.Data.Entity;

namespace CardGame.Web.Controllers
{
    public class AccountController : Controller
    {
        

        // GET: Account
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        #region Actionmethode LOGIN
        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public ActionResult Login(Login login)
        {
            bool hasAccess = AuthManager.AuthUser(login.Email, login.Password);

            login.Role = UserManager.GetRoleNamesByEmail(login.Email);


            if (hasAccess)
            {
                var authTicket = new FormsAuthenticationTicket(
                                1,                              //Ticket Version
                                login.Email,                    //Userindentifizierung
                                DateTime.Now,                   //Zeitpunkt der Erstellung
                                DateTime.Now.AddMinutes(20),    //Gültigkeitsdauer
                                true,                           //persistentes Ticket über Session hinweg
                                login.Role                      //Userrolle(n)
                                );

                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

                System.Web.HttpContext.Current.Response.Cookies.Add(authCookie);

                return RedirectToAction("Index", "Home");
            }

            var person = UserManager.GetPersonByEmail(User.Identity.Name);

            //Session.Add("ID", person.ID);
            //Session.Add("Gamertag", person.Gamertag);
            //Session.Add("CurrencyBalance", person.Currencybalance);
            return RedirectToAction("Error", "Home");
        } 
        #endregion



        /// <summary>
        /// Bei LOGOUT redirect auf 'Home-Seite (Index)'
        /// </summary>
        /// <returns>VIEW</returns>
        #region Actionmethode LOGOUT
        [HttpGet]
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        } 
        #endregion



        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="regUser"></param>
        /// <returns></returns>
        #region Actionmethode REGISTER
        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public ActionResult Register(Register regUser)
        {
            var dbUser = new Person();
            Session.Add("Person", dbUser);

            dbUser.Firstname = regUser.Firstname;
            dbUser.Lastname = regUser.Lastname;

            //hinzugefügte Felder von der Datenbank
            dbUser.Anschrift = regUser.Adresse;
            dbUser.Hausnummer = regUser.Hausnummer;
            dbUser.Ort = regUser.Ort;
            dbUser.PLZ = regUser.PLZ;

            dbUser.Gamertag = regUser.Gamertag;
            dbUser.Email = regUser.Email;
            dbUser.Password = regUser.Password;
            dbUser.Salt = regUser.Salt;
            dbUser.Role = "player";
            dbUser.Currencybalance = 1000;
            dbUser.Isactive = true;

            //LAP Erweiterung
            //dbUser.RegDatum = regUser.Registrierungsdatum;


            //dbUser.tblrole = new List<tblrole>();
            //dbUser.tblrole.Add(new tblrole());
            //dbUser.tblrole.FirstOrDefault().rolename = "user";

            if (AuthManager.Register(dbUser))
            {
                int userID = UserManager.GetPersonByEmail(dbUser.Email).ID;
                if (DeckManager.AddDefaultDecksByUserId(dbUser.ID))
                {
                    // gibt der ActionMethod VerifyRegistration ein neues OBJECT mit gamertag und cuurencybalance mit

                    return RedirectToAction("VerifyRegistration", new { gamertag = dbUser.Gamertag, currencybalance = dbUser.Currencybalance });
                }
            }
            return RedirectToAction("Error", "Home");
        }
        #endregion



        /// <summary>
        /// ActionMethode 'VerifyRegistration' gibt auf VIEW Bestätigung einer erfolgreichen Registrierung mit CURRENCYBALANCE = 1000 aus
        /// </summary>
        /// <param name="gamertag"></param>
        /// <param name="currencybalance"></param>
        /// <returns></returns>
        #region Actionmethode VERIFYREGISTRATION
        [HttpGet]
        [AllowAnonymous]
        public ActionResult VerifyRegistration(string gamertag, int? currencybalance)
        {
            //speichert Gamertag in VIEWBAG
            ViewBag.Gamertag = gamertag;

            //speichert Currencybalance in VIEWBAG
            ViewBag.CurrencyBalance = currencybalance;

            return View();
        } 
        #endregion
    }
}