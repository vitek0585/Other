using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Caching;
using System.Web.Mvc;
using AutoMapper;
using DomainModel;
using DomainView;
using WorkDataMVC.Models.CacheModel;

namespace WorkDataMVC.Util.Filters
{
    public abstract class CacheFilterBaseAttribute : FilterAttribute, IActionFilter
    {
        protected IHttpCache _cache;
        public string DbName { get; set; }
        public string TableName { get; set; }
        /// <summary>
        /// Cache.NoAbsoluteExpiration = Max Date
        /// </summary>
        public int AbsoluteExpirationSec { get; set; }
        /// <summary>
        /// Cache.NoSlidingExpiration = 0
        /// </summary>
        public int SlidingExpiration { get; set; }
        public bool IsSqlDependency { get; set; }
        private CacheDependency _dependency;
        public CacheItemPriority Priority { get; set; }
        public CacheItemRemovedCallback Callback { get; set; }

        public CacheFilterBaseAttribute()
        {
            _cache = DependencyResolver.Current.GetService<IHttpCache>();
            Priority = CacheItemPriority.Default;
        }
        public virtual void OnActionExecuting(ActionExecutingContext filterContext)
        {
        }

        protected abstract Expression<Func<IEnumerable<EmployeeView>>> GetElements(IEnumerable<Employee> model);
        public virtual void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (IsSqlDependency)
                _dependency = new SqlCacheDependency(DbName, TableName);

            try
            {
                var model = (IEnumerable<Employee>)(filterContext.Result as ViewResult).Model;
                var data = _cache.TryGetValue("index",GetElements(model),
                    new HttpCacheConfig()
                    {
                        Dependency = _dependency,
                        AbsoluteExpiration = DateTime.Now.AddSeconds(this.AbsoluteExpirationSec),
                        SlidingExpiration = TimeSpan.FromSeconds(this.SlidingExpiration),
                        Callback = this.Callback,
                        Priority = this.Priority
                    });

                filterContext.Controller.ViewData.Model = data;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}