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
        public async Task<ActionResult<ApiResponse<bool>>> DeleteSingleChoiceOptionById ([FromQuery] Guid singleChoiceOptionId)
        {
            var response = await _singleChoiceOptionsDeleterService.DeleteSingleChoiceOptionById(singleChoiceOptionId);
            return response;
        }

        #endregion

        #region Delete to remove existing Translation from existing Translation

        [HttpDelete]
        [Route("Translation")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteSingleChoiceOptionTranslationById([FromQuery] Language language, [FromQuery] Guid singleChoiceOptionId)
        {
            var response = await _singleChoiceOptionTranslationsDeleterService.DeleteSingleChoiceOptionTranslationById(singleChoiceOptionId, language);
            return response;
        }

        #endregion
    }
}
