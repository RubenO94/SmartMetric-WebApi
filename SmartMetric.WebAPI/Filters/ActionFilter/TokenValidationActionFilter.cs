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
    /// <summary>
    /// Filtro de ação para validação de tokens de acesso.
    /// </summary>
    public class TokenValidationActionFilter : IAsyncActionFilter
    {
        private readonly IJwtService _jwtService;
        private readonly ISmartTimeService _smartTimeService;

        /// <summary>
        /// Construtor que recebe instâncias de serviços necessários para a validação de tokens.
        /// </summary>
        /// <param name="jwtService">Serviço de manipulação de tokens JWT.</param>
        /// <param name="smartTimeService">Serviço relacionado à aplicação SmartTime.</param>
        public TokenValidationActionFilter(IJwtService jwtService, ISmartTimeService smartTimeService)
        {
            _jwtService = jwtService;
            _smartTimeService = smartTimeService;
        }

        /// <summary>
        /// Método chamado durante a execução da ação para validar o token de acesso.
        /// </summary>
        /// <param name="context">O contexto da execução da ação.</param>
        /// <param name="next">O próximo delegate na cadeia de execução da ação.</param>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            bool skipTokenValidation = ShouldSkipTokenValidation(context);

            if (!skipTokenValidation)
            {
                var tokenAndRefreshToken = GetTokensFromRequest(context);

                if (tokenAndRefreshToken != null)
                {
                    var (token, refreshToken) = tokenAndRefreshToken.Value;

                    var claimsPrincipal = _jwtService.GetPrincipalFromJwtToken(token);

                    if (claimsPrincipal == null)
                    {
                        HandleUnauthorized(context, "Invalid access");
                        return;
                    }

                    if (!IsTokenValid(claimsPrincipal, context))
                    {
                        return;
                    }

                    var user = await TryGetUserFromToken(claimsPrincipal, context);

                    if (user == null)
                    {
                        return;
                    }

                    await UpdateUserToken(user);
                }
                else
                {
                    HandleUnauthorized(context, "Access Token is missing");
                    return;
                }
            }

            await next();
        }

        /// <summary>
        /// Verifica se a validação do token deve ser ignorada com base nos atributos aplicados.
        /// </summary>
        /// <param name="context">O contexto da execução da ação.</param>
        /// <returns>True se a validação do token deve ser ignorada, false caso contrário.</returns>
        private bool ShouldSkipTokenValidation(ActionExecutingContext context)
        {
            return context.Controller.GetType().GetCustomAttributes(typeof(SkipTokenValidationAttribute), true).Any()
                || context.ActionDescriptor.EndpointMetadata.OfType<SkipTokenValidationAttribute>().Any();
        }

        /// <summary>
        /// Manipula a resposta quando a autorização falha, retornando um resultado de não autorizado.
        /// </summary>
        /// <param name="context">O contexto da execução da ação.</param>
        /// <param name="errorMessage">A mensagem de erro.</param>
        private void HandleUnauthorized(ActionExecutingContext context, string errorMessage)
        {
            context.Result = new UnauthorizedObjectResult(new { Error = errorMessage });
        }

        /// <summary>
        /// Verifica se o token de acesso é válido.
        /// </summary>
        /// <param name="claimsPrincipal">O principal de reivindicações do token de acesso.</param>
        /// <param name="context">O contexto da execução da ação.</param>
        /// <returns>True se o token for válido, false caso contrário.</returns>
        private bool IsTokenValid(ClaimsPrincipal claimsPrincipal, ActionExecutingContext context)
        {
            var expirationClaim = claimsPrincipal.FindFirst(JwtRegisteredClaimNames.Exp);

            if (expirationClaim != null && long.TryParse(expirationClaim.Value, out var expirationUnixTime))
            {
                var expirationDateTime = DateTimeOffset.FromUnixTimeSeconds(expirationUnixTime).UtcDateTime;

                if (expirationDateTime <= DateTime.UtcNow)
                {
                    HandleUnauthorized(context, "Token has expired");
                    return false;
                }
            }
            else
            {
                HandleUnauthorized(context, "Token is invalid");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Tenta obter informações do utilizador a partir do token de acesso.
        /// </summary>
        /// <param name="claimsPrincipal">O principal de reivindicações do token de acesso.</param>
        /// <param name="context">O contexto da execução da ação.</param>
        /// <returns>As informações do utilizador em objeto UserDTO se bem-sucedido, null caso contrário.</returns>
        private async Task<UserDTO?> TryGetUserFromToken(ClaimsPrincipal claimsPrincipal, ActionExecutingContext context)
        {
            if (Enum.TryParse(typeof(ApplicationUserType), claimsPrincipal.FindFirstValue(ClaimTypes.GivenName), out object? valorEnum))
            {
                ApplicationUserType? applicationUserType = (ApplicationUserType)valorEnum;

                if (applicationUserType == null)
                {
                    HandleUnauthorized(context, "Invalid access token");
                    return null;
                }

                string? id = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);

                if (int.TryParse(id, out int userId))
                {
                    UserDTO? user = null;

                    if (applicationUserType == ApplicationUserType.User)
                    {
                        user = await _smartTimeService.GetUserById(userId);
                    }
                    else if (applicationUserType == ApplicationUserType.Employee)
                    {
                        user = await _smartTimeService.GetEmployeeById(userId);
                    }

                    if (user == null || user.RefreshTokenExpiration <= DateTime.Now)
                    {
                        HandleUnauthorized(context, "Invalid Access, login time-out");
                        return null;
                    }

                    user.ApplicationUserType = applicationUserType;
                    return user;
                }
            }

            HandleUnauthorized(context, "Error retrieving user information from token");
            return null;
        }



        /// <summary>
        /// Atualiza o token de acesso e o token de atualização do utilizador.
        /// </summary>
        /// <param name="user">O objeto de transferência de dados do utilizador.</param>
        /// <returns>Uma tarefa assíncrona.</returns>
        private async Task UpdateUserToken(UserDTO user)
        {
            AuthenticationResponse authenticationResponse = _jwtService.CreateJwtToken(user);

            user.RefreshToken = authenticationResponse.RefreshToken;
            user.RefreshTokenExpiration = authenticationResponse.RefreshTokenExpiration;
            user.ApplicationUserType = (ApplicationUserType)Enum.Parse(typeof(ApplicationUserType), user.ApplicationUserType.ToString());

            await _smartTimeService.UpdateApplicationUser(user);
        }

        /// <summary>
        /// Obtém os tokens de acesso e atualização da solicitação HTTP.
        /// </summary>
        /// <param name="context">O contexto da execução da ação.</param>
        /// <returns>Os tokens de acesso e atualização, se presentes.</returns>
        private (string Token, string RefreshToken)? GetTokensFromRequest(ActionExecutingContext context)
        {
            var authorizationHeader = context.HttpContext.Request.Headers.Authorization;
            var acceptHeader = context.HttpContext.Request.Headers.Accept;

            if (!string.IsNullOrEmpty(authorizationHeader) && !string.IsNullOrEmpty(acceptHeader))
            {
                var token = authorizationHeader.ToString().Replace("Bearer ", string.Empty);
                var refreshToken = acceptHeader.ToString();

                return (token, refreshToken);
            }

            return null;
        }
    }


}
