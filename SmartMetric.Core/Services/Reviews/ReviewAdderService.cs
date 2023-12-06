using Microsoft.Extensions.Logging;
using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Core.DTO.AddRequest;
using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.Helpers;
using SmartMetric.Core.ServicesContracts.Reviews;

namespace SmartMetric.Core.Services.Reviews
{
    public class ReviewAdderService : IReviewAdderService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly ISmartTimeRepository _smartTimeRepository;
        private ILogger<ReviewAdderService> _logger;

        public ReviewAdderService(IReviewRepository reviewRepository, ISmartTimeRepository smartTimeRepository, ILogger<ReviewAdderService> logger)
        {
            _reviewRepository = reviewRepository;
            _smartTimeRepository = smartTimeRepository;
            _logger = logger;
        }

        public async Task<ApiResponse<ReviewDTOResponse?>> AddReview(ReviewDTOAddRequest? request)
        {
            _logger.LogInformation($"{nameof(ReviewAdderService)}.{nameof(AddReview)} foi iniciado");

            if (request == null) throw new ArgumentNullException(nameof(request));

            ValidationHelper.ModelValidation(request);

            var userResult = await _smartTimeRepository.GetUserById(request.CreatedByUserId!.Value);

            if (userResult == null) throw new ArgumentException("User does not exist.", nameof(request.CreatedByUserId));

            if (request.StartDate >= request.EndDate) throw new ArgumentException("Start date must be before the end date.", nameof(request.StartDate));

            var departments = await _smartTimeRepository.GetDepartmentsByListIds(request.ReviewDepartmentsIds!);

            var departmentsNotExisting = request.ReviewDepartmentsIds!.Except(departments.Select(temp => temp.Iddepartamento).ToList()).ToList();

            if (departmentsNotExisting.Any()) throw new ArgumentException("Some of the departments ids does not exist", nameof(request.ReviewDepartmentsIds));

            Review review = request.ToReview();

            foreach (var department in departments)
            {
                review.Departments?.Add(new ReviewDepartment() { Department = department, Review = review });
            }

            var result = await _reviewRepository.AddReview(review);

            if (result)
            {
                return new ApiResponse<ReviewDTOResponse?>()
                {
                    StatusCode = (int)System.Net.HttpStatusCode.Created,
                    Message = "Review create with success!",
                    Data = review.ToReviewDTOResponse()
                };
            }

            return new ApiResponse<ReviewDTOResponse?>()
            {
                StatusCode = (int)System.Net.HttpStatusCode.InternalServerError,
                Message = "Something went wrong!",
            };
        }
    }
}
