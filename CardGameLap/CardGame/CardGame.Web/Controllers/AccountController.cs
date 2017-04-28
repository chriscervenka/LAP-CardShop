using System;
using System.Web;
using System.Web.Mvc;
using CardGame.Web.Models;
using CardGame.DAL.Logic;
using CardGame.DAL.Model;
using System.Web.Security;


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

        [HttpPost]
        [AllowAnonymous]
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

                //return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Register regUser)
        {
            var dbUser = new tblperson();
            Session.Add("Person", dbUser);

            dbUser.firstname = regUser.Firstname;
            dbUser.lastname = regUser.Lastname;
            dbUser.gamertag = regUser.Gamertag;
            dbUser.email = regUser.Email;
            dbUser.password = regUser.Password;
            dbUser.salt = regUser.Salt;
            dbUser.userrole = "player";
            dbUser.currencybalance = 1000;
            dbUser.isactive = true;

            //dbUser.tblrole = new List<tblrole>();
            //dbUser.tblrole.Add(new tblrole());
            //dbUser.tblrole.FirstOrDefault().rolename = "user";

            AuthManager.Register(dbUser);

            // gibt der ActionMethod VerifyRegistration ein neues OBJECT mit gamertag und cuurencybalance mit
            return RedirectToAction("VerifyRegistration", new { gamertag = dbUser.gamertag, currencybalance = dbUser.currencybalance });
        }

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

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult VerifyRegistration(Register regUser)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View("Registration");
        //    }
        //    return RedirectToAction("VerifyRegistration");

        //}
    }
}