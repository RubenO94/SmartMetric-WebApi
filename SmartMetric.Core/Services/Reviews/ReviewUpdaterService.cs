using Microsoft.Extensions.Logging;
using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.DTO.UpdateRequest;
using SmartMetric.Core.Enums;
using SmartMetric.Core.Helpers;
using SmartMetric.Core.ServicesContracts.Reviews;
using System.Linq.Expressions;
using System.Net;
using System.Net.WebSockets;

namespace SmartMetric.Core.Services.Reviews
{
    public class ReviewUpdaterService : IReviewUpdaterService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly ISmartTimeRepository _smartTimeRepository;
        private readonly ILogger<ReviewUpdaterService> _logger;

        public ReviewUpdaterService(IReviewRepository reviewRepository, ISmartTimeRepository smartTimeRepository, ILogger<ReviewUpdaterService> logger)
        {
            _reviewRepository = reviewRepository;
            _smartTimeRepository = smartTimeRepository;
            _logger = logger;
        }

        public async Task<ApiResponse<ReviewDTOResponse>> UpdateReview(Guid? reviewId, ReviewDTOUpdate? reviewDTOUpdate)
        {
            _logger.LogInformation($"{nameof(ReviewUpdaterService)}.{nameof(UpdateReview)} foi iniciado");

            if (reviewId == null) throw new ArgumentNullException(nameof(reviewId));

            if (reviewDTOUpdate == null) throw new ArgumentNullException(nameof(reviewDTOUpdate));

            if (reviewDTOUpdate.StartDate >= reviewDTOUpdate.EndDate) throw new ArgumentException("Start date must be before the end date.", nameof(reviewDTOUpdate.StartDate));

            var departments = await _smartTimeRepository.GetDepartmentsByListIds(reviewDTOUpdate.DepartmentIds!.ToList());

            var departmentsNotExisting = reviewDTOUpdate.DepartmentIds!.Except(departments.Select(temp => temp.Iddepartamento).ToList()).ToList();

            if (departmentsNotExisting.Any()) throw new ArgumentException("Some of the departments ids does not exist", nameof(reviewDTOUpdate.DepartmentIds));


            var matchingReview = await _reviewRepository.GetReviewById(reviewId.Value);

            if (matchingReview == null) throw new ArgumentException("Review does not exist.", nameof(Review));

            matchingReview.ModifiedDate = DateTime.UtcNow;

            // Atualizar revisão usando o helper
            UpdateHelper.UpdateReview(matchingReview, reviewDTOUpdate);

            var response = await _reviewRepository.UpdateReview(matchingReview);

            return new ApiResponse<ReviewDTOResponse>()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Review updated successfully",
                Data = matchingReview.ToReviewDTOResponse()
            };
        }

        public async Task<ApiResponse<bool>> UpdateReviewStatus(Guid? reviewId, ReviewDTOUpdateStatus review)
        {
            _logger.LogInformation($"{nameof(ReviewUpdaterService)}.{nameof(UpdateReviewStatus)} foi iniciado");

            if (reviewId == null) throw new ArgumentNullException(nameof(reviewId));

            var enumNames = Enum.GetNames(typeof(ReviewStatus));
            if (!enumNames.Any(name => name == review.ReviewStatus.ToString())) throw new ArgumentException("Review Status doesn't exist");

            if (review.ReviewStatus == "Active" && (review.EndDate == null || review.EndDate.ToString() == "")) throw new ArgumentNullException("To Change reviewStatus to Active need an End Date for review.");

            var matchingReview = await _reviewRepository.GetReviewById(reviewId.Value);

            if (matchingReview == null) throw new ArgumentNullException("Review doesn't exist", nameof(reviewId));

            switch (matchingReview.ReviewStatus)
            {
                case "NotStarted":
                    if (review.ReviewStatus != "Active" && review.ReviewStatus != "Canceled") throw new ArgumentException($"Review Status can´t change to {review.ReviewStatus}");
                    break;
                case "Active":
                    if (review.ReviewStatus != "Canceled" && review.ReviewStatus != "Completed") throw new ArgumentException($"Review Status can´t change to {review.ReviewStatus}");
                    break;
                default:
                    throw new ArgumentException($"Review Status can´t change to {review.ReviewStatus}");
            }

            var result = await _reviewRepository.UpdateReviewStatus(reviewId.Value, review);

            return new ApiResponse<bool>()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Review Status updated successfully",
                Data = result
            };
        }
    }
}
