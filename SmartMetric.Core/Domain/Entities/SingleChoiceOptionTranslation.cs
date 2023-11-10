using SmartMetric.Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartMetric.Core.Domain.Entities
{
    public class SingleChoiceOptionTranslation
    {
        public Guid SingleChoiceOptionTranslationId { get; set; }
        public Guid? SingleChoiceOptionId { get; set; }

        [StringLength(20)]
        public string? Language { get; set; }

        [StringLength(200)]
        public string? Description { get; set; }

        [ForeignKey(nameof(SingleChoiceOptionId))]
        public virtual SingleChoiceOption? SingleChoiceOption { get; set; }
    }
}
