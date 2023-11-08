using SmartMetric.Core.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartMetric.Core.Domain.Entities
{
    public class Review
    {
        public Guid ReviewId { get; set; }
        public Guid FormTemplateId { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? ReviewType { get; set; }
        public string? SubjectType { get; set; }
        public string? ReviewStatus { get; set; }

        [ForeignKey(nameof(FormTemplateId))]
        public FormTemplate? FormTemplate { get; set; }
        public ICollection<Submission>? Submissions { get; set; }
    }

}
