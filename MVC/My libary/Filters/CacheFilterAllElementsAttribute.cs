using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using DomainModel;
using DomainView;

namespace WorkDataMVC.Util.Filters
{
    public class CacheFilterAllElementsAttribute:CacheFilterBaseAttribute
    {
        protected override Expression<Func<IEnumerable<EmployeeView>>> GetElements(IEnumerable<Employee> model)
        {
            return () => Mapper.Map<IEnumerable<EmployeeView>>(model.ToList());

        }
    }
}