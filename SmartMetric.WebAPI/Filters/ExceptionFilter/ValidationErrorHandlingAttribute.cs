using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using SmartMetric.Core.Exceptions;
using System.Text.Json;

namespace SmartMetric.WebAPI.Filters.ExceptionFilter
{
    /// <summary>
    /// Atributo para tratamento de exceções específicas.
    /// </summary>
    public class ValidationErrorHandlingAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        /// Método chamado ao ocorrer uma exceção, realiza o tratamento específico para diferentes tipos de exceções.
        /// </summary>
        /// <param name="context">O contexto da exceção.</param>
        public override void OnException(ExceptionContext context)
        {
            var type = context.Exception.GetType();

            if (context.Exception is ValidationException validationException)
            {
                HandleValidationException(context, validationException);
            }
            else if (context.Exception is HttpStatusException httpStatusException)
            {
                HandleHttpStatusException(context, httpStatusException);
            }
            else if (context.Exception is SecurityTokenSignatureKeyNotFoundException signatureKeyNotFoundException)
            {
                HandleSecurityTokenSignatureKeyNotFoundException(context, signatureKeyNotFoundException);
            }
            else if (context.Exception is ArgumentNullException argumentNullException)
            {
                HandleArgumentNullException(context, argumentNullException);
            }
            else if (context.Exception is ArgumentException argumentException)
            {
                HandleArgumentException(context, argumentException);
            }
            else if (context.Exception is JsonException)
            {
                context.Result = new BadRequestObjectResult(new { Error = "Error processing JSON" });
                context.ExceptionHandled = true;
            }
            else if(context.Exception is NotImplementedException notImplementedException)
            {
                HandleNotImplementedException(context, notImplementedException);
            }
            else
            {
                // Lógica de tratamento para outras exceções
                HandleGenericException(context);
            }
        }

        private void HandleNotImplementedException(ExceptionContext context, NotImplementedException notImplementedException)
        {
            context.Result = new ObjectResult(new
            {
                Error = "The functionality has not been implemented.",
                StatusCode = StatusCodes.Status501NotImplemented
            })
            {
                StatusCode = StatusCodes.Status501NotImplemented
            };
        }

        // Métodos privados para tratamento específico de cada tipo de exceção...

        private void HandleSecurityTokenSignatureKeyNotFoundException(ExceptionContext context, SecurityTokenSignatureKeyNotFoundException signatureKeyNotFoundException)
        {
            context.Result = new UnauthorizedObjectResult(new
            {
                Error = "Authorization error",
                Details = "Invalid token"
            });
            context.ExceptionHandled = true;
        }

        private void HandleHttpStatusException(ExceptionContext context, HttpStatusException httpStatusException)
        {
            context.Result = new BadRequestObjectResult(new
            {
                Error = "The request contains an error.",
                Details = httpStatusException.Message
            });

            context.ExceptionHandled = true;
        }

        private void HandleValidationException(ExceptionContext context, ValidationException validationException)
        {
            var firstError = validationException.Failures.FirstOrDefault();

            context.Result = new BadRequestObjectResult(new
            {
                Error = firstError.Key,
                Details = firstError.Value
            });

            context.ExceptionHandled = true;
        }

        private void HandleArgumentNullException(ExceptionContext context, ArgumentNullException argumentNullException)
        {
            context.Result = new BadRequestObjectResult(new
            {
                Error = "One or more arguments are null.",
                Details = argumentNullException.Message
            });

            context.ExceptionHandled = true;
        }

        private void HandleArgumentException(ExceptionContext context, ArgumentException argumentException)
        {
            context.Result = new BadRequestObjectResult(new
            {
                Error = "Invalid argument.",
                Details = argumentException.Message.Contains("JWT") ? "Invalid token format" : argumentException.Message
            });

            context.ExceptionHandled = true;
        }

        private void HandleGenericException(ExceptionContext context)
        {
            context.Result = new ObjectResult(new
            {
                Error = "An unexpected error occurred.",
                StatusCode = StatusCodes.Status500InternalServerError
            })
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

            context.ExceptionHandled = true;
        }
    }


}
