using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using DomainModel.Entity;
using FilterService;

namespace MvcRegionCity.Binders
{
    public class BinderExpression:IModelBinder
    {
        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            try
            {
                var values = actionContext.Request.RequestUri.ParseQueryString();

                var generatorR = new ConditionalGeneratorSimple<Region>(values);
                generatorR.SetKeyValueExpression<string>("nameR", (r, n) => r.RegionName.StartsWith(n, StringComparison.OrdinalIgnoreCase));

                var generatorC = new ConditionalGeneratorSimple<City>(values);
                generatorC.SetKeyValueExpression<string>("nameC", (r, n) => r.CityName.StartsWith(n, StringComparison.OrdinalIgnoreCase));

                bindingContext.Model = new MulticastDelegate[] { generatorR.GetConditional().Compile(),
                    generatorC.GetConditional().Compile() };

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}