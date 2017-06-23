using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using Castle.Windsor;
using Castle.Windsor.Installer;
using KaraSys.App_Start;
using KaraSys.Ioc;
using Logger;
using MyMemCache;

namespace KaraSys
{
    public class MvcApplication : HttpApplication
    {
        private readonly IWindsorContainer _container;

        public IWindsorContainer Container
        {
            get { return _container; }
        }

        public MvcApplication()
        {
            _container = new WindsorContainer();
        }

        private void InstallDependencies()
        {
            _container.Install(FromAssembly.This());
        }

        private void RegisterDependencyResolver()
        {
            //GlobalConfiguration.Configuration.Services.Replace(
            //    typeof (IHttpControllerActivator),
            //    new WindsorHttpControllerActivator(_container));
            GlobalConfiguration.Configuration.DependencyResolver = new WindsorDependencyResolver(_container.Kernel);
        }
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Bootstrapper.Bootstrap(_container);
            RegisterDependencyResolver();
            InstallDependencies();
            // Khởi tạo Cache
            var dateClearCache = DateTime.Now.AddMinutes(MyConfig.MyConfiguration.Cache.Default);
            var ts = dateClearCache - DateTime.Now;
            var cache = new CacheFactory();
            cache.InitCache(new CacheConfigModel(dateClearCache, ts));
        }

        void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            
            // look if any security information exists for this request
            if (HttpContext.Current.User == null) return;

            // see if this user is authenticated, any authenticated cookie (ticket) exists for this user
            if (!HttpContext.Current.User.Identity.IsAuthenticated) return;
            
            // see if the authentication is done using FormsAuthentication
            if (!(HttpContext.Current.User.Identity is FormsIdentity)) return;
            
            // Get the roles stored for this request from the ticket
            // get the identity of the user
            var identity = (FormsIdentity)HttpContext.Current.User.Identity;

            // get the forms authetication ticket of the user
            var ticket = identity.Ticket;

            // get the roles stored as UserData into the ticket
            var roles = ticket.UserData.Split(',');

            // create generic principal and assign it to the current request
            HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(identity, roles);
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown
        }

        void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            Response.Clear();

            CommonLogger.DefaultLogger.Error("Ghi log", exception);
        }

        protected void Application_PostAuthorizeRequest()
        {
            if (IsWebApiRequest())
            {
                HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);
            }
        }

        /// <summary>
        /// Validate request is API or not
        /// </summary>
        /// <returns></returns>
        private bool IsWebApiRequest()
        {
            return HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath != null
                && HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.ToLower().StartsWith("~/api");
        }
    }
}
