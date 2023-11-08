using SmartMetric.Core.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartMetric.Core.Domain.Entities
{
    public class QuestionTranslation
    {
        public Guid QuestionTranslationId { get; set; }
        public Guid QuestionId { get; set; }
        public string? Language { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }

        [ForeignKey(nameof(QuestionId))]
        public Question? Question { get; set; }
    }
}
