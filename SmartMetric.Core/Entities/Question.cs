using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.Entities
{
    public class Question
    {
        public Guid Id { get; set; }
        public Guid FormTemplateId { get; set; }
        public Guid? ScaleModelId { get; set; } // Referência ao modelo de escala se for uma pergunta de classificação
        public Guid? SingleChoiceModelId { get; set; }
        public ICollection<QuestionTranslation>? Translations { get; set; }
        public bool IsRequired { get; set; } // Indica se a pergunta é obrigatória
        public ResponseType ResponseType { get; set; } // Tipo de resposta (Texto, Classificação, Resposta Única)
        public
    }
}
