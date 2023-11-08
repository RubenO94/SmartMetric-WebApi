using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartMetric.Core.Domain.Entities
{
    public class ReviewResponse
    {
        public Guid ReviewResponseId { get; set; }
        public Guid QuestionId { get; set; }
        public Guid SubmissionId { get; set; }
        public Guid? SingleChoiceOptionId { get; set; }
        public string? TextResponse { get; set; }
        public int? RatingValue { get; set; }

        [ForeignKey(nameof(SingleChoiceOptionId))]
        SingleChoiceOption? SingleChoiceOption { get; set; }

        [ForeignKey(nameof(QuestionId))]
        [Required]
        Question? Question { get; set; }

        [ForeignKey(nameof(SubmissionId))]
        [Required]
        Submission? Submission { get; set; }
    }
}


