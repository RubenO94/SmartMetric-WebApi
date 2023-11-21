using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartMetric.Infrastructure.Models;

[Table("Perfis")]
public partial class Perfis
{
    public int Idperfil { get; set; }

    public string? Nome { get; set; }

    public string? Descricao { get; set; }

    public int? PortalColaborador { get; set; }
}
