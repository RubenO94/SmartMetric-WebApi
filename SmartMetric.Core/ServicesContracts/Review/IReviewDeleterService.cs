using SmartMetric.Core.DTO.Response;

namespace SmartMetric.Core.ServicesContracts.Reviews
{
    public interface IReviewDeleterService
    {
        Task<ApiResponse<bool>> DeleteReviewById(Guid? reviewId);
    }
}
