using SmartMetric.Core.DTO.Response;
using SmartMetric.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.ServicesContracts
{
    /// <summary>
    /// Serviço para criar Tokens de JSON Web (JWT) para autenticação de utilizadores.
    /// </summary>
    public interface IJwtService
    {
        /// <summary>
        /// Cria um token JWT para o utilizador especificado.
        /// </summary>
        /// <param name="user">O utilizador para o qual o token é gerado.</param>
        /// <returns>Um objeto AuthenticationResponse contendo o token JWT gerado e as informações do utilizador.</returns>
        AuthenticationResponse CreateJwtToken(Utilizador user);

        ClaimsPrincipal? GetPrincipalFromJwtToken(string? token);
    }
}
