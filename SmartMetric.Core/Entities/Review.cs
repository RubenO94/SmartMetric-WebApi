using SmartMetric.Core.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartMetric.Core.Entities
{
    public class Review
    {
        public Guid ReviewId { get; set; }
        public Guid FormTemplateId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ReviewType Type { get; set; }
        public SubjectType SubjectType { get; set; }

        [ForeignKey(nameof(FormTemplateId))]
        public FormTemplate? FormTemplate { get; set; }
        public ICollection<Submission>? Submissions { get; set; }
    }

}
