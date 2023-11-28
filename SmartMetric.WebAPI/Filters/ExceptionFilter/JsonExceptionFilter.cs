using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;

namespace SmartMetric.WebAPI.Filters.ExceptionFilter
{
    public class JsonExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is JsonException)
            {
                context.Result = new BadRequestObjectResult("Error processing JSON");
                context.ExceptionHandled = true;
            }
        }
    }
}
