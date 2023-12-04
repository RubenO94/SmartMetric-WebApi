using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.DTO.UpdateRequest;

namespace SmartMetric.Core.ServicesContracts.Reviews
{
    public interface IReviewUpdaterService
    {
        Task<ApiResponse<ReviewDTOResponse>> UpdateReview(Guid? reviewId, ReviewDTOUpdate? reviewDTOUpdate);
    }
}
