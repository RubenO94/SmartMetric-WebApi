using SmartMetric.Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartMetric.Core.Domain.Entities
{
    public class Question
    {
        [Key]
        public Guid QuestionId { get; set; }
        public virtual ICollection<FormTemplateQuestion>? FormTemplateQuestions { get; set; }
        public virtual ICollection<ReviewQuestion>? ReviewQuestions { get; set; }
        public virtual ICollection<QuestionTranslation>? Translations { get; set; }
        public virtual ICollection<RatingOption>? RatingOptions { get; set; }
        public virtual ICollection<SingleChoiceOption>? SingleChoiceOptions { get; set; }
        public bool IsRequired { get; set; }

        [StringLength(20)]
        public string? ResponseType { get; set; }
        public int? Position { get; set; }
    }
}
