using System.ComponentModel.DataAnnotations.Schema;

namespace SmartMetric.Core.Domain.Entities
{
    public class SingleChoiceOption
    {
        public Guid SingleChoiceOptionId { get; set; }
        public Guid? QuestionId { get; set; }
        public virtual ICollection<SingleChoiceOptionTranslation>? Translations { get; set; }

        [ForeignKey(nameof(QuestionId))]
        public virtual Question? Question { get; set; }
    }
}
