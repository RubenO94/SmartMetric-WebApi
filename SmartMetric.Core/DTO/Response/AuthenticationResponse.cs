using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.DTO.Response
{
    /// <summary>
    /// Classe que representa a estrutura de resposta para operações de autenticação.
    /// </summary>
    public class AuthenticationResponse
    {
        /// <summary>
        /// Obtém ou define o nome do utilizador autenticado.
        /// </summary>
        public string? UserName { get; set; } = string.Empty;

        /// <summary>
        /// Obtém ou define o endereço de e-mail do utilizador autenticado.
        /// </summary>
        public string? UserEmail { get; set; } = string.Empty;

        /// <summary>
        /// Obtém ou define o token de autenticação.
        /// </summary>
        public string? Token { get; set; } = string.Empty;

        /// <summary>
        /// Obtém ou define o token de atualização para renovar a autenticação.
        /// </summary>
        public string? RefreshToken { get; set; } = string.Empty;

        /// <summary>
        /// Obtém ou define a data e hora de expiração do token de autenticação.
        /// </summary>
        public DateTime Expiration { get; set; }

        /// <summary>
        /// Obtém ou define a data e hora de expiração do token de atualização.
        /// </summary>
        public DateTime RefreshTokenExpiration { get; set; }
    }

}
