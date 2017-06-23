using System.Web.Mvc;
using KaraSys.Areas.RoomModule.Models.Enums;
using MyUtility.Extensions;

namespace KaraSys.Areas.RoomModule
{
    public class RoomModuleAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "RoomModule";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                RoomRouteEnum.QuanLyPhong.Text(),
                "quan-ly-phong",
                new { controller = "Room", action = "Index" }
            );

            context.MapRoute(
                "RoomModule_default",
                "RoomModule/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}