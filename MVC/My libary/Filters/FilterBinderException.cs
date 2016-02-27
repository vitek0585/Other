using System;
using System.Diagnostics.Contracts;
using System.Web;
using System.Web.Mvc;
using WorkDataMVC.Util.Binder;
using WorkDataMVC.Util.Exceptions;

namespace WorkDataMVC.Util.Filters
{
    public class FilterBinderExceptionAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled)
            {
                filterContext.Result = new ViewResult()
                {
                    ViewName = "Errors",
                    ViewData = new ViewDataDictionary(filterContext.Exception.Message)
                };

                if (filterContext.Exception is HttpBinderException)
                    filterContext.HttpContext.Response.StatusCode = 
                        (int) ((HttpBinderException)filterContext.Exception).StatusCode;
                filterContext.ExceptionHandled = true;
            }
        }
    }
}