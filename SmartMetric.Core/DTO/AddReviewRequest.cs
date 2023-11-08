using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.DTO
{
    public class AddReviewRequest
    {
        public Guid FormTemplateId { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public ReviewType? ReviewType { get; set; }
        public SubjectType? SubjectType { get; set; }
        public ReviewStatus ReviewStatus { get; set; } = ReviewStatus.NotStarted;
    }
}
