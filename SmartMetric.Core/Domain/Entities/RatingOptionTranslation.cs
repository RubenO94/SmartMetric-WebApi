using SmartMetric.Core.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartMetric.Core.Domain.Entities
{
    public class RatingOptionTranslation
    {
        public Guid RatingOptionTranslationId { get; set; }
        public Guid RatingOptionId { get; set; }
        public string? Language { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }

        [ForeignKey(nameof(RatingOptionId))]
        public RatingOption? RatingOption { get; set; }
    }
}
