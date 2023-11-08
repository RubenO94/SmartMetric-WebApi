using SmartMetric.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.DTO
{
    public class FormTemplateTranslationDTO
    {
        public Language Language { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
    }
}
