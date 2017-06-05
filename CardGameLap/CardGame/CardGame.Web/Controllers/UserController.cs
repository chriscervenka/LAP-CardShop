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
        [Authorize]
        public ActionResult Index()
        {
            List<User> UserList = new List<User>();

            var dbUserlist = UserManager.GetAllUser();

            //var asd = new tblrole();
            

            foreach (var c in dbUserlist)
            {
                Register user = new Register();
                user.ID = c.ID;
                user.Firstname = c.Firstname;
                user.Lastname = c.Lastname;

                //hinzugefügte FELDER von der Datenbank
                user.Adresse = c.Anschrift;
                user.Ort = c.Ort;
                user.PLZ = c.PLZ;
                user.Hausnummer = c.Hausnummer;

                user.Gamertag = c.Gamertag;
                user.Email = c.Email;
                user.Role = c.Role;
                user.CurrencyBalance = (int)c.Currencybalance;  //Konvertiere einen NULLABLE int64 in einen 'normalen' int64 (int)
                user.Password = c.Password;
                user.Salt = c.Salt;

                UserList.Add(user);
            }
            return View(UserList);
        }

    }
}