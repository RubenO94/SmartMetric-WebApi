using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.IdentityModel.Tokens;
using SmartMetric.Core.DTO;
using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.Enums;
using SmartMetric.Core.ServicesContracts;
using SmartMetric.Infrastructure.Models;
using SmartMetric.WebAPI.Filters.ActionFilter;
using SmartMetric.WebAPI.Filters.ActionFilters;
using SmartMetric.WebAPI.Filters.AutorizationFilter;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SmartMetric.WebAPI.Controllers.v1
{
    /// <summary>
    /// Controlador responsável por operações de autenticação e geração de tokens.
    /// </summary>
    [AllowAnonymous]
    [ApiVersion("1.0")]
    [SkipTokenValidation]
    public class AccountsController : CustomBaseController
    {
        private readonly IJwtService _jwtService;
        private readonly ISmartTimeService _smartTimeService;

        /// <summary>
        /// Construtor do controlador Accounts.
        /// </summary>
        /// <param name="jwtService">Serviço JWT para operações de autenticação.</param>
        /// <param name="smartTimeService">Serviço SmartTime para operações administrativas.</param>
        public AccountsController(IJwtService jwtService, ISmartTimeService smartTimeService)
        {
            _jwtService = jwtService;
            _smartTimeService = smartTimeService;
        }

        /// <summary>
        /// DEV TOOL: Apenas para uso em ambiente de desenvolvimento. Gera um token de autenticação.
        /// </summary>
        /// <param name="userName">Nome do utilizador para autenticação.</param>
        /// <returns>Um IActionResult representando o token gerado.</returns>
        [HttpGet("Dev/AuthToken")]
        [SkipPermissionAuthorization]
        public IActionResult GenerateAuthToken(string userName)
        {
            byte[] encbuff = Encoding.UTF8.GetBytes($"{userName}" + "§" + DateTime.Now.Ticks + "§" + "508268800");

            string base64UrlEncoded = WebEncoders.Base64UrlEncode(Encrypt(encbuff));

            return Ok(base64UrlEncoded);
        }


        /// <summary>
        /// Realiza a autenticação e gera um token de acesso.
        /// </summary>
        /// <param name="token">Token de autenticação.</param>
        /// <returns>Um IActionResult representando o resultado da autenticação e o token gerado.</returns>
        [HttpGet]
        [TokenAuthorizationFilter]
        [SkipPermissionAuthorization]
        public async Task<IActionResult> SignIn([FromQuery] string? token)
        {

            if (HttpContext.Items.TryGetValue("ApplicationUserType", out var userTypeObj) && userTypeObj is ApplicationUserType applicationUserType)
            {
                string? valueToSearch = HttpContext.Items["ToSearchBy"] as string;

                UserDTO? user = applicationUserType == ApplicationUserType.SmartTimeUser ? await _smartTimeService.GetUserByName(valueToSearch) : await _smartTimeService.GetEmployeeByEmail(valueToSearch);

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

        /// <summary>
        /// Gera um novo token de acesso com base no token de atualização fornecido.
        /// </summary>
        /// <param name="refreshToken">Token de atualização.</param>
        /// <returns>Um IActionResult representando o novo token de acesso gerado.</returns>
        [HttpPost]
        public async Task<IActionResult> GenerateNewAccessToken(string refreshToken)
        {

            ClaimsPrincipal? principal = _GetPrincipal();
            if (principal == null)
            {
                throw new ArgumentException("Access Token", "Invalid access token");
            }


            if (Enum.TryParse(typeof(ApplicationUserType), principal.FindFirst("UserType")?.Value, out object? valorEnum))
            {
                ApplicationUserType? applicationUserType = (ApplicationUserType)valorEnum;

                if (applicationUserType == null)
                {
                    throw new ArgumentException("Access Token", "Invalid access token");
                }

                string? id = principal.FindFirst("UserId")?.Value;
                int userId = Convert.ToInt32(id);

                UserDTO? user = applicationUserType == ApplicationUserType.SmartTimeUser ? await _smartTimeService.GetUserById(userId) : await _smartTimeService.GetEmployeeById(userId);




                if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiration <= DateTime.Now)
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

        /// <summary>
        /// DEV TOOL: Apenas para uso em ambiente de desenvolvimento
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private byte[] Encrypt(byte[] input)
        {
            PasswordDeriveBytes pdb =
              new PasswordDeriveBytes("Smart12qazxswSt3p",
              new byte[] { 0x16, 0x29, 0x81, 0x91 });
            MemoryStream ms = new MemoryStream();
            Aes aes = new AesManaged();
            aes.Key = pdb.GetBytes(aes.KeySize / 8);
            aes.IV = pdb.GetBytes(aes.BlockSize / 8);
            CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(input, 0, input.Length);
            cs.Close();
            return ms.ToArray();
        }

        /// <summary>
        /// Obtém o principal de claims a partir do token de autenticação.
        /// </summary>
        /// <returns>Um ClaimsPrincipal representando as claims extraídas do token de autenticação.</returns>
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
