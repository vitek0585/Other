using System;
using System.Diagnostics.Contracts;
//using System.Diagnostics.Contracts;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Autofac.Integration.Mvc;
using DomainModel;
using DomainModel.Common;
using Service;
using Service.Common;
using Service.Interfaces;
using WorkDataMVC.Util.Exceptions;

namespace WorkDataMVC.Util.Binder
{

    public class BinderBase<T, TId> : IModelBinder where T : EntityBase
    {
        private readonly IEntityService<T, TId> _service;

        public BinderBase(IEntityService<T, TId> service)
        {
            _service = service;
        }

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            try
            {
                var id = bindingContext.ValueProvider.GetValue("id");
                
                if (id == null)
                    throw new HttpBinderException("Bad request", HttpStatusCode.BadRequest);

                var tId = (TId)id.ConvertTo(typeof(TId));
                var item = _service.GetById(tId);

                if (item == null)
                    throw new HttpBinderException("Not Found", HttpStatusCode.NotFound);
                
                return item;
            }
            catch (HttpBinderException ex)
            {
                throw ex;
            }
            catch (InvalidOperationException ex)
            {
                throw new HttpBinderException("Bad request", HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.GetType().ToString(), ex);
            }
        }
    }
}