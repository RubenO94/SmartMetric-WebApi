using System.ComponentModel.DataAnnotations.Schema;

namespace SmartMetric.Core.Domain.Entities
{
    public class RatingOption
    {
        public Guid RatingOptionId { get; set; }
        public Guid QuestionId { get; set; }
        public ICollection<RatingOptionTranslation>? Translations { get; set; }
        public int NumericValue { get; set; }

        [ForeignKey(nameof(QuestionId))]
        Question? Question { get; set; }
    }
}
