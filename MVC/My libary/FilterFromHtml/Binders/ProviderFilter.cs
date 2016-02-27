using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using System.Web.Http.ValueProviders;
using DomainModel.Entity;
using FilterService;
using Newtonsoft.Json;

namespace MvcRegionCity.Binders
{
    public class ExpressionValueProviderFactory : ValueProviderFactory
    {
        public override IValueProvider GetValueProvider(HttpActionContext actionContext)
        {
            return new ProviderFilter(actionContext);
        }
    }
    public class ProviderFilter : IValueProvider
    {
        private NameValueCollection _values;

        public ProviderFilter(HttpActionContext actionContext)
        {
            if (actionContext == null)
            {
                throw new ArgumentNullException("actionContext");
            }
            _values = HttpUtility.ParseQueryString(actionContext.Request.RequestUri.Query);

        }

        public bool ContainsPrefix(string prefix)
        {
            return true;
        }

        public ValueProviderResult GetValue(string key)
        {
            ConditionalGeneratorSimple<Region> generatorR = new ConditionalGeneratorSimple<Region>(_values);
            generatorR.SetKeyValueExpression<string>("nameR", (r, n) => r.RegionName.StartsWith(n,StringComparison.OrdinalIgnoreCase));

            ConditionalGeneratorSimple<City> generatorC = new ConditionalGeneratorSimple<City>(_values);
            generatorC.SetKeyValueExpression<string>("nameC", (r, n) => r.CityName.StartsWith(n,StringComparison.OrdinalIgnoreCase));

            Func<Region, bool> expressionR = generatorR.GetConditional().Compile();
            Func<City, bool> expressionC = generatorC.GetConditional().Compile();
            return new ValueProviderResult(new MulticastDelegate[] {expressionR, expressionC}, key, CultureInfo.CurrentCulture);
        }
    }
}