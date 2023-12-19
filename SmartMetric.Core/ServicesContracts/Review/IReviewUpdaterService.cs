using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.DTO.UpdateRequest;
using SmartMetric.Core.Enums;

namespace SmartMetric.Core.ServicesContracts.Reviews
{
    public interface IReviewUpdaterService
    {
        Task<ApiResponse<ReviewDTOResponse>> UpdateReview(Guid? reviewId, ReviewDTOUpdate? reviewDTOUpdate);

        Task<ApiResponse<bool>> UpdateReviewStatus(Guid? reviewId, ReviewDTOUpdateStatus reviewStatus);
    }
}
