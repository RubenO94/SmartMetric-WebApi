using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartMetric.Core.DTO.AddRequest;
using SmartMetric.Core.ServicesContracts.Adders;
using SmartMetric.Core.ServicesContracts.Deleters;
using SmartMetric.Core.ServicesContracts.Getters;
using System.Net;

namespace SmartMetric.WebAPI.Controllers.v1
{
    [ApiVersion("1.0")]
    public class RatingOptionController : CustomBaseController
    {
        //VARIABLES
        private readonly IRatingOptionAdderService _ratingOptionAdderService;
        private readonly IRatingOptionGetterService _ratingOptionGetterService;
        private readonly IRatingOptionDeleterService _ratingOptionDeleterService;
        private readonly IRatingOptionTranslationAdderService _ratingOptionTranslationAdderService;
        private readonly IRatingOptionTranslationDeleterService _ratingOptionTranslationDeleterService;
        private readonly IQuestionGetterService _questionGetterService;

        //CONSTRUCTOR
        public RatingOptionController (
            IRatingOptionAdderService ratingOptionAdderService,
            IRatingOptionGetterService ratingOptionGetterService,
            IRatingOptionDeleterService ratingOptionDeleterService,
            IRatingOptionTranslationAdderService ratingOptionTranslationAdderService,
            IRatingOptionTranslationDeleterService ratingOptionTranslationDeleterService,
            IQuestionGetterService questionGetterService
        )
        {
            _ratingOptionAdderService = ratingOptionAdderService;
            _ratingOptionGetterService = ratingOptionGetterService;
            _ratingOptionDeleterService = ratingOptionDeleterService;
            _ratingOptionTranslationAdderService = ratingOptionTranslationAdderService;
            _ratingOptionTranslationDeleterService = ratingOptionTranslationDeleterService;
            _questionGetterService = questionGetterService;
        }

        //ENDPOINTS
        #region Post to add new RatingOption

        [HttpPost]
        public async Task<IActionResult> AddRatingOption([FromQuery] Guid questionId, [FromBody] RatingOptionDTOAddRequest ratingOptionDTOAddRequest)
        {
            var questionExist = await _questionGetterService.GetQuestionById(questionId);
            if (questionExist != null)
            {
                ratingOptionDTOAddRequest.QuestionId = questionId;
                var response = await _ratingOptionAdderService.AddRatingOption(ratingOptionDTOAddRequest);

                return CreatedAtAction(nameof(AddRatingOption), new
                {
                    StatusCode = (int)HttpStatusCode.Created,
                    Message = "RatingOption created",
                    RatingOptionId = response?.RatingOptionId.ToString()
                });
            }

            return BadRequest(new
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Message = "Question doesn't exist"
            });
        }

        #endregion

        #region Post to add new Translation to existing RatingOption

        [HttpPost]
        [Route("Translation")]
        public async Task<IActionResult> AddRatingOptionTranslation([FromQuery] Guid ratingOptionId, [FromBody] RatingOptionTranslationDTOAddRequest ratingOptionTranslationDTOAddRequest)
        {
            var ratingOptionExist = await _ratingOptionGetterService.GetRatingOptionById(ratingOptionId);
            if (ratingOptionExist != null)
            {
                ratingOptionTranslationDTOAddRequest.RatingOptionId = ratingOptionId;
                var response = await _ratingOptionTranslationAdderService.AddRatingOptionTranslation(ratingOptionTranslationDTOAddRequest);

                return CreatedAtAction(nameof(AddRatingOptionTranslation), new
                {
                    StatusCode = (int)HttpStatusCode.Created,
                    Message = "RatingOption Translation Created",
                    RatingOptionTranslationId = response.RatingOptionTranslationId.ToString(),
                });
            }

            return BadRequest(new
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Message = "RatingOption doesn't exist"
            });
        }

        #endregion

        #region Delete to delete existing RatingOption

        [HttpDelete]
        public async Task<IActionResult> DeleteRatingOptionById (Guid ratingOptionId)
        {
            await _ratingOptionDeleterService.DeleteRatingOptionById(ratingOptionId);
            return NoContent();
        }

        #endregion

        #region Delete to delete existing Translation from existing RatingOption

        [HttpDelete]
        [Route("Translation")]
        public async Task<IActionResult> DeleteRatingOptionTranslationById(Guid ratingOptionTranslationId)
        {
            await _ratingOptionTranslationDeleterService.DeleteRatingOptionTranslationById(ratingOptionTranslationId);
            return NoContent();
        }

        #endregion
    }
}
