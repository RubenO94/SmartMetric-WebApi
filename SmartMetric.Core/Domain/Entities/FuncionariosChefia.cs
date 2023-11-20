using System;
using System.Collections.Generic;

namespace SmartMetric.Core.Domain.Entities;

public partial class FuncionariosChefia
{
    public int? IdfuncionarioSuperior { get; set; }

    public int? Idfuncionario { get; set; }

    public int? Iddepartamento { get; set; }

    public string? NivelFerias { get; set; }

    public string? NivelAusencias { get; set; }

    public int? NivelBh { get; set; }

    public string? NivelExtras { get; set; }

    public string? NivelFuncionariosMarcacoes { get; set; }

    public string? NivelAusenciasServico { get; set; }

    public string? Nivel { get; set; }
}
