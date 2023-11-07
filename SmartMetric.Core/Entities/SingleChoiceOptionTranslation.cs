using SmartMetric.Core.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartMetric.Core.Entities
{
    public class SingleChoiceOptionTranslation
    {
        public Guid SingleChoiceOptionTranslationId { get; set; }
        public Guid SingleChoiceTemplateId { get; set; }
        public string? Language { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }

        [ForeignKey(nameof(SingleChoiceTemplateId))]
        public SingleChoiceTemplate? SingleChoiceTemplate { get; set; }
    }
}
