using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartMetric.Infrastructure.Models;

[Table("Utilizadores")]
public partial class Utilizador
{
    public int Idutilizador { get; set; }

    public string? Nome { get; set; }

    public string? Password { get; set; }

    public int? Idperfil { get; set; }

    public string? Email { get; set; }

    public bool? Ativo { get; set; }

    public DateTime? UltimoAcessoSmartTime { get; set; }

    public int? Idfuncionario { get; set; }

    public string? Descricao { get; set; }

    public bool? Activo { get; set; }

    public int? TentativasAcesso { get; set; }

    public DateTime? UltimoAcessoSmartAccess { get; set; }

    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiration { get; set; }
}
