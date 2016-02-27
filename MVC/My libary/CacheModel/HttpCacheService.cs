using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Caching;
using AutoMapper;
using DomainModel;

namespace WorkDataMVC.Models.CacheModel
{
    public class HttpCacheConfig : IHttpCacheConfig
    {
        public DateTime AbsoluteExpiration { get; set; }
        public TimeSpan SlidingExpiration { get; set; }
        public CacheDependency Dependency { get; set; }
        public CacheItemPriority Priority { get; set; }
        public CacheItemRemovedCallback Callback { get; set; }

        public HttpCacheConfig()
        {
            Priority = CacheItemPriority.Default;
        }


    }
    public class HttpCacheService : IHttpCache
    {
        private Cache _cache;

        public HttpCacheService()
        {
            _cache = HttpContext.Current.Cache;

        }

        public TResult TryGetValue<TResult>(string key, Expression<Func<TResult>> value, IHttpCacheConfig config)
        {
            if (_cache[key] != null)
            {
                return (TResult)_cache[key];
            }
            HttpCacheConfig manual = config as HttpCacheConfig;

            if (manual != null)
                _cache.Insert(key, value.Compile().Invoke(), manual.Dependency, manual.AbsoluteExpiration,
                    manual.SlidingExpiration, manual.Priority, manual.Callback);

            return (TResult)_cache[key];
        }

    }
}