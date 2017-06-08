using CardGame.DAL.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace CardGame.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }



        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            HttpCookie authCookie = Context.Request.Cookies[FormsAuthentication.FormsCookieName];

            // Abfrage ob COOKIE NULL oder keinen Inhalt hat
            if (authCookie == null || authCookie.Value == "")
                return;

            // neues authTicket erstellen
            FormsAuthenticationTicket authTicket;

            try
            {
                //COOKIE wird entschlüsselt
                authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            }
            catch (Exception)
            {
                return;
            }

            string[] roles = authTicket.UserData.Split(';');

            // bekomme die Daten vom Ticket (login.Role => siehe ACCOUNT-CONTROLLER
            Context.User = new GenericPrincipal(new GenericIdentity(authTicket.Name), roles);
        }


        protected void Session_Start(Object sender, EventArgs e)
        {
            //int temp = 90;

            var person = UserManager.GetPersonByEmail(User.Identity.Name);
            HttpContext.Current.Session.Add("Gamertag", person.Gamertag);
            HttpContext.Current.Session.Add("ID", person.ID);
            HttpContext.Current.Session.Add("CurrencyBalance", person.Currencybalance);
        }
    }
}
