using SmartMetric.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.DTO.Response
{
    public class PerfilDTOResponse
    {
        public int PerfilId { get; set; }
        public string? PerfilName { get; set;}
        public PerfilType PerfilType { get; set; }
    }
}
