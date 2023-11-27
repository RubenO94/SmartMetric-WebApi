using Microsoft.AspNetCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
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

                var type = exception?.GetType();

                if (exception is HttpStatusException errorResponse)
                {
                    code = errorResponse.Status;
                    message = errorResponse.Message;
                }
                else if(exception is ArgumentNullException argumentNullException)
                {
                    code = HttpStatusCode.BadRequest;
                    message = exception.Message;
                }
                else if(exception is ArgumentException argumentException)
                {
                    code = HttpStatusCode.BadRequest;
                    message = $"Invalid argument";
                }
                else if (exception is SecurityTokenSignatureKeyNotFoundException securityTokenSignatureKeyNotFoundException)
                {
                    code = HttpStatusCode.Unauthorized;
                    message = "Access Token invalid";
                }
                else
                {
                    code = HttpStatusCode.InternalServerError;
                    message = "An unexpected error occurred. Please try again later or contact support.";

                }

                var response = new
                {
                    StatusCode = (int)code,
                    Message = message,
                };

                context.Response.StatusCode = (int)code;
                await context.Response.WriteAsJsonAsync(response);

            });
        }
    }
}
