﻿using Microsoft.AspNetCore.Mvc;
using SmartMetric.Core.DTO.AddRequest;
using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.Enums;
using SmartMetric.Core.ServicesContracts.RatingOptions;
using SmartMetric.Core.ServicesContracts.RatingOptionTranslations;

namespace SmartMetric.WebAPI.Controllers.v2
{
    [ApiVersion("2.0")]
    public class RatingOptionsController : CustomBaseController
    {
        //VARIABLES
        private readonly IRatingOptionGetterService _ratingOptionGetterService;
        private readonly IRatingOptionDeleterService _ratingOptionDeleterService;
        private readonly IRatingOptionTranslationAdderService _ratingOptionTranslationAdderService;
        private readonly IRatingOptionTranslationDeleterService _ratingOptionTranslationDeleterService;

        //CONSTRUCTOR
        public RatingOptionsController(

            IRatingOptionGetterService ratingOptionGetterService,
            IRatingOptionDeleterService ratingOptionDeleterService,
            IRatingOptionTranslationAdderService ratingOptionTranslationAdderService,
            IRatingOptionTranslationDeleterService ratingOptionTranslationDeleterService
        )
        {
            _ratingOptionGetterService = ratingOptionGetterService;
            _ratingOptionDeleterService = ratingOptionDeleterService;
            _ratingOptionTranslationAdderService = ratingOptionTranslationAdderService;
            _ratingOptionTranslationDeleterService = ratingOptionTranslationDeleterService;
        }

        //ENDPOINTS

        #region Post method to add new Translation to existing RatingOption

        [HttpPost]
        [Route("{ratingOptionId}/Translation")]
        public async Task<IActionResult> AddRatingOptionTranslation(Guid? ratingOptionId, [FromBody] TranslationDTOAddRequest ratingOptionTranslationDTOAddRequest)
        {
            var response = await _ratingOptionTranslationAdderService.AddRatingOptionTranslation(ratingOptionId, ratingOptionTranslationDTOAddRequest);

            return CreatedAtAction(nameof(AddRatingOptionTranslation), response);
        }

        #endregion

        #region Delete method to delete existing RatingOption

        [HttpDelete("{ratingOptionId}")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteRatingOptionById(Guid? ratingOptionId)
        {
            var response = await _ratingOptionDeleterService.DeleteRatingOptionById(ratingOptionId);
            return response;
        }

        #endregion

        #region Delete method to delete existing Translation from existing RatingOption

        [HttpDelete]
        [Route("{ratingOptionId}/Translations/{language}")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteRatingOptionTranslationById(Language language, Guid? ratingOptionId)
        {
            var response = await _ratingOptionTranslationDeleterService.DeleteRatingOptionTranslationById(ratingOptionId, language);
            return response;
        }

        #endregion

    }
}
