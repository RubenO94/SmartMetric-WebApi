using System;
using System.Collections.Generic;

namespace SmartMetric.Infrastructure.Models;

public partial class PerfisJanela
{
    public int? Idperfil { get; set; }

    public int? Idjanela { get; set; }

    public string? Aplicacao { get; set; }

    public string? Modulo { get; set; }
}
