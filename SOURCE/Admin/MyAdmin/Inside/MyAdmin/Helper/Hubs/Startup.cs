using MyAdmin.Helper.Hubs;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace MyAdmin.Helper.Hubs
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
            //GlobalHost.DependencyResolver.Register(typeof(IUserIdProvider), () => new CustomUserIdProvider());
            app.MapSignalR();
        }
    }
}
