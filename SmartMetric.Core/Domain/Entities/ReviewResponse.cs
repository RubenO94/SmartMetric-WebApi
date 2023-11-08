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
        public SingleChoiceOption? SingleChoiceOption { get; set; }

        [ForeignKey(nameof(QuestionId))]
        [Required]
        public Question? Question { get; set; }

        [ForeignKey(nameof(SubmissionId))]
        [Required]
        public Submission? Submission { get; set; }
    }
}


