using System.ComponentModel.DataAnnotations.Schema;

namespace SmartMetric.Core.Domain.Entities
{
    public class SingleChoiceOption
    {
        public Guid SingleChoiceOptionId { get; set; }
        public Guid SingleChoiceTemplateId { get; set; }
        public ICollection<SingleChoiceOptionTranslation>? Translations { get; set; }

        [ForeignKey(nameof(SingleChoiceTemplateId))]
        public SingleChoiceTemplate? SingleChoiceTemplate { get; set; }
    }
}
