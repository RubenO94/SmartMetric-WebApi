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

        public async Task<ApiResponse<List<ReviewDTOResponse>>> GetCompletedReviews()
        {
            _logger.LogInformation($"{nameof(ReviewGetterService)}.{nameof(GetCompletedReviews)} foi iniciado");

            var result = await _repository.GetReviewsCompleted();

            return new ApiResponse<List<ReviewDTOResponse>>()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Data retrieved successfully.",
                Data = result.Select(temp => temp.ToReviewDTOResponse()).ToList()
            };
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

            var data = review?.ToReviewDTOResponse()!;
            data.SubmissionsTotal = await _repository.GetTotalSubmissions(data.ReviewId);
            data.SubmissionsCompleted = await _repository.GetTotalSubmissionsCompleted(data.ReviewId);

            return new ApiResponse<ReviewDTOResponse?>()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Data retrieved successfully.",
                Data = data
            };
        }

        public async Task<ApiResponse<List<ReviewDTOResponse>>> GetReviews(int page = 1, int pageSize = 2, Language? language = null)
        {
            var result =  await _repository.GetAllReviews(page, pageSize, language.ToString());

            var hasLanguage = language != null;

            var totalCount = await _repository.GetTotalRecords(hasLanguage ? temp => temp.Translations!.Any(tr => tr.Language == language.ToString()) : null);

            var data = result.Select(temp => temp.ToReviewDTOResponse()).ToList();
            foreach (var review in data)
            {
                review.SubmissionsTotal = await _repository.GetTotalSubmissions(review.ReviewId);
                review.SubmissionsCompleted = await _repository.GetTotalSubmissionsCompleted(review.ReviewId);
            }

            return new ApiResponse<List<ReviewDTOResponse>>() 
            { 
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Data retrieved successfully.",
                Data = data,
                TotalCount = totalCount
            };
        }

        public Task<ApiResponse<List<ReviewDTOResponse>>> GetReviewsByUserId(int userId, int page = 1, int pageSize = 20)
        {
            throw new NotImplementedException();
        }
    }
}
