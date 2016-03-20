using System;
using Ninject.Parameters;
using TimePicker.Services;

namespace TimePicker
{
    public class IoC
    {

        private static Func<Type,IParameter[],object> _container;

        private IoC()
        {
        }
        public static TService Resolve<TService>(params IParameter[] parameters)
        {
            return (TService)_container(typeof(TService),parameters);
        }

        internal static void SetContainer(Func<Type,IParameter[], object> container)
        {
            _container = container;
        }
    }
}