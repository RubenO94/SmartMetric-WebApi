using SmartMetric.Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartMetric.Core.Domain.Entities
{
    public class Review
    {
        public Guid ReviewId { get; set; }
        public Guid? FormTemplateId { get; set; }
        public int? CreatedByUserId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        [StringLength(20)]
        public string? ReviewType { get; set; }
        [StringLength(20)]
        public string? SubjectType { get; set; }
        [StringLength(20)]
        public string? ReviewStatus { get; set; }
        public virtual ICollection<Submission>? Submissions { get; set; }

        [ForeignKey(nameof(FormTemplateId))]
        public virtual FormTemplate? FormTemplate { get; set; }
    }

}
