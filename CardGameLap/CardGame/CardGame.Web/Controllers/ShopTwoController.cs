using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CardGame.Web.Controllers
{
    public class ShopTwoController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult Packs()
        {
            try
            {
                
            }
            catch (Exception)
            {

                throw;
            }

            return View();
        }

        public ActionResult Pay()
        {
            return View();
        }
    }
}