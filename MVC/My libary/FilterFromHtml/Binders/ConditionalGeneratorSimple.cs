using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.ValueProviders;

namespace FilterService
{

    public class ConditionalGeneratorSimple<TItem>
    {

        private NameValueCollection _values;
        private Lazy<IDictionary<string, ContainerExpression>> _expressions =
            new Lazy<IDictionary<string, ContainerExpression>>(() => new Dictionary<string, ContainerExpression>());
        public ConditionalGeneratorSimple(NameValueCollection dic)
        {
            _values = dic;

        }
        public void SetKeyValueExpression<TConst>(string key, Expression<Func<TItem, TConst, bool>> expr)
        {
            _expressions.Value[key] = new ContainerExpression(expr, expr.Parameters[1].Type);
        }

        public virtual Expression<Func<TItem, bool>> GetConditional()
        {
            try
            {
                var items = from item in _expressions.Value
                            where _values[item.Key] != null && _values[item.Key].Any()
                            select new
                            {
                                expr = item.Value.Expression,
                                value = _values[item.Key],
                                type = item.Value.Type
                            };

                Expression<Func<TItem, bool>> predicate = t => true;
                predicate = items.Aggregate(predicate, (pr, item) =>
                    And(pr, Expression.Invoke(item.expr, predicate.Parameters[0], SetupValue(item.value, item.type))));

                return predicate;
            }
            catch (Exception e)
            {
                return t => false;
            }
        }
        protected ConstantExpression SetupValue(string val, Type type)
        {
            var result = Convert.ChangeType(val, type);
            return Expression.Constant(result);
        }
        protected Expression<Func<T, bool>> And<T>(Expression<Func<T, bool>> expr1, InvocationExpression invokedExpr)
        {
            return Expression.Lambda<Func<T, bool>>
                  (Expression.AndAlso(expr1.Body, invokedExpr), expr1.Parameters);
        }
        #region Container for expression
        private struct ContainerExpression
        {
            public Expression Expression { get; set; }
            public Type Type { get; set; }

            public ContainerExpression(Expression expression, Type type)
                : this()
            {
                Expression = expression;
                Type = type;
            }
        }
        #endregion

    }

}

