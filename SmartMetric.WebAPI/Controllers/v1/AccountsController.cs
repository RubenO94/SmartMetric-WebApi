using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
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

        /// <summary>
        /// DEV TOOL: Apenas para uso em ambiente de desenvolvimento.
        /// </summary>
        /// <returns></returns>
        [HttpGet("Dev/AuthToken")]
        [SkipPermissionAuthorization]
        public IActionResult GenerateAuthToken()
        {
            byte[] encbuff = Encoding.UTF8.GetBytes("Pedro.Maia" + "§" + DateTime.Now.Ticks + "§" + "508268800");

            string base64UrlEncoded = WebEncoders.Base64UrlEncode(Encrypt(encbuff));

            return Ok(base64UrlEncoded);
        }

        [HttpGet]
        [TokenAuthorizationFilter]
        [SkipPermissionAuthorization]
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
        [SkipPermissionAuthorization]
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


            if (Enum.TryParse(typeof(ApplicationUserType), principal.FindFirst("UserType")?.Value, out object? valorEnum))
            {
                ApplicationUserType? applicationUserType = (ApplicationUserType)valorEnum;

                if (applicationUserType == null)
                {
                    throw new ArgumentException("Access Token", "Invalid access token");
                }

                string? id = principal.FindFirst("UserId")?.Value;
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
    }
}
