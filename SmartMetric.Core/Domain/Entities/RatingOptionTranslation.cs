using SmartMetric.Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartMetric.Core.Domain.Entities
{
    public class RatingOptionTranslation
    {
        public Guid RatingOptionTranslationId { get; set; }
        public Guid RatingOptionId { get; set; }

        [StringLength(10)]
        public string? Language { get; set; }
        [StringLength(50)]
        public string? Description { get; set; }

        [ForeignKey(nameof(RatingOptionId))]
        public virtual RatingOption? RatingOption { get; set; }
    }
}
