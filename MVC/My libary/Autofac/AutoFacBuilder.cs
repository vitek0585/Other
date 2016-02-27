using System.Reflection;
using System.Web.Mvc;
using Autofac;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using DomainModel;
using WorkDataMVC.Util.Binder;

namespace WorkDataMVC.Util
{
    public static class AutofacBuilder
    {
        public static ILifetimeScope GetContainer()
        {
            var build = new ContainerBuilder();

            Assembly a = typeof(MvcApplication).Assembly;

            build.RegisterControllers(a).PropertiesAutowired();
            build.RegisterApiControllers(typeof (MvcApplication).Assembly);

            build.RegisterModule(new RepositoryModule());
            build.RegisterModule(new ServiceModule());
            build.RegisterModule(new EFModule());

            build.RegisterModule(new CacheModule());

            return build.Build();

        }

    }
}


