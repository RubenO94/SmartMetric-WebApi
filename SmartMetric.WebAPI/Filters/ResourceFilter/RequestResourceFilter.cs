using Microsoft.AspNetCore.Mvc.Filters;

namespace SmartMetric.WebAPI.Filters.ResourceFilter
{
    public class RequestResourceFilter : IResourceFilter
    {
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            throw new NotImplementedException();
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            var result = context;
        }
    }
}
