using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CardGame.Web.Models;
using CardGame.DAL.Logic;

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
            if (User.Identity.Name != "")
            {
                ViewBag.Email = User.Identity.Name;

                var dbUser = UserManager.GetPersonByEmail(User.Identity.Name);

                ViewBag.Firstname = dbUser.FirstName;
                ViewBag.Lastname = dbUser.LastName;
                ViewBag.Gamertag = dbUser.GamerTag;
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
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Statistics()
        {
            Statistic s = new Statistic();

            //Befülle die Statistik
            s.NumUsers = DBInfoManager.GetNumUsers();
            s.NumCards = DBInfoManager.GetNumCards();
            s.NumDecks = DBInfoManager.GetNumDecks(); 

            return View(s);
        }


    }
}