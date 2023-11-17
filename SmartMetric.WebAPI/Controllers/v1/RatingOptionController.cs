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
        public RatingOptionController(
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

            ratingOptionDTOAddRequest.QuestionId = questionId;
            var response = await _ratingOptionAdderService.AddRatingOption(ratingOptionDTOAddRequest);

            return CreatedAtAction(nameof(AddRatingOption), response);
        }

        #endregion

        #region Post to add new Translation to existing RatingOption

        [HttpPost]
        [Route("Translation")]
        public async Task<IActionResult> AddRatingOptionTranslation([FromQuery] Guid ratingOptionId, [FromBody] RatingOptionTranslationDTOAddRequest ratingOptionTranslationDTOAddRequest)
        {
            ratingOptionTranslationDTOAddRequest.RatingOptionId = ratingOptionId;
            var response = await _ratingOptionTranslationAdderService.AddRatingOptionTranslation(ratingOptionTranslationDTOAddRequest);

            return CreatedAtAction(nameof(AddRatingOptionTranslation), response);
        }

        #endregion

        #region Delete to delete existing RatingOption

        [HttpDelete]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteRatingOptionById (Guid ratingOptionId)
        {
            var response = await _ratingOptionDeleterService.DeleteRatingOptionById(ratingOptionId);
            return response;
        }

        #endregion

        #region Delete to delete existing Translation from existing RatingOption

        [HttpDelete]
        [Route("Translation")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteRatingOptionTranslationById([FromQuery] Language language, [FromQuery] Guid ratingOptionId)
        {
            var response = await _ratingOptionTranslationDeleterService.DeleteRatingOptionTranslationById(ratingOptionId, language);
            return response;
        }

        #endregion
    }
}
