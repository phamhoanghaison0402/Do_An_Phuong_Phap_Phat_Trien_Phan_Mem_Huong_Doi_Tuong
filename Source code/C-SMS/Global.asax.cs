using System;
using System.Security.Principal;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace WebBanHang
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Application["PageView"] = 0;
            Application["Online"] = 0;

        }

        protected void Session_Start()
        {
            Application.Lock();
            Application["PageView"] = (int)Application["PageView"] + 1;
            Application["Online"] = (int)Application["Online"] + 1;
            Application.UnLock();
        }
        protected void Session_End()
        {
            Application.Lock();
            Application["Online"] = (int)Application["Online"] - 1;
            Application.UnLock();
        }
        public void Application_End()
        {
            Application.Lock();
            Application["Online"] = (int)Application["Online"] - 1;
            Application.UnLock();
        }
        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
       {
            var AccCookie = Context.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (AccCookie != null)
            {
                var authTicket = FormsAuthentication.Decrypt(AccCookie.Value);
                var Aut = authTicket.UserData.Split(new Char[] { ',' });
                var userPrincipal = new GenericPrincipal(new GenericIdentity(authTicket.Name), Aut);
                Context.User = userPrincipal;
            }
        }
    }
}
