using System;
using System.Collections.Generic;

namespace SmartMetric.Infrastructure.Models;

public partial class PerfisDepartamento
{
    public int? Idperfil { get; set; }

    public int? Iddepartamento { get; set; }

    public string? NivelFerias { get; set; }

    public string? NivelAusencias { get; set; }

    public string? NivelExtras { get; set; }

    public string? NivelFuncionariosMarcacoes { get; set; }

    public string? NivelAusenciasServico { get; set; }

    public string? Nivel { get; set; }
}
