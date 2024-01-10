using SmartMetric.Core.DTO.AddRequest;
using SmartMetric.Core.DTO.Response;

namespace SmartMetric.Core.ServicesContracts.Submissions
{
    public interface ISubmissionAdderService
    {
        Task<ApiResponse<SubmissionDTOResponse>> AddSubmission(SubmissionDTOAddRequest? request);
    }
}
