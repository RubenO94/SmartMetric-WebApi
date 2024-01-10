using SmartMetric.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.DTO.UpdateRequest
{
    public class SubmissionDTOUpdate
    {
        public Guid SubmissionId { get; set; }
        public Guid ReviewId { get; set; }
        public int? EvaluatedEmployeeId { get; set; }
        public int? EvaluatorEmployeeId { get; set; }
        public DateTime? SubmissionDate { get; set; }
        public List<ReviewResponse>? ReviewResponses { get; set; }
    }
}
