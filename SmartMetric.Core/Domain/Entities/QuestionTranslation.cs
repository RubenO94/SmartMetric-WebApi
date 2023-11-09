using SmartMetric.Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartMetric.Core.Domain.Entities
{
    public class QuestionTranslation
    {
        public Guid QuestionTranslationId { get; set; }
        public Guid QuestionId { get; set; }
        [StringLength(10)]
        public string? Language { get; set; }
        [StringLength(100)]
        public string? Title { get; set; }
        [StringLength(300)]
        public string? Description { get; set; }

        [ForeignKey(nameof(QuestionId))]
        public virtual Question? Question { get; set; }
    }
}
