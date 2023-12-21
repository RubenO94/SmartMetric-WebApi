using Microsoft.Extensions.Logging;
using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.Enums;
using SmartMetric.Core.Exceptions;
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

        public async Task<ApiResponse<ReviewDTOResponse?>> GetReviewById(Guid? reviewId)
        {
            _logger.LogInformation($"{nameof(ReviewGetterService)}.{nameof(GetReviewById)} foi iniciado");

            if (reviewId == null) throw new HttpStatusException(HttpStatusCode.BadRequest, "The 'reviewId' parameter is required and must be a valid GUID.");

            var review = await _repository.GetReviewById(reviewId.Value);

            if (review == null) throw new HttpStatusException(HttpStatusCode.NotFound, "Resource not found. The provided ID does not exist.");

            return new ApiResponse<ReviewDTOResponse?>()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Data retrieved successfully.",
                Data = review?.ToReviewDTOResponse()!
            };
        }

        public async Task<ApiResponse<List<ReviewDTOResponse>>> GetReviews(int page = 1, int pageSize = 2, Language? language = null)
        {
            var result =  await _repository.GetAllReviews(page, pageSize, language.ToString());

            var totalCount = await _repository.GetTotalRecords(temp => temp.Translations!.Any(tr => tr.Language == language.ToString()));

            return new ApiResponse<List<ReviewDTOResponse>>() 
            { 
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Data retrieved successfully.",
                Data = result.Select(temp => temp.ToReviewDTOResponse()).ToList(),
                TotalCount = totalCount
            };
        }

        public Task<ApiResponse<List<ReviewDTOResponse>>> GetReviewsByUserId(int userId, int page = 1, int pageSize = 20)
        {
            throw new NotImplementedException();
        }
    }
}
