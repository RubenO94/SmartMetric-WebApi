using SmartMetric.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.DTO
{
    public class AddFormTemplateRequest
    {
        public DateTime CreatedDate { get; set; }
        public int CreatedByUserId { get; set; }
        public Language Language { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }

        //TODO: Adicionar metodo para conversão do objecto Request em objeto Entity
    }
}
