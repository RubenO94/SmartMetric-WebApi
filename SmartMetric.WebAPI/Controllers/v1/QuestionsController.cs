using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartMetric.Core.DTO.AddRequest;
using SmartMetric.Core.ServicesContracts.Adders;
using SmartMetric.Core.ServicesContracts.Getters;
using System.Net;

namespace SmartMetric.WebAPI.Controllers.v1
{
    [ApiVersion("1.0")]
    public class QuestionsController : CustomBaseController
    {
        private readonly IQuestionAdderService _questionAdderService;
        private readonly IQuestionGetterService _questionGetterService;
        private readonly IFormTemplatesGetterService _formTemplatesGetterService;

        public QuestionsController(IQuestionAdderService questionAdderService, IQuestionGetterService questionGetterService, IFormTemplatesGetterService formTemplatesGetterService)
        {
            _questionAdderService = questionAdderService;
            _questionGetterService = questionGetterService;
            _formTemplatesGetterService = formTemplatesGetterService;
        }


        [HttpPost]
        public async Task<IActionResult> AddQuestionToFormTemplate([FromQuery] Guid formTemplateId, [FromBody] QuestionDTOAddRequest questionDTOAddRequest)
        {
            var formTemplate = await _formTemplatesGetterService.GetFormTemplateById(formTemplateId);

            if (formTemplate != null)
            {
                questionDTOAddRequest.FormTemplateId = formTemplateId;

                var response = await _questionAdderService.AddQuestionToFormTemplate(questionDTOAddRequest);

                if (response != null)
                {
                    return Ok(response);
                }
                return BadRequest(new
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "Something went wrong. Please check the provided data."
                });
            }

            return NotFound(new
            {
                StatusCode = (int)HttpStatusCode.NotFound,
                Message = $"FormTemplate with ID {formTemplateId} not found."
            });
        }
    }
}
