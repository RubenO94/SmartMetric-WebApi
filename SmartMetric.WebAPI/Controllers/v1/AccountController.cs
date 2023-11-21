using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmartMetric.Core.Domain.Identity;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Core.DTO;
using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.ServicesContracts;
using SmartMetric.Infrastructure.Models;
using System.Security.Claims;

namespace SmartMetric.WebAPI.Controllers.v1
{
    [AllowAnonymous]
    [ApiVersion("1.0")]
    public class AccountController : CustomBaseController
    {
        private readonly IJwtService _jwtService;
        private readonly ISmartTimeRepository _smartTimeRepository;

        public AccountController(IJwtService jwtService, ISmartTimeRepository smartTimeRepository)
        {
            _jwtService = jwtService;
            _smartTimeRepository = smartTimeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetToken()
        {
            Utilizador? user = await _smartTimeRepository.GetUser(49);

            if (user == null)
            {
                return NoContent();
            }

            var authenticationResponse = _jwtService.CreateJwtToken(user);

            user.RefreshToken = authenticationResponse.RefreshToken;

            user.RefreshTokenExpiration = authenticationResponse.RefreshTokenExpiration;
            var result = await _smartTimeRepository.UpdateUser(user);

            return Ok(authenticationResponse);
        }


        [HttpPost("generate-new-token")]
        public async Task<IActionResult> GenerateNewAccessToken(TokenDTO tokenDTO)
        {
            if (tokenDTO == null)
            {
                return BadRequest("Invalid client request");
            }

            ClaimsPrincipal? principal = _jwtService.GetPrincipalFromJwtToken(tokenDTO.Token);
            if (principal == null)
            {
                return BadRequest("Invalid jwt access token");
            }

            string? id = principal.FindFirstValue(ClaimTypes.NameIdentifier);
            var idUtilizador = Convert.ToInt32(id);

            Utilizador? user = await _smartTimeRepository.GetUser(idUtilizador);

            if (user == null || user.RefreshToken != tokenDTO.RefreshToken || user.RefreshTokenExpiration <= DateTime.Now)
            {
                return BadRequest("Invalid refresh token");
            }

            AuthenticationResponse authenticationResponse = _jwtService.CreateJwtToken(user);

            user.RefreshToken = authenticationResponse.RefreshToken;
            user.RefreshTokenExpiration = authenticationResponse.RefreshTokenExpiration;

            var result = await _smartTimeRepository.UpdateUser(user);

            return Ok(authenticationResponse);
        }
    }
}
