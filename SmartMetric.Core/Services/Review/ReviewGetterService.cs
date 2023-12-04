using Microsoft.Extensions.Logging;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.ServicesContracts.Reviews;
using System.Net;

namespace SmartMetric.Core.Services.Reviews
{
    public class ReviewGetterService : IReviewGetterService
    {
        private readonly IReviewRepository _repository;
        private readonly ILogger<ReviewGetterService> _logger;

        public ReviewGetterService(IReviewRepository repository, ILogger<ReviewGetterService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public Task<ApiResponse<List<ReviewDTOResponse>>> GetFilteredReviews(string searchBy, string? searchString, int page = 1, int pageSize = 20)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<ReviewDTOResponse?>> GetReviewById(Guid? reviewId)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<List<ReviewDTOResponse>>> GetReviews(int page = 1, int pageSize = 20)
        {
            var result =  await _repository.GetAllReviews(page, pageSize);

            return new ApiResponse<List<ReviewDTOResponse>>() 
            { 
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Data retrieved successfully.",
                Data = result.Select(temp => temp.ToReviewDTOResponse()).ToList()
            };
        }

        public Task<ApiResponse<List<ReviewDTOResponse>>> GetReviewsByUserId(int userId, int page = 1, int pageSize = 20)
        {
            throw new NotImplementedException();
        }
    }
}
