using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Web.WebPages;

namespace DomainModel.Attribute
{
    public class DateValidationAttribute : ValidationAttribute
    {
        public string NameTag { get; set; }
        public override bool IsValid(object value)
        {
            try
            {
                return value.ToString().IsDateTime();
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
    public class DateValidator : DataAnnotationsModelValidator<DateValidationAttribute>
    {
        public DateValidator(ModelMetadata metadata, ControllerContext context, DateValidationAttribute attribute)
            : base(metadata, context, attribute)
        {
            if (!attribute.IsValid(context.HttpContext.Request.Form[attribute.NameTag]))
            {
                var propertyName = metadata.PropertyName;
                if (context.Controller.ViewData.ModelState[propertyName] != null)
                {
                    context.Controller.ViewData.ModelState[propertyName].Errors.Clear();
                    context.Controller.ViewData.ModelState[propertyName].Errors.Add(attribute.ErrorMessage);
                }
            }
        }
    }
}