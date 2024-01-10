using SmartMetric.Core.DTO.Response;

namespace SmartMetric.Core.ServicesContracts.Submissions
{
    public interface ISubmissionDeleterService
    {
        Task<ApiResponse<bool>> DeleteSubmission(Guid? submissionId);
    }
}
