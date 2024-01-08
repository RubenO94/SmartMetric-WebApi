using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Enums;
using SmartMetric.Core.Services;
using SmartMetric.Core.ServicesContracts;
using SmartMetric.Infrastructure.Models;
using System.Security.Claims;

namespace SmartMetric.WebAPI.Controllers.v1
{
    /// <summary>
    /// 
    /// </summary>
    [AllowAnonymous]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ISmartTimeService _smartTimeService;
        private readonly IJwtService _jwtService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="smartTimeService"></param>
        public UsersController(ISmartTimeService smartTimeService, IJwtService jwtService)
        {
            _smartTimeService = smartTimeService;
            _jwtService = jwtService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        [HttpGet("Me")]
        public async Task<IActionResult> GetAutenticatedUser()
        {
            var principal = _GetPrincipal();
            if (principal != null)
            {
                var userType = principal.FindFirst("UserType");
                var userId = principal.FindFirst("UserId");

                if (userType != null && userType != null)
                {
                    Enum.TryParse(userType?.Value, out ApplicationUserType applicationUserType);

                    var perfil = await _smartTimeService.GetProfileByUserId(applicationUserType, int.Parse(userId?.Value!));

                    return Ok(perfil);
                }


            }

            throw new ArgumentException("Unidentified user", "Application User");

        }

        private ClaimsPrincipal? _GetPrincipal()
        {
            var authorizationHeader = HttpContext.Request.Headers.Authorization;
            var token = authorizationHeader.ToString().Replace("Bearer ", string.Empty);

            if (!token.IsNullOrEmpty())
            {
                return _jwtService.GetPrincipalFromJwtToken(token);
            }

            return null;
        }
    }
}
