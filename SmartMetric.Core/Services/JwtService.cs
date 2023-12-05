using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SmartMetric.Core.DTO;
using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.ServicesContracts;
using SmartMetric.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.Services
{

    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="JwtService"/>.
        /// </summary>
        /// <param name="configuration">O objeto de configuração para aceder às definições da aplicação.</param>
        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public AuthenticationResponse CreateJwtToken(UserDTO user)
        {
            // Define o tempo de expiração do token
            DateTime expiration = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:EXPIRATION_MINUTES"]));

            // Define as claims a serem incluídas no token
            Claim[] claims = new Claim[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()), // Assunto (id do utilizador)
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // ID único do JWT
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()), // Emitido em (data e hora da geração do token)
            new Claim(JwtRegisteredClaimNames.Name, user.UserName!.ToString()), // Nome do utilizador (Opcional)
            new Claim(JwtRegisteredClaimNames.UniqueName, user.UserId!.ToString()), // Id do utilizador (Opcional)
            new Claim(JwtRegisteredClaimNames.Email, user.UserEmail?.ToString() ?? string.Empty),// Email do utilizador (Opcional)
            new Claim(JwtRegisteredClaimNames.GivenName, user.ApplicationUserType!.ToString()!),// tipo de utilizador da aplicação (Opcional),
            new Claim(JwtRegisteredClaimNames.FamilyName, user.UserProfileId!.ToString() ?? string.Empty),// id do perfil do utilizador,
            new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(expiration).ToUnixTimeSeconds().ToString()) // Tempo de expiração (Obrigatório)
            };

            // Configura a chave de segurança e as credenciais de assinatura
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Gera o token JWT
            JwtSecurityToken tokenGenerator = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: expiration,
                signingCredentials: signingCredentials
            );

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            // Escreve o token como uma string
            string token = tokenHandler.WriteToken(tokenGenerator);

            // Retorna o AuthenticationResponse com o token e as informações do utilizador
            return new AuthenticationResponse()
            {
                Token = token,
                UserEmail = user.UserEmail,
                UserName = user.UserName,
                Expiration = expiration,
                RefreshToken = GenerateRefreshToken(),
                RefreshTokenExpiration = DateTime.Now.AddMinutes(Convert.ToInt32(_configuration["RefreshToken:EXPIRATION_MINUTES"]))
            };
        }

        /// <summary>
        /// Cria um Refresh Token (string base64 de numeros aleatórios)
        /// </summary>
        /// <returns>Retorna uma string base64</returns>
        private string GenerateRefreshToken()
        {
            Byte[] bytes = new byte[64];
            var randomNumberGenerator = RandomNumberGenerator.Create();

            randomNumberGenerator.GetBytes(bytes);
            return Convert.ToBase64String(bytes);
        }

        public ClaimsPrincipal? GetPrincipalFromJwtToken(string? token)
        {
            var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateAudience = true,
                ValidAudience = _configuration["Jwt:Audience"],
                ValidateIssuer = true,
                ValidIssuer = _configuration["Jwt:Issuer"],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)),

                ValidateLifetime = false //should be false
            };

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            ClaimsPrincipal principal = jwtSecurityTokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }
    }

}
