using System;
using BusinessObject.MembershipModule;
using BusinessObject.MembershipModule.Enums;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessObject.MembershipModule.Contract;
using BusinessObject.MembershipModule.Models.Request;

namespace UnitTestProject
{
    [TestClass]
    public class LogActionAdmin
    {
        private readonly IWindsorContainer _container = new WindsorContainer();

        public LogActionAdmin()
        {
            RegisterLog(_container);
        }

        [TestMethod]
        public void TestLogActionAdmin()
        {
            var ipRequest = WebUtility.WebUitility.GetIpAddressRequest();
            var userAgent = "User Agent Test";

            var _actionAdminBusiness = _container.Resolve<ILogActionAdminBusiness>();

            _actionAdminBusiness.InsertLog(new LogActionAdminRequestModel
            {
                AdminId = 2,
                Description = "Unit Test Run",
                ObjectId = "",
                BeforeConfig = "",
                IpRequest = ipRequest,
                UserAgent = userAgent,
                ActionId = PageFunctionEnum.DieuChinhKhuyenMaiUpdate,
            });
        }

        private static void RegisterLog(IWindsorContainer container)
        {
            container.Register(
                Classes.FromAssemblyNamed("EntitiesObject")
                    .Pick()
                    .WithService.AllInterfaces()
                    .Configure(o => o.LifestylePerThread()));
            container.Register(
                Classes.FromAssemblyNamed("DataAccess")
                    .Pick()
                    .WithService.AllInterfaces()
                    .Configure(o => o.LifestylePerThread()));
            container.Register(
                Classes.FromAssemblyNamed("BusinessObject")
                    .Pick()
                    .WithService.AllInterfaces()
                    .Configure(o => o.LifestylePerThread()));
            //container.Register(
            //     Classes.FromAssemblyNamed("BanCaApi")
            //        .Pick()
            //         .WithServiceAllInterfaces()
            //         .Configure(o => o.LifestylePerThread()));
        }
    }
}
