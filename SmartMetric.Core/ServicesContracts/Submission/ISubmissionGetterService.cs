using SmartMetric.Core.DTO.Response;

namespace SmartMetric.Core.ServicesContracts.Submissions
{
    public interface ISubmissionGetterService
    {
        Task<ApiResponse<List<SubmissionDTOResponse>>> GetAllSubmissions();
        Task<ApiResponse<List<SubmissionDTOResponse>>> GetSubmissionsByReviewId(Guid? reviewId);
        Task<ApiResponse<SubmissionDTOResponse>> GetSubmissionById(Guid? submissionId);
        Task<ApiResponse<List<SubmissionDTOResponse>>> GetSubmissionsByEvaluatorEmployeeId(int employeeId);
        Task<ApiResponse<List<SubmissionDTOResponse>>> GetSubmissionsByEvaluatedEmployeeId(int employeeId);
    }
}
