using System;
using System.Collections.Generic;

namespace SmartMetric.Core.Domain.Entities;

public partial class Departamento
{
    public int Iddepartamento { get; set; }

    public string? Codigo { get; set; }

    public string? Descricao { get; set; }

    public int? IddepartamentoPai { get; set; }

    public int? Identidade { get; set; }

    public string? Notas { get; set; }

    public int? NumeroFuncionariosFerias { get; set; }

    public int? MaximoFuncionariosFerias { get; set; }

    public string? EmailChefia { get; set; }

    public bool? NaoValidarDocumentos { get; set; }
}
