using Microsoft.AspNetCore.Diagnostics;
using SmartMetric.Core.Exceptions;
using System.Diagnostics;
using System.Net;

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


                HttpStatusCode code;

                if (exception is HttpStatusException errorResponse)
                {
                    code = errorResponse.Status;
                }
                else
                {
                    code = HttpStatusCode.InternalServerError;
                }

                var response = new
                {
                    code = (int)code,
                    error = exception?.Message,
                };

                await context.Response.WriteAsJsonAsync(response);

            });
        }
    }
}
