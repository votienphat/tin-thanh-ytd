using System;
using System.Web;
using Castle.Windsor;

namespace KaraSys.Ioc
{
    public static class IoC
    {
        private static readonly object LockObj = new object();

        private static IWindsorContainer _container = new WindsorContainer();

        public static IWindsorContainer Container
        {
            get { return _container; }

            set
            {
                lock (LockObj)
                {
                    _container = value;
                }
            }
        }

        public static T Resolve<T>()
        {
            if (HttpContext.Current == null)
            {
                HttpContext.Current = new HttpContext(
                    new HttpRequest(null, "http://tempuri.org", null),
                    new HttpResponse(null));
            }

            return _container.Resolve<T>();

        }

        public static object Resolve(Type type)
        {
            return _container.Resolve(type);
        }
    }
}