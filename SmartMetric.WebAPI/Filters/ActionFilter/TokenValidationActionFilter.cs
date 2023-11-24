using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SmartMetric.Core.DTO.Response;
using System.Security.Claims;
using SmartMetric.Core.ServicesContracts;
using SmartMetric.Core.DTO;
using System.IdentityModel.Tokens.Jwt;
using SmartMetric.Core.Enums;
using SmartMetric.Core.Exceptions;

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
                        var expirationClaim = claimsPrincipal.FindFirst(JwtRegisteredClaimNames.Exp);
                        if (expirationClaim != null && long.TryParse(expirationClaim.Value, out var expirationUnixTime))
                        {
                            var expirationDateTime = DateTimeOffset.FromUnixTimeSeconds(expirationUnixTime).UtcDateTime;

                            if (expirationDateTime <= DateTime.UtcNow)
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

                        if (Enum.TryParse(typeof(ApplicationUserType), claimsPrincipal.FindFirstValue(ClaimTypes.GivenName), out object? valorEnum))
                        {
                            ApplicationUserType? applicationUserType = (ApplicationUserType)valorEnum;

                            if (applicationUserType == null)
                            {
                                context.Result = new UnauthorizedObjectResult("Invalid access token");
                            }

                            string? id = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
                            int userId = Convert.ToInt32(id);

                            UserDTO? user = applicationUserType == ApplicationUserType.User ? await _smartTimeService.GetUserById(userId) : await _smartTimeService.GetEmployeeById(userId);

                            if (user == null || user.RefreshTokenExpiration <= DateTime.Now)
                            {
                                context.Result = new UnauthorizedObjectResult("Invalid Access, login time-out");
                            }
                            else
                            {
                                AuthenticationResponse authenticationResponse = _jwtService.CreateJwtToken(user);

                                user.RefreshToken = authenticationResponse.RefreshToken;
                                user.RefreshTokenExpiration = authenticationResponse.RefreshTokenExpiration;
                                user.ApplicationUserType = applicationUserType;

                                await _smartTimeService.UpdateApplicationUser(user);

                                //context.Result = new OkObjectResult(authenticationResponse);
                            }

                        }
                    }
                }
                else
                {
                    throw new HttpStatusException(System.Net.HttpStatusCode.Unauthorized, "Access Token is missing");

                }
            }
            await next(); // Continue a execução da ação
        }
    }
}
