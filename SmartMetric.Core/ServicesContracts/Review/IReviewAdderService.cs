using SmartMetric.Core.DTO.AddRequest;
using SmartMetric.Core.DTO.Response;

namespace SmartMetric.Core.ServicesContracts.Reviews
{
    public interface IReviewAdderService
    {
        Task<ApiResponse<ReviewDTOResponse?>> AddReview(ReviewDTOAddRequest? request);
    }
}
