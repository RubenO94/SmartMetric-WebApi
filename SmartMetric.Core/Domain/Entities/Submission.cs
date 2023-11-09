using System.ComponentModel.DataAnnotations.Schema;

namespace SmartMetric.Core.Domain.Entities
{
    public class Submission
    {
        public Guid SubmissionId { get; set; }
        public Guid? ReviewId { get; set; }
        public int? EvaluatedEmployeeId { get; set; }
        public int? EvaluatorEmployeeId { get; set; }
        public DateTime? SubmissionDate { get; set; }
        public virtual ICollection<ReviewResponse>? ReviewResponses { get; set; }

        [ForeignKey(nameof(ReviewId))]
        public virtual Review? Review { get; set; }
    }

}
