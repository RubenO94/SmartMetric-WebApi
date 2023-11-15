using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartMetric.Core.DTO.AddRequest;
using SmartMetric.Core.ServicesContracts.Adders;
using SmartMetric.Core.ServicesContracts.Getters;

namespace SmartMetric.WebAPI.Controllers.v1
{
    [ApiVersion("1.0")]
    public class QuestionsController : CustomBaseController
    {
        private readonly IQuestionAdderService _questionAdderService;
        private readonly IQuestionGetterService _questionGetterService;

        public QuestionsController(IQuestionAdderService questionAdderService, IQuestionGetterService questionGetterService)
        {
            _questionAdderService = questionAdderService;
            _questionGetterService = questionGetterService;
        }


        [HttpPost]
        public async Task<IActionResult> AddQuestionToFormTemplate([FromQuery] Guid formTemplateId, [FromBody] QuestionDTOAddRequest questionDTOAddRequest)
        {
            questionDTOAddRequest.FormTemplateId = formTemplateId;

            var response = await _questionAdderService.AddQuestion(questionDTOAddRequest);

            if(response != null)
            {
                Ok(response);
            }
            return BadRequest();
        }
    }
}
