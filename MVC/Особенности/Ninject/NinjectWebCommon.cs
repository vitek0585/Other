using System;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Common;
using Ninject.Web.WebApi;
using TimePicker;
using TimePicker.Identity;
using TimePicker.Services;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(NinjectWebCommon), "Stop")]

namespace TimePicker
{
    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();
        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel(new IdentityModule());
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                //using Ninject.Web.WebApi; NinjectDependencyResolver
                GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(kernel);
                IoC.SetContainer(kernel.Get);
           
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {

            kernel.Bind<ITaxService>().To<TaxService>();
            //kernel.Bind<ITaxService>().To<TaxService>().;
            kernel.Bind<IIdentityService>().To<IdentityService>().InRequestScope();

        }
    }

    class IdentityModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ApplicationUserManager>()
            .ToSelf()
            .InRequestScope();

            Bind<ApplicationSignInManager>()
              .ToSelf()
              .InRequestScope();

            Bind<ApplicationDbContext>()
              .ToSelf()
              .InRequestScope();

            Bind<IdentityFactoryOptions<ApplicationUserManager>>()
              .ToMethod(x => new IdentityFactoryOptions<ApplicationUserManager>()
              {
                  DataProtectionProvider = Startup.DataProtectionProvider
              });

            Bind<IUserStore<ApplicationUser>>()
             .ToMethod(x => new UserStore<ApplicationUser>(x.Kernel.Get<ApplicationDbContext>()))
             .InRequestScope();

            Bind<IAuthenticationManager>()
              .ToMethod(x => HttpContext.Current.GetOwinContext().Authentication)
              .InRequestScope();
        }
    }
}
