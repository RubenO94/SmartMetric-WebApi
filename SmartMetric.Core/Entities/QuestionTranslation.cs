using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.Entities
{
    public class QuestionTranslation
    {
        public Guid Id { get; set; }

        // Idioma da tradução
        public string? Language { get; set; }

        public string? Title { get; set; }
        public string? Description { get; set; }

        // Chave estrangeira para a pergunta
        public Guid QuestionId { get; set; }
        public Question? Question { get; set; }
    }
}
