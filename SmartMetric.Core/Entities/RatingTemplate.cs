using System.ComponentModel.DataAnnotations;

namespace SmartMetric.Core.Entities
{
    public class RatingTemplate
    {
        [Key]
        public Guid RatingTemplateId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int CreatedByUserId { get; set; }
        public ICollection<RatingTemplateTranslation>? Translations { get; set; }
        public ICollection<RatingOption>? RatingOptions { get; set; }
    }
}
