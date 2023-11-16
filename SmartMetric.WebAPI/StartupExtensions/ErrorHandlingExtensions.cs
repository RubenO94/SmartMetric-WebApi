using Microsoft.AspNetCore.Diagnostics;
using System.Diagnostics;

namespace SmartMetric.WebAPI.StartupExtensions
{
    public static class ErrorHandlingExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();

                var exceptionDetails = context.Features.Get<IExceptionHandlerFeature>();

                var exception = exceptionDetails?.Error;

                logger.LogError(exception, $"Cloud not process the request. TraceId: {Activity.Current?.Id}");

                await Results.Problem(
                    title: "Oops! Something Went Wrong – Our Apologies.",
                    statusCode: StatusCodes.Status500InternalServerError,
                    extensions: new Dictionary<string, object?> {
                        { "traceId", Activity.Current?.Id }
                    }
                    ).ExecuteAsync(context);


            });
        }
    }
}
