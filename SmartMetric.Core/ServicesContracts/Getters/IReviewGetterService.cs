using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.ServicesContracts.Getters
{
    public interface IReviewGetterService
    {
        Task<ApiResponse<List<ReviewDTOResponse>>> GetReviews(int page = 1, int pageSize = 20);
        Task<ApiResponse<List<ReviewDTOResponse>>> GetReviewsByUserId(int userId, int page = 1, int pageSize = 20);
        Task<ApiResponse<ReviewDTOResponse?>> GetReviewById(Guid? reviewId);
        Task<ApiResponse<List<ReviewDTOResponse>>> GetFilteredReviews(string searchBy, string? searchString ,int page = 1, int pageSize = 20);

    }
}
