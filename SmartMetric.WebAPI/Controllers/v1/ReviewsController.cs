using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.DTO.AddRequest;
using SmartMetric.Core.DTO.UpdateRequest;
using SmartMetric.Core.ServicesContracts;
using SmartMetric.Core.ServicesContracts.Adders;
using SmartMetric.Core.ServicesContracts.Deleters;
using SmartMetric.Infrastructure.DatabaseContext;

namespace SmartMetric.WebAPI.Controllers.v1
{
    [ApiVersion("1.0")]
    public class ReviewsController : CustomBaseController
    {
        private readonly ISmartTimeService _smartTimeService;
        private readonly IReviewAdderService _reviewAdderService;
        private readonly IReviewDeleterService _reviewDeleterService;

        public ReviewsController(ISmartTimeService smartTimeService, IReviewAdderService reviewAdderService, IReviewDeleterService reviewDeleterService)
        {
            _smartTimeService = smartTimeService;
            _reviewAdderService = reviewAdderService;
            _reviewDeleterService = reviewDeleterService;
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
        public async Task<IActionResult> DeleteReview(Guid? reviewId)
        {
            var response = await _reviewDeleterService.DeleteReviewById(reviewId);

            if (response.Data == true) return NoContent();
            return BadRequest();
        }

        [HttpPut("{reviewId}")]
        public async Task<IActionResult> UpdateReview(Guid? reviewId, [FromBody] ReviewDTOUpdate? reviewDTOUpdate)
        {
            //TODO: Review Update Endpoint
            throw new NotImplementedException();
        }
    }
}
