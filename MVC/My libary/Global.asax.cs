using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using DomainModel;
using DomainModel.Attribute;
using Repository.Common;
using Service;
using StackExchange.Profiling;
using StackExchange.Profiling.EntityFramework6;
using WorkDataMVC.Models.CacheModel;
using WorkDataMVC.Util;
using WorkDataMVC.Util.Binder;
using WorkDataMVC.Util.ModelMapper;


namespace WorkDataMVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
     
        protected void Application_Start()
        {
            MiniProfilerEF6.Initialize();

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //для валидации
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(DateValidationAttribute), typeof(DateValidator));

            DependencyResolver.SetResolver(new AutofacDependencyResolver(AutofacBuilder.GetContainer()));
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(AutofacBuilder.GetContainer());

            SqlDependencyAdmin.InitialSqlDependencyDataBase();
            ModelMapper.InitializeConfigMapper();
        }

        private void Application_BeginRequest(object sender, EventArgs e)
        {
            if (Request.IsLocal)
            {
                MiniProfiler.Start();
            }
        }
        private void Application_EndRequest(object sender, EventArgs e)
        {
            MiniProfiler.Stop();
        }
    }
}
