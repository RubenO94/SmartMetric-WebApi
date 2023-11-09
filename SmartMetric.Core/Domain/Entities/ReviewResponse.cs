using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartMetric.Core.Domain.Entities
{
    public class ReviewResponse
    {
        public Guid ReviewResponseId { get; set; }
        public Guid? ReviewQuestionId { get; set; }
        public Guid? SubmissionId { get; set; }
        public Guid? SingleChoiceOptionId { get; set; }

        [StringLength(500)]
        public string? TextResponse { get; set; }
        public int? RatingValue { get; set; }

        [ForeignKey(nameof(SingleChoiceOptionId))]
        public virtual SingleChoiceOption? SingleChoiceOption { get; set; }

        [ForeignKey(nameof(ReviewQuestionId))]
        [Required]
        public virtual ReviewQuestion? ReviewQuestion { get; set; }

        [ForeignKey(nameof(SubmissionId))]
        [Required]
        public virtual Submission? Submission { get; set; }
    }

}


