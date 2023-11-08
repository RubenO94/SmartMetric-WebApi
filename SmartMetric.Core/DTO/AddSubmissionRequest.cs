using SmartMetric.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.DTO
{
    public class AddSubmissionRequest
    {
        public Guid ReviewId { get; set; }
        public int EvaluatedEmployeeId { get; set; }
        public int EvaluatorEmployeeId { get; set; }
        public DateTime SubmissionDate { get; set; }
        public ICollection<ReviewResponse>? ReviewResponses { get; set; }

    }
}
