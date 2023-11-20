using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartMetric.Core.DTO.AddRequest;
using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.Enums;
using SmartMetric.Core.ServicesContracts.Adders;
using SmartMetric.Core.ServicesContracts.Deleters;
using SmartMetric.Core.ServicesContracts.Getters;
using System.Net;

namespace SmartMetric.WebAPI.Controllers.v1
{
    [ApiVersion("1.0")]
    public class QuestionsController : CustomBaseController
    {
        private readonly IQuestionAdderService _questionAdderService;
        private readonly IQuestionGetterService _questionGetterService;
        private readonly IQuestionTranslationAdderService _questionTranslationsAdderService;
        private readonly IQuestionTranslationDeleterService _questionTranslationDeleterService;

        public QuestionsController(IQuestionAdderService questionAdderService, IQuestionGetterService questionGetterService, IQuestionTranslationAdderService questionTranslationsAdderService, IQuestionTranslationDeleterService questionTranslationDeleterService)
        {
            _questionAdderService = questionAdderService;
            _questionGetterService = questionGetterService;
            _questionTranslationsAdderService = questionTranslationsAdderService;
            _questionTranslationDeleterService = questionTranslationDeleterService;
        }


        [HttpGet]
        [Route("FormTemplateQuestions")]
        public async Task<IActionResult> GetAllQuestionsByFormTemplateId([FromQuery] Guid formTemplateId)
        {
            var questions = await _questionGetterService.GetQuestionsByFormTemplateId(formTemplateId);
            return Ok(questions);
        }


        [HttpPost]
        [Route("FormTemplateQuestions")]
        public async Task<IActionResult> AddQuestionToFormTemplate([FromQuery] Guid formTemplateId, [FromBody] QuestionDTOAddRequest questionDTOAddRequest)
        {
            questionDTOAddRequest.FormTemplateId = formTemplateId;

            var response = await _questionAdderService.AddQuestionToFormTemplate(questionDTOAddRequest);
            return Ok(response);
        }

        [HttpPost]
        [Route("Translation")]
        public async Task<IActionResult> AddQuestionTranslation([FromQuery] Guid questionId, [FromBody] QuestionTranslationDTOAddRequest questionTranslationDTOAddRequest)
        {
            questionTranslationDTOAddRequest.QuestionId = questionId;

            var response = await _questionTranslationsAdderService.AddQuestionTranslation(questionTranslationDTOAddRequest);
            return CreatedAtAction(nameof(AddQuestionTranslation), response);
        }

        [HttpDelete]
        [Route("Translation")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteQuestionTranslationById([FromQuery] Guid questionId, [FromQuery] Language language)
        {
            var response = await _questionTranslationDeleterService.DeleteQuestionTranslationById(questionId, language);
            return response;
        }
    }
}
