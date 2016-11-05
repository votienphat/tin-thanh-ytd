using System.Web.Mvc;
namespace MyAdmin.Areas.MembershipModule
{
    public class MembershipModuleAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "MembershipModule";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "MembershipModule",
                "MembershipModule/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}