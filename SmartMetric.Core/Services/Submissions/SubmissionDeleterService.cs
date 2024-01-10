using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.ServicesContracts.Submissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.Services.Submissions
{
    public class SubmissionDeleterService : ISubmissionDeleterService
    {
        public Task<ApiResponse<bool>> DeleteSubmission(Guid? submissionId)
        {
            throw new NotImplementedException();
        }
    }
}
