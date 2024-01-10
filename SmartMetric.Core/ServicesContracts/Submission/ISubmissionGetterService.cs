using SmartMetric.Core.DTO.Response;

namespace SmartMetric.Core.ServicesContracts.Submissions
{
    public interface ISubmissionGetterService
    {
        Task<ApiResponse<List<SubmissionDTOResponse>>> GetAllSubmissions();
        Task<ApiResponse<List<SubmissionDTOResponse>>> GetSubmissionsByReviewId(Guid? reviewId);
        Task<ApiResponse<List<SubmissionDTOResponse>>> GetSubmissionsById(Guid? submissionId);
    }
}
