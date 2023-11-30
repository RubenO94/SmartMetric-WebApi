using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SmartMetric.Core.Exceptions;

namespace SmartMetric.WebAPI.Filters.ActionFilter
{
    /// <summary>
    /// Custom attribute for request validation in ASP.NET MVC or ASP.NET Core.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class RequestValidationAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Called before the action method, validates the model state, and throws a ValidationException if errors are found.
        /// </summary>
        /// <param name="context">The context in which the action is executed.</param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var failures = context.ModelState
                    .Where(e => e.Value.Errors.Any())
                    .ToDictionary(
                        e => e.Key,
                        e => e.Value?.Errors.Select(x => x.ErrorMessage).FirstOrDefault()
                    );
                if (failures != null)
                {
                    throw new SmartMetric.Core.Exceptions.ValidationException(failures);
                }
            }
        }
    }

}
