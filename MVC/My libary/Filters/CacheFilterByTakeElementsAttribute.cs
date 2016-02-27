using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using DomainModel;
using DomainView;
using WorkDataMVC.Util.Filters;

namespace WorkDataMVC.Util.Filters
{

    public class CacheFilterByTakeElementsAttribute : CacheFilterBaseAttribute
    {
        protected override Expression<Func<IEnumerable<EmployeeView>>> GetElements(IEnumerable<Employee> model)
        {
            return () => Mapper.Map<IEnumerable<EmployeeView>>(model.Take(10).ToList());
        }
    }
}