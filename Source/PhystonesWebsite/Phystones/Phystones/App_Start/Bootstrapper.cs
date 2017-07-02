using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Phystones.Ioc;

namespace Phystones
{
    public class Bootstrapper
    {
        private static void RegisterServices()
        {
            WindsorRegistrar.RegisterAllFromAssemblies("EntitiesObject");
            WindsorRegistrar.RegisterAllFromAssemblies("DataAccess");
            WindsorRegistrar.RegisterAllFromAssemblies("BusinessObject");
        }
        
        public static void BootstrapApi(IWindsorContainer container)
        {
            container.Register(
                Classes.FromAssemblyNamed("EntitiesObject")
                    .Pick()
                    .WithService.AllInterfaces()
                    .Configure(o => o.LifestylePerWebRequest()));
            container.Register(
                Classes.FromAssemblyNamed("DataAccess")
                    .Pick()
                    .WithService.AllInterfaces()
                    .Configure(o => o.LifestylePerWebRequest()));
            container.Register(
                Classes.FromAssemblyNamed("BusinessObject")
                    .Pick()
                    .WithService.AllInterfaces()
                    .Configure(o => o.LifestylePerWebRequest()));
            container.Register(
             Classes.FromAssemblyNamed("Phystones")
                 .InNamespace("Phystones.Hubs")
                 .WithServiceAllInterfaces()
                 .Configure(o => o.LifestylePerWebRequest()));
        }

        public static void Bootstrap(IWindsorContainer container)
        {
            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(IoC.Container));
            RegisterServices();
            BootstrapApi(container);
        }
    }

    public class WindsorControllerFactory : DefaultControllerFactory
    {
        private readonly IWindsorContainer _container;

        public WindsorControllerFactory(IWindsorContainer container)
        {
            _container = container;
            IEnumerable<Type> controllerTypes =
                from t in Assembly.GetExecutingAssembly().GetTypes()
                where typeof(IController).IsAssignableFrom(t)
                select t;
            foreach (Type t in controllerTypes)
                container.Register(Component.For(t).LifeStyle.PerWebRequest);
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            try
            {
                if (controllerType == null)
                {
                    throw new HttpException(404, "NotFound");
                }
                return (IController)_container.Resolve(controllerType);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public override void ReleaseController(IController controller)
        {
            _container.Release(controller);
            base.ReleaseController(controller);
         }
    }
}