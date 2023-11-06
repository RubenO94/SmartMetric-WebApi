using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.Entities
{
    public class FormTemplateTranslation
    {
        public Guid Id { get; set; }

        // Idioma da tradução (por exemplo, "pt" para português, "en" para inglês, etc.)
        public string? Language { get; set; }

        public string? Title { get; set; }
        public string? Description { get; set; }

        // Chave estrangeira para o modelo de formulário
        public Guid FormTemplateId { get; set; }
        public FormTemplate? FormTemplate { get; set; }
    }
}
