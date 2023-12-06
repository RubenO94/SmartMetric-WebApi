using Microsoft.AspNetCore.Mvc;
using SmartMetric.Core.DTO.AddRequest;
using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.Enums;
using SmartMetric.Core.ServicesContracts.Questions;
using SmartMetric.Core.ServicesContracts.SingleChoiceOptions;
using SmartMetric.Core.ServicesContracts.SingleChoiceOptionTranslations;

namespace SmartMetric.WebAPI.Controllers.v2
{
    [ApiVersion("2.0")]
    public class SingleChoiceOptionsController : CustomBaseController
    {
        //VARIABLES
        private readonly ISingleChoiceOptionDeleterService _singleChoiceOptionsDeleterService;
        private readonly ISingleChoiceOptionGetterService _singleChoiceOptionsGetterService;
        private readonly ISingleChoiceOptionTranslationsAdderService _singleChoiceOptionTranslationsAdderService;
        private readonly ISingleChoiceOptionTranslationDeleterService _singleChoiceOptionTranslationsDeleterService;
        private readonly IQuestionGetterService _questionGetterService;

        //CONSTRUCTOR
        public SingleChoiceOptionsController(
            ISingleChoiceOptionDeleterService singleChoiceOptionDeleterService,
            ISingleChoiceOptionGetterService singleChoiceOptionGetterService,
            ISingleChoiceOptionTranslationsAdderService singleChoiceOptionTranslationsAdderService,
            ISingleChoiceOptionTranslationDeleterService singleChoiceOptionTranslationDeleterService,
            IQuestionGetterService questionGetterService
        )
        {
            _singleChoiceOptionsDeleterService = singleChoiceOptionDeleterService;
            _singleChoiceOptionsGetterService = singleChoiceOptionGetterService;
            _singleChoiceOptionTranslationsAdderService = singleChoiceOptionTranslationsAdderService;
            _singleChoiceOptionTranslationsDeleterService = singleChoiceOptionTranslationDeleterService;
            _questionGetterService = questionGetterService;
        }

        //ENDPOINTS

        #region Post to add new Translation to existing SingleChoiceOption

        [HttpPost]
        [Route("Translations")]
        public async Task<IActionResult> AddSingleChoiceOptionTranslation([FromQuery] Guid singleChoiceOptionId, [FromBody] TranslationDTOAddRequest singleChoiceOptionTranslationDTOAddRequest)
        {
            var response = await _singleChoiceOptionTranslationsAdderService.AddSingleChoiceOptionTranslation(singleChoiceOptionId, singleChoiceOptionTranslationDTOAddRequest);

            return CreatedAtAction(nameof(AddSingleChoiceOptionTranslation), response);
        }

        #endregion

        #region Delete to remove existing SingleChoiceQuestion

        [HttpDelete("{singleChoiceOptionId}")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteSingleChoiceOptionById(Guid? singleChoiceOptionId)
        {
            var response = await _singleChoiceOptionsDeleterService.DeleteSingleChoiceOptionById(singleChoiceOptionId);
            return response;
        }

        #endregion

        #region Delete to remove existing Translation from existing Translation

        [HttpDelete]
        [Route("{singleChoiceOptionId}/Translations/{language}")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteSingleChoiceOptionTranslationById(Language language, Guid? singleChoiceOptionId)
        {
            var response = await _singleChoiceOptionTranslationsDeleterService.DeleteSingleChoiceOptionTranslationById(singleChoiceOptionId, language);
            return response;
        }

        #endregion
    }
}
