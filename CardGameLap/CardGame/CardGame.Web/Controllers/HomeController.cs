using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CardGame.Web.Models;
using CardGame.DAL.Logic;
using System.Diagnostics;

namespace CardGame.Web.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Index()
        {
            Debug.WriteLine("GET - Home - Index");
            if (User.Identity.Name != "")
            {
                ViewBag.Email = User.Identity.Name;

                var dbUser = UserManager.GetPersonByEmail(User.Identity.Name);

                ViewBag.Firstname = dbUser.Firstname;
                ViewBag.Lastname = dbUser.Lastname;
                ViewBag.Gamertag = dbUser.Gamertag;
            }
            //Eigentlich von DAL auf ViewModel mappen und View dem Viewmodel mitgeben             
            return View();
        }


        /// <summary>
        /// Steht im HomeController
        /// </summary>
        /// <param name="seite"></param>
        /// <returns></returns>
        [Authorize]
        public ActionResult About(int? seite)
        {
            Debug.WriteLine("GET - Home - About");
            ViewBag.Message = "Your application description page.";

            return View(seite ?? 1);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult Contact()
        {
            Debug.WriteLine("GET - Home - Contact");
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize]
        public ActionResult Statistics()
        {
            Debug.WriteLine("GET - Home - Statistics");
            Statistic s = new Statistic();

            //Befülle die Statistik
            s.NumUsers = DBInfoManager.GetNumUsers();
            s.NumCards = DBInfoManager.GetNumCards();
            s.NumDecks = DBInfoManager.GetNumDecks(); 

            return View(s);
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}