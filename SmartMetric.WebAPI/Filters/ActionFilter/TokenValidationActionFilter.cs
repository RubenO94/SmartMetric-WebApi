using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SmartMetric.Core.DTO.Response;
using System.Security.Claims;
using SmartMetric.Core.ServicesContracts;
using SmartMetric.Core.DTO;

namespace SmartMetric.WebAPI.Filters.ActionFilters
{
    public class TokenValidationActionFilter : IAsyncActionFilter
    {
        private readonly IJwtService _jwtService;
        private readonly ISmartTimeService _smartTimeService;

        public TokenValidationActionFilter(IJwtService jwtService, ISmartTimeService smartTimeService)
        {
            _jwtService = jwtService;
            _smartTimeService = smartTimeService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            bool skipTokenValidation = context.Controller.GetType().GetCustomAttributes(typeof(SkipTokenValidationAttribute), true).Any()
                                 || context.ActionDescriptor.EndpointMetadata.OfType<SkipTokenValidationAttribute>().Any();


            if (!skipTokenValidation)
            {

                bool? isUtilizador = context.HttpContext.Items["isUtilizador"] as bool?;
                if (isUtilizador == null)
                {
                    context.Result = new UnauthorizedObjectResult("User is not defined");
                    return;
                }

                var authorizationHeader = context.HttpContext.Request.Headers.Authorization;
                var acceptHeader = context.HttpContext.Request.Headers.Accept;

                if (!string.IsNullOrEmpty(authorizationHeader) && !string.IsNullOrEmpty(acceptHeader))
                {
                    var token = authorizationHeader.ToString().Replace("Bearer ", string.Empty);
                    var refreshToken = acceptHeader.ToString();

                    ClaimsPrincipal? claimsPrincipal = _jwtService.GetPrincipalFromJwtToken(token);
                    if (claimsPrincipal == null)
                    {
                        context.Result = new UnauthorizedObjectResult("Invalid Access");
                        return;
                    }
                    else
                    {
                        // Check if the token has expired
                        var expirationClaim = claimsPrincipal.FindFirst(ClaimTypes.Expiration);
                        if (expirationClaim != null && DateTime.TryParse(expirationClaim.Value, out var expirationDate))
                        {
                            if (expirationDate <= DateTime.UtcNow)
                            {
                                context.Result = new UnauthorizedObjectResult("Token has expired");
                                return;
                            }
                        }
                        else
                        {
                            context.Result = new UnauthorizedObjectResult("Token is invalid");
                            return;
                        }

                        // Continue with your refresh token and other checks here...

                        string? email = claimsPrincipal?.FindFirstValue(ClaimTypes.Email);
                        UserDTO? user = await _smartTimeService.GetUserByEmail(email);

                        if (user == null || user.RefreshToken != refreshToken ||
                        user.RefreshTokenExpiration <= DateTime.Now)
                        {
                            context.Result = new UnauthorizedObjectResult("Invalid Access, login time-out");
                        }
                        else
                        {
                            AuthenticationResponse authenticationResponse = _jwtService.CreateJwtToken(user);

                            user.RefreshToken = authenticationResponse.RefreshToken;
                            user.RefreshTokenExpiration = authenticationResponse.RefreshTokenExpiration;

                            await _smartTimeService.UpdateApplicationUser(user);

                            context.Result = new OkObjectResult(authenticationResponse);
                        }
                    }
                }
                else
                {
                    context.Result = new BadRequestObjectResult(new
                    {
                        StatusCode = 400,
                        Message = "Request invalid"
                    });
                    return;
                }
            }
            await next(); // Continue a execução da ação
            return;
        }
    }
}
