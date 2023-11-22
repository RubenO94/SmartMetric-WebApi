using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Enums;
using SmartMetric.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.DTO
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? UserEmail { get; set; }
        public string? UserPhoto { get; set; } = string.Empty;
        public string? RefreshToken { get; set; } = string.Empty;
        public DateTime? RefreshTokenExpiration { get; set; }
        public ApplicationUserType? ApplicationUserType { get; set; }
    }

    public static class UserExtensions
    {
        public static UserDTO ToUserDTO(this Utilizador utilizador)
        {
            return new UserDTO()
            {
                UserId = utilizador.Idutilizador,
                UserEmail = utilizador.Email,
                UserName = utilizador.Nome,
                UserPhoto = string.Empty, //TODO: User Photo. Será preciso??
                RefreshToken = utilizador.RefreshToken,
                RefreshTokenExpiration = utilizador.RefreshTokenExpiration
            };
        }

        public static UserDTO ToUserDTO(this Funcionario funcionario)
        {
            return new UserDTO()
            {
                UserId = funcionario.Idfuncionario,
                UserEmail = funcionario.Email,
                UserName = funcionario.Nome,
                UserPhoto = string.Empty, //TODO: User Photo. Será preciso??
                RefreshToken = funcionario.RefreshToken,
                RefreshTokenExpiration = funcionario.RefreshTokenExpiration
            };
        }
    }
}
