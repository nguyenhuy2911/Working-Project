using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Autofac.Integration.Mvc;
using Ecommerce.Domain.Infrastructure;

namespace Ecommerce.Web
{
    public static class Bootstrapper
    {
        public static void Run()
        {
            SetAutofacContainer();
        }

        private static void SetAutofacContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<DatabaseFactory>().As<IDatabaseFactory>().InstancePerLifetimeScope();
        }
    }
}