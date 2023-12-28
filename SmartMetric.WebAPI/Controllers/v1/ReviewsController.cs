using Microsoft.AspNetCore.Mvc;
using SmartMetric.Core.DTO.AddRequest;
using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.DTO.UpdateRequest;
using SmartMetric.Core.Enums;
using SmartMetric.Core.ServicesContracts.Reviews;

namespace SmartMetric.WebAPI.Controllers.v1
{
    [ApiVersion("1.0")]
    public class ReviewsController : CustomBaseController
    {
        private readonly IReviewGetterService _reviewGetterService;
        private readonly IReviewAdderService _reviewAdderService;
        private readonly IReviewDeleterService _reviewDeleterService;
        private readonly IReviewUpdaterService _reviewUpdaterService;

        public ReviewsController(IReviewGetterService reviewGetterService, IReviewAdderService reviewAdderService, IReviewDeleterService reviewDeleterService, IReviewUpdaterService reviewUpdaterService)
        {
            _reviewGetterService = reviewGetterService;
            _reviewAdderService = reviewAdderService;
            _reviewDeleterService = reviewDeleterService;
            _reviewUpdaterService = reviewUpdaterService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReviews(int page = 1, int pageSize = 20) 
        {
            var response = await _reviewGetterService.GetReviews(page, pageSize);
            return Ok(response);
        }

        [HttpGet("{reviewId}")]
        public async Task<IActionResult> GetReviewById(Guid? reviewId)
        {
            var review = await _reviewGetterService.GetReviewById(reviewId);
            return Ok(review);
        }

        [HttpPost]
        public async Task<IActionResult> AddReview([FromBody] ReviewDTOAddRequest? request)
        {
            var response = await _reviewAdderService.AddReview(request);
            if (response.Data != null)
                return Ok(response);

            return BadRequest(response);
        }

        [HttpDelete("{reviewId}")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteReview(Guid? reviewId)
        {
            var response = await _reviewDeleterService.DeleteReviewById(reviewId);
            return response;
        }

        [HttpPut("{reviewId}")]
        public async Task<IActionResult> UpdateReview(Guid? reviewId, [FromBody] ReviewDTOUpdate? reviewDTOUpdate)
        {
            //TODO: Review Update Endpoint
            var response = await _reviewUpdaterService.UpdateReview(reviewId, reviewDTOUpdate);

            return Ok(response);
        }

        [HttpPatch("{reviewId}")]
        public async Task<IActionResult> UpdateReviewStatus(Guid? reviewId, [FromBody] ReviewDTOUpdateStatus review)
        {
            var response = await _reviewUpdaterService.UpdateReviewStatus(reviewId, review);
            return Ok(response);
            
        }
    }
}
