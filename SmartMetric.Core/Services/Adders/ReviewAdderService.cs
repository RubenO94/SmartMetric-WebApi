using Microsoft.Extensions.Logging;
using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Core.DTO.AddRequest;
using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.Exceptions;
using SmartMetric.Core.ServicesContracts.Adders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.Services.Adders
{
    public class ReviewAdderService : IReviewAdderService
    {
        private readonly IReviewRepository _reviewRepository;
        private ILogger<ReviewAdderService> _logger;

        public ReviewAdderService(IReviewRepository reviewRepository, ILogger<ReviewAdderService> logger)
        {
            _reviewRepository = reviewRepository;
            _logger = logger;
        }

        public async Task<ApiResponse<ReviewDTOResponse?>> AddReview(ReviewDTOAddRequest? request)
        {
            _logger.LogInformation($"{nameof(ReviewAdderService)}.{nameof(AddReview)} foi iniciado");

            if(request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if(request.CreatedByUserId == null)
            {
                throw new ArgumentNullException(nameof(request.CreatedByUserId));
            }

            if (request.CreatedDate == null)
            {
                throw new ArgumentNullException(nameof(request.CreatedDate));
            }

            if (request.SubjectType == null)
            {
                throw new ArgumentNullException(nameof(request.SubjectType));
            }

            if (request.ReviewType == null)
            {
                throw new ArgumentNullException(nameof(request.ReviewType));
            }

            if (request.Translations == null)
            {
                throw new ArgumentNullException(nameof(request.Translations));
            }

            if (!request.Translations.Any())
            {
                throw new HttpStatusException(HttpStatusCode.BadRequest, "Review should atleast have one translation");
            }

            var reviewId = Guid.NewGuid();

            foreach (var translation in request.Translations)
            {
                translation.ReviewId = reviewId;
            }

            Review review = request.ToReview();
            review.ReviewId = reviewId;

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
