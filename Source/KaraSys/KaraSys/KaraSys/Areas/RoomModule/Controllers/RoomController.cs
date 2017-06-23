using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BussinessObject.MembershipModule.Contract;
using BussinessObject.RoomModule.Contract;

namespace KaraSys.Areas.RoomModule.Controllers
{
    public class RoomController : Controller
    {
        #region Variables

        private IRoomBusiness _roomBusiness;

        public RoomController(IRoomBusiness roomBusiness)
        {
            _roomBusiness = roomBusiness;
        }

        #endregion

        public ActionResult Index()
        {
            object rooms = _roomBusiness.GetAllRoom();

            return View(rooms);
        }

        public JsonResult GetAllRooms()
        {
            object rooms = _roomBusiness.GetAllRoom();

            return Json(rooms);
        }
    }
}