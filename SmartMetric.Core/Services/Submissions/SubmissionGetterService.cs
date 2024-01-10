using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.ServicesContracts.Submissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.Services.Submissions
{
    public class SubmissionGetterService : ISubmissionGetterService
    {
        public Task<ApiResponse<List<SubmissionDTOResponse>>> GetAllSubmissions()
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<List<SubmissionDTOResponse>>> GetSubmissionsById(Guid? submissionId)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<List<SubmissionDTOResponse>>> GetSubmissionsByReviewId(Guid? reviewId)
        {
            throw new NotImplementedException();
        }
    }
}
