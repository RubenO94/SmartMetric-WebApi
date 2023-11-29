﻿using Microsoft.Extensions.Logging;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.ServicesContracts.Deleters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.Services.Deleters
{
    public class ReviewDeleterService : IReviewDeleterService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly ILogger<ReviewDeleterService> _logger;

        public ReviewDeleterService(IReviewRepository reviewRepository, ILogger<ReviewDeleterService> logger)
        {
            _reviewRepository = reviewRepository;
            _logger = logger;
        }

        public async Task<ApiResponse<bool>> DeleteReviewById(Guid? reviewId)
        {
            _logger.LogInformation($"{nameof(ReviewDeleterService)}.{nameof(DeleteReviewById)} foi iniciado");

            if (reviewId == null) throw new ArgumentNullException(nameof(reviewId));

            var associatedReview = await _reviewRepository.GetReviewById(reviewId.Value);

            if (associatedReview == null) throw new ArgumentNullException("Review doesn't exist");

            var result = await _reviewRepository.DeleteReview(reviewId.Value);

            return new ApiResponse<bool>()
            {
                StatusCode = (int)HttpStatusCode.NoContent,
                Message = "Review removed succesfuly",
                Data = result
            };

        }
    }
}
