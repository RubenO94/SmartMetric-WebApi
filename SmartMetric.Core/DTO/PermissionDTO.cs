using SmartMetric.Core.Enums;
using SmartMetric.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.DTO
{
    public class PermissionDTO
    {
        public int PermissionId { get; set; }
        public PermissionType? PermissionType { get; set; }
        public bool HasPermission { get; set; }

        public PerfisJanela ToPerfilJanela()
        {
            return new PerfisJanela()
            {
                Idjanela = PermissionId,
                Aplicacao = "SmartTime",
                Modulo = "Desempenho"
            };           
        }
    }
}
