using SmartMetric.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.DTO.Response
{
    public class SubmissionDTOResponse
    {
        public Guid SubmissionId { get; set; }
        public Guid? ReviewId { get; set; }
        public EmployeeDTOResponse? EvaluatedEmployeeId { get; set; }
        public EmployeeDTOResponse? EvaluatorEmployeeId { get; set; }
        public DepartmentDTOResponse? EvaluatedDepartmentId { get; set; }
        public DepartmentDTOResponse? EvaluatorDepartmentId { get; set; }
        public DateTime? SubmissionDate { get; set; }
        public List<ReviewResponseDTOResponse>? ReviewResponses { get; set; }

    }


    public static class SubmissioExtensions
    {
        public static SubmissionDTOResponse ToSubmissionDTOResponse(this Submission submission)
        {
            return new SubmissionDTOResponse()
            {
                SubmissionId = submission.SubmissionId,
                ReviewId = submission.ReviewId,
                EvaluatedEmployeeId = submission.EvaluatedEmployee?.ToEmployeeDTOResponse(),
                EvaluatorEmployeeId = submission.EvaluatorEmployee?.ToEmployeeDTOResponse(),
                EvaluatedDepartmentId = submission.EvaluatedDepartment?.ToDepartamentDTOResponse(),
                EvaluatorDepartmentId = submission.EvaluatorDepartment?.ToDepartamentDTOResponse(),
                SubmissionDate = submission.SubmissionDate,
                ReviewResponses = submission.ReviewResponses?.Select(temp => temp.ToReviewResponseDTOResponse()).ToList()
            };
        }
    }
}
