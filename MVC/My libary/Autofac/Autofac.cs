using System.Data.Entity;
using System.Reflection;
using Autofac;
using Autofac.Builder;
using Autofac.Features.Scanning;
using DomainModel.Context;
using Repository.Common;
using WorkDataMVC.Models.CacheModel;
using Module = Autofac.Module;

namespace WorkDataMVC.Util
{
    public class CacheModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<HttpCacheService>().As<IHttpCache>();
        }
    }
     public class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.Load("Repository"))
                   .Where(t => t.Name.EndsWith("Repository"))
                   .AsImplementedInterfaces()
                  .InstancePerLifetimeScope(); 
        }
    }

    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> list
                = builder.RegisterAssemblyTypes(Assembly.Load("Service")).Where(t => t.Name.EndsWith("Service"));
            list.AsImplementedInterfaces().InstancePerLifetimeScope();
        }
    }

    public class EFModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType(typeof(WorkDbContext)).As(typeof(DbContext)).InstancePerLifetimeScope();
            var u = builder.RegisterType(typeof (UnityOfWork)).As(typeof (IUnityOfWork)).InstancePerRequest();
        }

    }

}