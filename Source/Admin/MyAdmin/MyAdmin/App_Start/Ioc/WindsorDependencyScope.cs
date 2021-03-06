﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;
using Castle.MicroKernel;
using Castle.MicroKernel.Lifestyle;

namespace MyAdmin.Ioc
{
    public class WindsorDependencyScope : IDependencyScope
    {
        private readonly IKernel container;

        //private readonly IDependencyResolver resolver;

        private readonly IDisposable scope;

        public WindsorDependencyScope(IKernel container)
        {
            this.container = container;
            scope = container.BeginScope();
        }

        public object GetService(Type serviceType)
        {
            return container.HasComponent(serviceType) ? container.Resolve(serviceType) : null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return container.ResolveAll(serviceType).Cast<object>();
        }

        public void Dispose()
        {
            scope.Dispose();
        }
    }
}