using System;
using System.Linq.Expressions;

namespace WorkDataMVC.Models.CacheModel
{
    public interface IHttpCacheConfig
    {

    }

    public interface IHttpCache
    {
        TResult TryGetValue<TResult>(string key, Expression<Func<TResult>> value,
            IHttpCacheConfig config = null);
    }
}