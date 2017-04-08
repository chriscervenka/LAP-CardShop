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
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
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
                                "admin" //login.Role            //Userrolle(n)
                                );

                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

                System.Web.HttpContext.Current.Response.Cookies.Add(authCookie);
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
        public ActionResult Register(Register regUser)
        {
            var dbUser = new tblperson();

            dbUser.firstname = regUser.Firstname;
            dbUser.lastname = regUser.Lastname;
            dbUser.gamertag = regUser.Gamertag;
            dbUser.email = regUser.Email;
            dbUser.password = regUser.Password;
            dbUser.salt = regUser.Salt;
            dbUser.userrole = "admin";
            dbUser.currencybalance = 1000;
            dbUser.isactive = true;

            //dbUser.tblrole = new List<tblrole>();
            //dbUser.tblrole.Add(new tblrole());
            //dbUser.tblrole.FirstOrDefault().rolename = "user";

            AuthManager.Register(dbUser);

            return RedirectToAction("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult VerifyRegistration(tblperson regUser)
        {
            if (ModelState.IsValid)
            {
                using (ClonestoneFSEntities db = new ClonestoneFSEntities())
                {
                    db.tblperson.Add(regUser);
                    db.SaveChanges();
                    ModelState.Clear();
                    regUser = null;
                    ViewBag.Message = "succeed";
                }

            }
            return View();

        }
    }
}