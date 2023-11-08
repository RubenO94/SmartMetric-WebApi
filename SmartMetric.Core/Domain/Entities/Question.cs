using SmartMetric.Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartMetric.Core.Domain.Entities
{
    public class Question
    {
        [Key]
        public Guid QuestionId { get; set; }
        public Guid FormTemplateId { get; set; }
        public Guid? RatingTemplateId { get; set; }
        public Guid? SingleChoiceTemplateId { get; set; }
        public ICollection<QuestionTranslation>? Translations { get; set; }
        public bool IsRequired { get; set; }
        public string? ResponseType { get; set; }


        [ForeignKey(nameof(FormTemplateId))]
        public FormTemplate? FormTemplate { get; set; }

        [ForeignKey(nameof(RatingTemplateId))]
        public RatingTemplate? RatingTemplate { get; set; }

        [ForeignKey(nameof(SingleChoiceTemplateId))]
        public SingleChoiceTemplate? SingleChoiceTemplate { get; set; }
    }
}
