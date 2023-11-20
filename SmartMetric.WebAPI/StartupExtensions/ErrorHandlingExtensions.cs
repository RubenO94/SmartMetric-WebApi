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
                string message;

                if (exception is HttpStatusException errorResponse)
                {
                    code = errorResponse.Status;
                    message = errorResponse.Message;
                }
                else
                {
                    code = HttpStatusCode.InternalServerError;
                    message = "An unexpected error occurred. Please try again later or contact support.";

                }

                var response = new
                {
                    StatusCode = (int)code,
                    ErrorMessage = exception?.Message,
                    TraceId = Activity.Current?.Id
                };

                context.Response.StatusCode = (int)code;
                await context.Response.WriteAsJsonAsync(response);

            });
        }
    }
}
