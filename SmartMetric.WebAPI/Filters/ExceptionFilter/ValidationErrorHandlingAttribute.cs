using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using SmartMetric.Core.Exceptions;
using System;

namespace SmartMetric.WebAPI.Filters.ExceptionFilter
{
    public class ValidationErrorHandlingAttribute : ExceptionFilterAttribute
    {
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
            else
            {
                // Lógica de tratamento para outras exceções
                HandleGenericException(context);
            }


        }

        private void HandleSecurityTokenSignatureKeyNotFoundException(ExceptionContext context, SecurityTokenSignatureKeyNotFoundException signatureKeyNotFoundException)
        {
            context.Result = new UnauthorizedObjectResult(new
            {
                Message = "Security token signature key not found."
            });
            context.ExceptionHandled = true;
        }

        private void HandleHttpStatusException(ExceptionContext context, HttpStatusException httpStatusException)
        {
            context.Result = new BadRequestObjectResult(new
            {
                Message = "The request contains an error.",
                Details = httpStatusException.Message
            });

            context.ExceptionHandled = true;
        }

        private void HandleValidationException(ExceptionContext context, ValidationException validationException)
        {
            var errors = validationException.Failures.Select(failure => new
            {
                Field = failure.Key,
                Message = failure.Value
            }).ToList();

            context.Result = new BadRequestObjectResult(new
            {
                Message = "The request contains validation errors.",
                Errors = errors
            });

            context.ExceptionHandled = true;
        }

        private void HandleArgumentNullException(ExceptionContext context, ArgumentNullException argumentNullException)
        {
            context.Result = new BadRequestObjectResult(new
            {
                Message = "One or more arguments are null.",
                Details = argumentNullException.Message,
                ArgumentName = argumentNullException.ParamName
            });

            context.ExceptionHandled = true;
        }

        private void HandleArgumentException(ExceptionContext context, ArgumentException argumentException)
        {
            context.Result = new BadRequestObjectResult(new
            {
                Message = "Invalid argument.",
                Details = argumentException.Message.Contains("JWT") ? "Invalid token format" : argumentException.Message,
                ArgumentName = argumentException.ParamName
            });

            context.ExceptionHandled = true;
        }

        private void HandleGenericException(ExceptionContext context)
        {
            context.Result = new ObjectResult(new
            {
                Message = "An unexpected error occurred.",
                StatusCode = StatusCodes.Status500InternalServerError
            })
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

            context.ExceptionHandled = true;
        }
    }
}
