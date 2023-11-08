using SmartMetric.Core.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartMetric.Core.Domain.Entities
{
    public class SingleChoiceOptionTranslation
    {
        public Guid SingleChoiceOptionTranslationId { get; set; }
        public Guid SingleChoiceOptionId { get; set; }
        public string? Language { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }

        [ForeignKey(nameof(SingleChoiceOptionId))]
        public SingleChoiceOption? SingleChoiceOption { get; set; }
    }
}
