using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.DTO.AddRequest;
using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.Enums;
using SmartMetric.Core.Services.Deleters;
using SmartMetric.Core.ServicesContracts.Adders;
using SmartMetric.Core.ServicesContracts.Deleters;
using SmartMetric.Core.ServicesContracts.Getters;
using System.Net;

namespace SmartMetric.WebAPI.Controllers.v1
{
    [ApiVersion("1.0")]
    public class RatingOptionsController : CustomBaseController
    {
        //VARIABLES
        private readonly IRatingOptionAdderService _ratingOptionAdderService;
        private readonly IRatingOptionGetterService _ratingOptionGetterService;
        private readonly IRatingOptionDeleterService _ratingOptionDeleterService;
        private readonly IRatingOptionTranslationAdderService _ratingOptionTranslationAdderService;
        private readonly IRatingOptionTranslationDeleterService _ratingOptionTranslationDeleterService;

        //CONSTRUCTOR
        public RatingOptionsController(
            IRatingOptionAdderService ratingOptionAdderService,
            IRatingOptionGetterService ratingOptionGetterService,
            IRatingOptionDeleterService ratingOptionDeleterService,
            IRatingOptionTranslationAdderService ratingOptionTranslationAdderService,
            IRatingOptionTranslationDeleterService ratingOptionTranslationDeleterService
        )
        {
            _ratingOptionAdderService = ratingOptionAdderService;
            _ratingOptionGetterService = ratingOptionGetterService;
            _ratingOptionDeleterService = ratingOptionDeleterService;
            _ratingOptionTranslationAdderService = ratingOptionTranslationAdderService;
            _ratingOptionTranslationDeleterService = ratingOptionTranslationDeleterService;
        }

        //ENDPOINTS
        #region Post method to add new RatingOption

        [HttpPost]
        public async Task<IActionResult> AddRatingOption([FromQuery] Guid questionId, [FromBody] RatingOptionDTOAddRequest ratingOptionDTOAddRequest)
        {

            ratingOptionDTOAddRequest.QuestionId = questionId;
            var response = await _ratingOptionAdderService.AddRatingOption(ratingOptionDTOAddRequest);

            return CreatedAtAction(nameof(AddRatingOption), response);
        }

        #endregion

        #region Post method to add new Translation to existing RatingOption

        [HttpPost]
        [Route("Translation")]
        public async Task<IActionResult> AddRatingOptionTranslation([FromQuery] Guid ratingOptionId, [FromBody] RatingOptionTranslationDTOAddRequest ratingOptionTranslationDTOAddRequest)
        {
            ratingOptionTranslationDTOAddRequest.RatingOptionId = ratingOptionId;
            var response = await _ratingOptionTranslationAdderService.AddRatingOptionTranslation(ratingOptionTranslationDTOAddRequest);

            return CreatedAtAction(nameof(AddRatingOptionTranslation), response);
        }

        #endregion

        #region Delete method to delete existing RatingOption

        [HttpDelete]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteRatingOptionById (Guid? ratingOptionId)
        {
            var response = await _ratingOptionDeleterService.DeleteRatingOptionById(ratingOptionId);
            return response;
        }

        #endregion

        #region Delete method to delete existing Translation from existing RatingOption

        [HttpDelete]
        [Route("Translation")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteRatingOptionTranslationById([FromQuery] Language language, [FromQuery] Guid ratingOptionId)
        {
            var response = await _ratingOptionTranslationDeleterService.DeleteRatingOptionTranslationById(ratingOptionId, language);
            return response;
        }

        #endregion

        #region Get method to get all RatingOption of questionId received in parameter

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RatingOptionDTOResponse>>> GetRatingOptionsByQuestionId(Guid questionId)
        {
            var ratingOptionList = await _ratingOptionGetterService.GetRatingOptionsByQuestionId(questionId);
            return Ok(ratingOptionList);
        }

        #endregion
    }
}
