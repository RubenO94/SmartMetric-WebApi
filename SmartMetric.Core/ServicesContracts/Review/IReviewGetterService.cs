﻿using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.Enums;
using SmartMetric.Core.DTO;

namespace SmartMetric.Core.ServicesContracts.Reviews
{
    public interface IReviewGetterService
    {
        Task<ApiResponse<List<ReviewDTOResponse>>> GetReviews(int page = 1, int pageSize = 20, Language? language = null, string name = "", string? reviewStatus = null);
        Task<ApiResponse<List<ReviewDTOResponse>>> GetCompletedReviews();
        Task<ApiResponse<List<ReviewDTOResponse>>> GetReviewsByUserId(int userId, int page = 1, int pageSize = 20);
        Task<ApiResponse<ReviewDTOResponse?>> GetReviewById(Guid? reviewId);
        Task<ApiResponse<List<ReviewDTOResponse>>> GetFilteredReviews(string searchBy, string? searchString ,int page = 1, int pageSize = 20);
    }
}
