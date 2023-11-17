using Microsoft.AspNetCore.Mvc;
using SmartMetric.Core.DTO.AddRequest;
using SmartMetric.Core.Enums;
using SmartMetric.Core.ServicesContracts.Adders;
using SmartMetric.Core.ServicesContracts.Deleters;
using SmartMetric.Core.ServicesContracts.Getters;
using System.Net;

namespace SmartMetric.WebAPI.Controllers.v1
{
    [ApiVersion("1.0")]
    public class SingleChoiceOptionsController : CustomBaseController
    {
        //VARIABLES
        private readonly ISingleChoiceOptionsAdderService _singleChoiceOptionsAdderService;
        private readonly ISingleChoiceOptionDeleterService _singleChoiceOptionsDeleterService;
        private readonly ISingleChoiceOptionGetterService _singleChoiceOptionsGetterService;
        private readonly ISingleChoiceOptionTranslationsAdderService _singleChoiceOptionTranslationsAdderService;
        private readonly ISingleChoiceOptionTranslationDeleterService _singleChoiceOptionTranslationsDeleterService;
        private readonly IQuestionGetterService _questionGetterService;

        //CONSTRUCTOR
        public SingleChoiceOptionsController(
            ISingleChoiceOptionsAdderService singleChoiceOptionsAdderService,
            ISingleChoiceOptionDeleterService singleChoiceOptionDeleterService,
            ISingleChoiceOptionGetterService singleChoiceOptionGetterService,
            ISingleChoiceOptionTranslationsAdderService singleChoiceOptionTranslationsAdderService,
            ISingleChoiceOptionTranslationDeleterService singleChoiceOptionTranslationDeleterService,
            IQuestionGetterService questionGetterService
        )
        {
            _singleChoiceOptionsAdderService = singleChoiceOptionsAdderService;
            _singleChoiceOptionsDeleterService = singleChoiceOptionDeleterService;
            _singleChoiceOptionsGetterService = singleChoiceOptionGetterService;
            _singleChoiceOptionTranslationsAdderService = singleChoiceOptionTranslationsAdderService;
            _singleChoiceOptionTranslationsDeleterService = singleChoiceOptionTranslationDeleterService;
            _questionGetterService = questionGetterService;
        }

        //ENDPOINTS
        #region Post to add new SingleChoiceOption

        [HttpPost]
        public async Task<IActionResult> AddSingleChoiceOption([FromQuery] Guid questionId, [FromBody] SingleChoiceOptionDTOAddRequest singleChoiceOptionDTOAddRequest)
        {
            var questionExist = await _questionGetterService.GetQuestionById(questionId);
            if (questionExist != null && questionExist.ResponseType == ResponseType.SingleChoice.ToString())
            {
                singleChoiceOptionDTOAddRequest.QuestionId = questionId;
                var response = await _singleChoiceOptionsAdderService.AddSingleChoiceOption(singleChoiceOptionDTOAddRequest);

                return CreatedAtAction(nameof(AddSingleChoiceOption), new
                {
                    StatusCode = (int)HttpStatusCode.Created,
                    Message = "SingleChoiceOption created",
                    SingleChoiceOptionId = response?.SingleChoiceOptionId.ToString(),
                });
            }

            return BadRequest(new
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Message = "Question doesn't exist or Question isn't from type SingleChoice"
            });
        }

        #endregion

        #region Post to add new Translation to existing SingleChoiceOption

        [HttpPost]
        [Route("Translation")]
        public async Task<IActionResult> AddSingleChoiceOptionTranslation([FromQuery] Guid singleChoiceOptionId, [FromBody] SingleChoiceOptionTranslationDTOAddRequest singleChoiceOptionTranslationDTOAddRequest)
        {
            var singleChoiceOptionExist = await _singleChoiceOptionsGetterService.GetSingleChoiceOptionById(singleChoiceOptionId);

            if (singleChoiceOptionExist != null)
            {
                singleChoiceOptionTranslationDTOAddRequest.SingleChoiceOptionId = singleChoiceOptionId;
                var response = await _singleChoiceOptionTranslationsAdderService.AddSingleChoiceOptionTranslation(singleChoiceOptionTranslationDTOAddRequest);

                return CreatedAtAction(nameof(AddSingleChoiceOptionTranslation), new
                {
                    StatusCode = (int)HttpStatusCode.Created,
                    Message = "SingleChoiceOption Translation Created",
                    RatingOptionTranslationId = response.SingleChoiceOptionTranslationId.ToString(),
                });

            }

            return BadRequest(new
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Message = "SingleChoiceOption doesn't exist"
            });
        }

        #endregion

        #region Delete to remove existing SingleChoiceQuestion

        [HttpDelete]
        public async Task<IActionResult> DeleteSingleChoiceOptionById ([FromQuery] Guid singleChoiceOptionId)
        {
            await _singleChoiceOptionsDeleterService.DeleteSingleChoiceOptionById(singleChoiceOptionId);
            return NoContent();
        }

        #endregion

        #region Delete to remove existing Translation from existing Translation

        [HttpDelete]
        [Route("Translation")]
        public async Task<IActionResult> DeleteSingleChoiceOptionTranslationById([FromQuery] Language language, [FromQuery] Guid singleChoiceOptionId)
        {
            var singleChoiceOptionExist = await _singleChoiceOptionsGetterService.GetSingleChoiceOptionById(singleChoiceOptionId);

            if (singleChoiceOptionExist == null)
            {
                return NotFound(new
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = $"SingleChoice with Id equal to {singleChoiceOptionId} not found"
                });
            }

            if (singleChoiceOptionExist.Translations == null || singleChoiceOptionExist.Translations.Count < 2)
            {
                return BadRequest(new
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = $"SingleChoice must have at least one translation."
                });
            }

            var translationToBeDeleted = singleChoiceOptionExist.Translations.FirstOrDefault(temp => temp.Language == language.ToString());

            if (translationToBeDeleted == null)
            {
                return BadRequest(new
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = $"SingleChoice doesn't have a {language} translation."
                });
            }

            var hasDeleted = await _singleChoiceOptionTranslationsDeleterService.DeleteSingleChoiceOptionTranslationById(translationToBeDeleted.SingleChoiceOptionTranslationId);
            return hasDeleted ? NoContent() : StatusCode((int)HttpStatusCode.InternalServerError);
        }

        #endregion
    }
}
