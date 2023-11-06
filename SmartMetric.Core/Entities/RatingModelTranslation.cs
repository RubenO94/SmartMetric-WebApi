using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.Entities
{
    public class RatingModelTranslation
    {
        public Guid Id { get; set; }

        // Idioma da tradução
        public string? Language { get; set; }

        public string? Title { get; set; }
        public string? Description { get; set; }

        // Chave estrangeira para o modelo de escala
        public Guid ScaleModelId { get; set; }
        public RatingTemplate? ScaleModel { get; set; }
    }
}
