using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SmartMetric.Core.Exceptions;

namespace SmartMetric.WebAPI.Filters.ExceptionFilter
{
    public class ValidationErrorHandlingAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is ValidationException validationException)
            {
                var errors = new List<object>();

                foreach (var failure in validationException.Failures)
                {
                    errors.Add(new
                    {
                        Field = failure.Key,
                        Message = failure.Value
                    });
                }

                context.Result = new BadRequestObjectResult(new
                {
                    Message = "The request contains validation errors.",
                    Errors = errors
                });
                context.ExceptionHandled = true;
            }
        }
    }
}
