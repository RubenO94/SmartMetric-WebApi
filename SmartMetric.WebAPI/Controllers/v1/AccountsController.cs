using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartMetric.Core.DTO;
using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.Enums;
using SmartMetric.Core.ServicesContracts;
using SmartMetric.WebAPI.Filters.ActionFilters;
using SmartMetric.WebAPI.Filters.AutorizationFilter;
using System.Net;
using System.Security.Claims;

namespace SmartMetric.WebAPI.Controllers.v1
{
    //[AllowAnonymous]
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
        public async Task<IActionResult> GetToken()
        {
            bool? isUtilizador = HttpContext.Items["IsUtilizador"] as bool?;
            string? valueToSearch = HttpContext.Items["Identifier"] as string;
            if (isUtilizador == null || valueToSearch == null)
            {
                return Unauthorized();
            }

            UserDTO? user = (bool)isUtilizador ? await _smartTimeService.GetUserByName(valueToSearch) : await _smartTimeService.GetEmployeeByEmail(valueToSearch);

            if (user == null)
            {
                return NoContent();
            }

            user.ApplicationUserType = (bool)isUtilizador ? ApplicationUserType.User : ApplicationUserType.Employee;

            var authenticationResponse = _jwtService.CreateJwtToken(user);

            user.Token  = authenticationResponse.Token;
            user.Expiration = authenticationResponse.Expiration;

            user.RefreshToken = authenticationResponse.RefreshToken;
            user.RefreshTokenExpiration = authenticationResponse.RefreshTokenExpiration;
            
            var result = await _smartTimeService.UpdateApplicationUser(user);

            return Ok(new ApiResponse<object>()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Token created sucessfuly",
                Data = user
            });
        }


        [HttpPost]
        public async Task<IActionResult> GenerateNewAccessToken(TokenDTO? token)
        {
            //TODO: ApplicationUserType

            if (token == null)
            {
                return BadRequest("Invalid client request");
            }

            ClaimsPrincipal? principal = _jwtService.GetPrincipalFromJwtToken(token.Token);
            if (principal == null)
            {
                return BadRequest("Invalid access token");
            }


            if (Enum.TryParse(typeof(ApplicationUserType), principal.FindFirstValue(ClaimTypes.GivenName), out object? valorEnum))
            {
                ApplicationUserType? applicationUserType = (ApplicationUserType)valorEnum;

                if (applicationUserType == null)
                {
                    return BadRequest("Invalid access token");
                }

                string? id = principal.FindFirstValue(ClaimTypes.NameIdentifier);
                int userId = Convert.ToInt32(id);

                UserDTO? user = applicationUserType == ApplicationUserType.User ? await _smartTimeService.GetUserById(userId) : await _smartTimeService.GetEmployeeById(userId);




                if (user == null || user.RefreshToken != token.RefreshToken || user.RefreshTokenExpiration <= DateTime.Now)
                {
                    return BadRequest("Invalid refresh token");
                }

                AuthenticationResponse authenticationResponse = _jwtService.CreateJwtToken(user);

                user.ApplicationUserType = applicationUserType;
                user.RefreshToken = authenticationResponse.RefreshToken;
                user.RefreshTokenExpiration = authenticationResponse.RefreshTokenExpiration;

                var result = await _smartTimeService.UpdateApplicationUser(user);

                return Ok(authenticationResponse);
            }

            return BadRequest("Invalid access token");
        }
    }
}
