using System.ComponentModel.DataAnnotations;

namespace SmartMetric.Core.Domain.Entities
{
    public class FormTemplate
    {
        [Key]
        public Guid FormTemplateId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int CreatedByUserId { get; set; }
        public ICollection<FormTemplateTranslation>? Translations { get; set; }
        public ICollection<Question>? Questions { get; set; }
    }

}
