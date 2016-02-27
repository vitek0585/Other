using System;
using System.Web.Http;

using System.Web.Mvc;

namespace MvcRegionCity.Binders
{
    public class BinderMvc:DefaultModelBinder
    {
        public BinderMvc()
        {
            
        }
        protected override object CreateModel(ControllerContext controllerContext, ModelBindingContext bindingContext, Type modelType)
        {
            return base.CreateModel(controllerContext, bindingContext, modelType);
        }
    }
}