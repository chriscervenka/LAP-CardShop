using System.Collections.Generic;
using System.Web.Mvc;
using CardGame.DAL.Logic;
using CardGame.Web.Models;

namespace CardGame.Web.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        [HttpGet]
        [Authorize(Roles = "player, admin")]
        public ActionResult Index()
        {
            List<User> UserList = new List<User>();

            var dbUserlist = UserManager.GetAllUser();

            //var asd = new tblrole();
            

            foreach (var c in dbUserlist)
            {
                Register user = new Register();
                user.ID = c.idperson;
                user.Firstname = c.firstname;
                user.Lastname = c.lastname;
                user.Gamertag = c.gamertag;
                user.Email = c.email;
                user.Role = c.userrole;
                user.CurrencyBalance = (int)c.currencybalance;  //Konvertiere einen NULLABLE int64 in einen 'normalen' int64 (int)
                user.Password = c.password;
                user.Salt = c.salt;

                UserList.Add(user);
            }
            return View(UserList);
        }

    }
}