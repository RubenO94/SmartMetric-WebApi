using SmartMetric.Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartMetric.Core.Domain.Entities
{
    public class FormTemplateTranslation
    {
        [Key]
        public Guid FormTemplateTranslationId { get; set; }
        public Guid? FormTemplateId { get; set; }
        
        [StringLength(10)]
        public string? Language { get; set; }
        [StringLength(100)]
        public string? Title { get; set; }
        [StringLength(300)]
        public string? Description { get; set; }

        [ForeignKey(nameof(FormTemplateId))]
        public virtual FormTemplate? FormTemplate { get; set; }
    }
}
