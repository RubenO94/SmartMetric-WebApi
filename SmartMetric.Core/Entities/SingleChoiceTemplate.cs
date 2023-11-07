using System.ComponentModel.DataAnnotations;

namespace SmartMetric.Core.Entities
{
    public class SingleChoiceTemplate
    {
        [Key]
        public Guid SingleChoiceTemplateId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int CreatedByUserId { get; set; }
        public ICollection<SingleChoiceTemplateTranslation>? Translations { get; set; }
        public ICollection<SingleChoiceOption>? SingleChoiceOptions { get; set; }
    }
}
