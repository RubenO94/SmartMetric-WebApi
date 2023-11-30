using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartMetric.Core.DTO;
using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.Enums;
using SmartMetric.Core.ServicesContracts;
using SmartMetric.Infrastructure.Models;
using SmartMetric.WebAPI.Filters.ActionFilters;
using SmartMetric.WebAPI.Filters.AutorizationFilter;
using System.Net;
using System.Security.Claims;

namespace SmartMetric.WebAPI.Controllers.v1
{
    [AllowAnonymous]
    [ApiVersion("1.0")]
    [SkipTokenValidation]
    public class AccountsController : CustomBaseController
    {
        private readonly IJwtService _jwtService;
        private readonly ISmartTimeService _smartTimeService;

        public AccountsController(IJwtService jwtService, ISmartTimeService smartTimeService)
        {
            _jwtService = jwtService;
            _smartTimeService = smartTimeService;
        }

        [HttpGet]
        [TokenAuthorizationFilter]
        public async Task<IActionResult> SignIn([FromQuery] string? token)
        {

            if (HttpContext.Items.TryGetValue("ApplicationUserType", out var userTypeObj) && userTypeObj is ApplicationUserType applicationUserType)
            {
                string? valueToSearch = HttpContext.Items["ToSearchBy"] as string;

                UserDTO? user = applicationUserType == ApplicationUserType.User ? await _smartTimeService.GetUserByName(valueToSearch) : await _smartTimeService.GetEmployeeByEmail(valueToSearch);

                if (user == null)
                {
                    return NoContent();
                }

                user.ApplicationUserType = applicationUserType;

                var authenticationResponse = _jwtService.CreateJwtToken(user);

                user.Token = authenticationResponse.Token;
                user.Expiration = authenticationResponse.Expiration;

                user.RefreshToken = authenticationResponse.RefreshToken;
                user.RefreshTokenExpiration = authenticationResponse.RefreshTokenExpiration;

                await _smartTimeService.UpdateApplicationUser(user);

                return Ok(new ApiResponse<object>()
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "Token created sucessfuly",
                    Data = new
                    {
                        token = authenticationResponse.Token,
                        refreshToken = authenticationResponse.RefreshToken,
                    }
                });

            }
            else
            {
                return Unauthorized(new
                {
                    error = "Unidentified user"
                });
            }

            
        }


        [HttpPost]
        public async Task<IActionResult> GenerateNewAccessToken(TokenDTO? token)
        {
            //TODO: ApplicationUserType

            if (token == null)
            {
                throw new ArgumentNullException(nameof(token), "Invalid client request");
            }

            ClaimsPrincipal? principal = _jwtService.GetPrincipalFromJwtToken(token.Token);
            if (principal == null)
            {
                throw new ArgumentException("Access Token", "Invalid access token");
            }


            if (Enum.TryParse(typeof(ApplicationUserType), principal.FindFirstValue(ClaimTypes.GivenName), out object? valorEnum))
            {
                ApplicationUserType? applicationUserType = (ApplicationUserType)valorEnum;

                if (applicationUserType == null)
                {
                    throw new ArgumentException("Access Token", "Invalid access token");
                }

                string? id = principal.FindFirstValue(ClaimTypes.NameIdentifier);
                int userId = Convert.ToInt32(id);

                UserDTO? user = applicationUserType == ApplicationUserType.User ? await _smartTimeService.GetUserById(userId) : await _smartTimeService.GetEmployeeById(userId);




                if (user == null || user.RefreshToken != token.RefreshToken || user.RefreshTokenExpiration <= DateTime.Now)
                {
                    throw new ArgumentException("Refresh Token", "Invalid refresh token");
                }

                AuthenticationResponse authenticationResponse = _jwtService.CreateJwtToken(user);

                user.ApplicationUserType = applicationUserType;
                user.RefreshToken = authenticationResponse.RefreshToken;
                user.RefreshTokenExpiration = authenticationResponse.RefreshTokenExpiration;

                await _smartTimeService.UpdateApplicationUser(user);

                return Ok(new ApiResponse<object>()
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "New Token created sucessfuly",
                    Data = new
                    {
                        token = authenticationResponse.Token,
                        refreshToken = authenticationResponse.RefreshToken,
                    }
                });
            }

            throw new ArgumentException("Access Token", "Invalid access token");
        }
    }
}
