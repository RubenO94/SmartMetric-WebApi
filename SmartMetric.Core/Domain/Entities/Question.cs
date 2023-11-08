using SmartMetric.Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartMetric.Core.Domain.Entities
{
    public class Question
    {
        [Key]
        public Guid QuestionId { get; set; }
        public ICollection<FormTemplateQuestion>? FormTemplateQuestions { get; set; }
        public ICollection<QuestionTranslation>? Translations { get; set; }
        public ICollection<RatingOption>? RatingOptions { get; set; }
        public ICollection <SingleChoiceOption>? SingleChoiceOptions { get; set; }
        public bool IsRequired { get; set; }
        public string? ResponseType { get; set; }
    }
}
