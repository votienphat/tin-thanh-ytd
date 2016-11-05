using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessObject.MembershipModule.Contract;
using BusinessObject.MembershipModule.Enums;
using BusinessObject.MembershipModule.Models.Request;
using MyAdmin.Ioc;

namespace MyAdmin.Helper
{
    public class LogActionAdminHelper
    {
        private static ILogActionAdminBusiness _actionAdminBusiness = IoC.Resolve<ILogActionAdminBusiness>();

        public LogActionAdminHelper(ILogActionAdminBusiness actionAdminBusiness)
        {
            _actionAdminBusiness = actionAdminBusiness;
        }

        /// <summary>
        /// <para>Author: TrungTT</para>
        /// <para>Date: 2016-01-20</para>
        /// <para>Description: Ghi log thao tac cua admin</para>
        /// </summary>
        /// <param name="controllerContext">ControllerContext: la property cua Controller</param>
        /// <param name="model">Tham so can truyen vao de ghi log</param>
        /// <returns></returns>
        public static LogActionAdminStatus Log(ControllerContext controllerContext, LogActionAdminModel model)
        {
            //var actionController = controllerContext.RouteData.Values["Controller"].ToString();
            //var actionName = controllerContext.RouteData.Values["Action"].ToString();

            //var actionLink = actionController + "/" + actionName;

            var ipRequest = WebUtility.WebUitility.GetIpAddressRequest();
            var userAgent = controllerContext.HttpContext.Request.UserAgent;

            return _actionAdminBusiness.InsertLog(new LogActionAdminRequestModel
            {
                AdminId = model.AdminId,
                Description = model.Description,
                ActionId = model.ActionId,
                ObjectId = model.ObjectId,
                Config = model.BeforeConfig,
                IpRequest = ipRequest,
                UserAgent = userAgent
            });
        }
    }
}