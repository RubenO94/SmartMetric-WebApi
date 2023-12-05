using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SmartMetric.Core.DTO.Response;
using System.Collections;
using System.Collections.Generic;

namespace SmartMetric.WebAPI.Filters.ActionFilter
{
    public class AddTotalCountHeaderAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultContext = await next();

            if (context.HttpContext.Request.Query.ContainsKey("page") &&
                context.HttpContext.Request.Query.ContainsKey("pageSize"))
            {
                if (resultContext.Result is ObjectResult objectResult && objectResult.Value is ApiResponse<object> apiResponse)
                {
                    var listCount = GetListCount(apiResponse.Data);
                    resultContext.HttpContext.Response.Headers.Add("X-Total-Count", listCount.ToString());
                }
            }

        }

        private int GetListCount(object? data)
        {
            if (data is IEnumerable enumerable)
            {
                int count = 0;

                foreach (var item in enumerable)
                {
                    count++;
                }

                return count;
            }

            return 0;
        }
    }
}
