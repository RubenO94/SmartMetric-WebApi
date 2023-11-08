using SmartMetric.Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartMetric.Core.Domain.Entities
{
    public class FormTemplateTranslation
    {
        [Key]
        public Guid FormTemplateTranslationId { get; set; }
        public Guid FormTemplateId { get; set; }
        public string? Language { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }

        [ForeignKey(nameof(FormTemplateId))]
        public FormTemplate? FormTemplate { get; set; }
    }
}
