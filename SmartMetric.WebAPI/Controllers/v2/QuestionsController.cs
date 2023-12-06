using Microsoft.AspNetCore.Mvc;
using SmartMetric.Core.DTO.AddRequest;
using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.Enums;
using SmartMetric.Core.ServicesContracts.Questions;
using SmartMetric.Core.ServicesContracts.QuestionTranslations;
using SmartMetric.Core.ServicesContracts.RatingOptions;
using SmartMetric.Core.ServicesContracts.SingleChoiceOptions;

namespace SmartMetric.WebAPI.Controllers.v2
{
    [ApiVersion("2.0")]
    public class QuestionsController : CustomBaseController
    {
        private readonly IQuestionAdderService _questionAdderService;
        private readonly IQuestionGetterService _questionGetterService;
        private readonly IQuestionTranslationAdderService _questionTranslationsAdderService;
        private readonly IQuestionTranslationDeleterService _questionTranslationDeleterService;
        private readonly IRatingOptionAdderService _ratingOptionAdderService;
        private readonly ISingleChoiceOptionAdderService _singleChoiceOptionsAdderService;


        public QuestionsController(IQuestionAdderService questionAdderService, IQuestionGetterService questionGetterService, IQuestionTranslationAdderService questionTranslationsAdderService, IQuestionTranslationDeleterService questionTranslationDeleterService, IRatingOptionAdderService ratingOptionAdderService, ISingleChoiceOptionAdderService singleChoiceOptionsAdderService)
        {
            _questionAdderService = questionAdderService;
            _questionGetterService = questionGetterService;
            _questionTranslationsAdderService = questionTranslationsAdderService;
            _questionTranslationDeleterService = questionTranslationDeleterService;
            _ratingOptionAdderService = ratingOptionAdderService;
            _singleChoiceOptionsAdderService = singleChoiceOptionsAdderService;
        }


        [HttpPost]
        [Route("{questionId}/Translations")]
        public async Task<IActionResult> AddQuestionTranslation(Guid? questionId, [FromBody] TranslationDTOAddRequest questionTranslationDTOAddRequest)
        {

            var response = await _questionTranslationsAdderService.AddQuestionTranslation(questionId, questionTranslationDTOAddRequest);
            return CreatedAtAction(nameof(AddQuestionTranslation), response);
        }

        [HttpDelete]
        [Route("{questionId}/Translations")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteQuestionTranslationById(Guid? questionId, [FromQuery] Language language)
        {
            var response = await _questionTranslationDeleterService.DeleteQuestionTranslationById(questionId, language);
            return response;
        }

        [HttpPost("{questionId}/RatingOption")]
        public async Task<IActionResult> AddRatingOption(Guid? questionId, [FromBody] RatingOptionDTOAddRequest ratingOptionDTOAddRequest)
        {

            var response = await _ratingOptionAdderService.AddRatingOption(questionId, ratingOptionDTOAddRequest);

            return CreatedAtAction(nameof(AddRatingOption), response);
        }

        [HttpPost("{questionId}/SingleChoiceOption")]
        public async Task<IActionResult> AddSingleChoiceOption(Guid? questionId, [FromBody] SingleChoiceOptionDTOAddRequest singleChoiceOptionDTOAddRequest)
        {
            var response = await _singleChoiceOptionsAdderService.AddSingleChoiceOption(questionId, singleChoiceOptionDTOAddRequest);

            return CreatedAtAction(nameof(AddSingleChoiceOption), response);
        }
    }
}
