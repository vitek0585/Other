using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using DomainModel;
using Repository;
using Repository.Common;
using Service;
using Service.Common;
using Service.Interfaces;

namespace WorkDataMVC.Util.Binder
{
    public class BinderEmployee : BinderBase<Employee, int>
    {
        public BinderEmployee ()
            : base(service: DependencyResolver.Current.GetService<IEmployeeService>())
        {

        }
    }
}