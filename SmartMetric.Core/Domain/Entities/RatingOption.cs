using System.ComponentModel.DataAnnotations.Schema;

namespace SmartMetric.Core.Domain.Entities
{
    public class RatingOption
    {
        public Guid RatingOptionId { get; set; }
        public Guid RatingTemplateId { get; set; }
        public ICollection<RatingOptionTranslation>? Translations { get; set; }
        public int NumericValue { get; set; }

        [ForeignKey(nameof(RatingTemplateId))]
        public RatingTemplate? RatingTemplate { get; set; }
    }
}
