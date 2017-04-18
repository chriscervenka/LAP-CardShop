﻿using System;
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
        [AllowAnonymous]
        public ActionResult Index()
        {
            if (User.Identity.Name != "")
            {
                ViewBag.Email = User.Identity.Name;

                var dbUser = UserManager.GetPersonByEmail(User.Identity.Name);

                ViewBag.Firstname = dbUser.firstname;
                ViewBag.Lastname = dbUser.lastname;
                ViewBag.Gamertag = dbUser.gamertag;
            }
            //Eigentlich von DAL auf ViewModel mappen und View dem Viewmodel mitgeben
                    
            return View();
        }

        [Authorize(Roles ="player, admin")]
        public ActionResult About(int? seite)
        {
            ViewBag.Message = "Your application description page.";

            return View(seite ?? 1);
        }

        [Authorize(Roles = "player, admin")]
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