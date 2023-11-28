using Microsoft.AspNetCore.Mvc;
using SmartMetric.Core.DTO.AddRequest;
using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.Enums;
using SmartMetric.Core.Services.Adders;
using SmartMetric.Core.ServicesContracts.Adders;
using SmartMetric.Core.ServicesContracts.Deleters;
using SmartMetric.Core.ServicesContracts.Getters;

namespace SmartMetric.WebAPI.Controllers.v1
{
    [ApiVersion("1.0")]
    //[RequestValidation]
    public class FormTemplatesController : CustomBaseController
    {
        private readonly IFormTemplatesGetterService _formTemplateGetterService;
        private readonly IFormTemplatesAdderService _formTemplateAdderService;
        private readonly IFormTemplatesDeleterService _formTemplatesDeleterService;

        private readonly IQuestionAdderService _questionAdderService;

        private readonly IFormTemplateTranslationsAdderService _formTemplateTranslationsAdderService;
        private readonly IFormTemplateTranslationsDeleterService _formTemplateTranslationsDeleterService;

        public FormTemplatesController(
            IFormTemplatesGetterService formTemplateGetterService,
            IFormTemplatesAdderService formTemplatesAdderService,
            IFormTemplatesDeleterService formTemplatesDeleterService,
            IQuestionAdderService questionAdderService,
            IFormTemplateTranslationsAdderService formTemplateTranslationsAdderService,
            IFormTemplateTranslationsDeleterService formTemplateTranslationsDeleterService
        )
        {
            _formTemplateGetterService = formTemplateGetterService;
            _formTemplateAdderService = formTemplatesAdderService;
            _formTemplatesDeleterService = formTemplatesDeleterService;

            _questionAdderService = questionAdderService;

            _formTemplateTranslationsAdderService = formTemplateTranslationsAdderService;
            _formTemplateTranslationsDeleterService = formTemplateTranslationsDeleterService;
        }


        /// <summary>
        /// Retorna todos os FormTemplates
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FormTemplateDTOResponse>>> GetAllFormTemplates()
        {
            var formTemplates = await _formTemplateGetterService.GetAllFormTemplates();

            return Ok(formTemplates);
        }

        [HttpGet("{formTemplateId}")]
        public async Task<ActionResult<FormTemplateDTOResponse>> GetFormTemplateById(Guid? formTemplateId)
        {
            var formTemplate = await _formTemplateGetterService.GetFormTemplateById(formTemplateId);
            return Ok(formTemplate);
        }

        [HttpPost]
        public async Task<IActionResult> AddFormTemplate([FromBody] FormTemplateDTOAddRequest? formTemplateDTOAddRequest)
        {
            var response = await _formTemplateAdderService.AddFormTemplate(formTemplateDTOAddRequest);
            return CreatedAtAction(nameof(AddFormTemplate), response);
        }

        [HttpPost]
        [Route("{formTemplateId}/Translations")]
        public async Task<IActionResult> AddFormTemplateTranslation(Guid? formTemplateId, [FromBody] FormTemplateTranslationDTOAddRequest? formTemplateTranslationDTOAddRequest)
        {
            var translation = await _formTemplateTranslationsAdderService.AddFormTemplateTranslation(formTemplateId, formTemplateTranslationDTOAddRequest);

            return CreatedAtAction(nameof(AddFormTemplateTranslation), translation);
        }

        [HttpPost]
        [Route("{formTemplateId}/Questions")]
        public async Task<IActionResult> AddQuestionToFormTemplate(Guid? formTemplateId, [FromBody] QuestionDTOAddRequest questionDTOAddRequest)
        {

            var response = await _questionAdderService.AddQuestionToFormTemplate(formTemplateId, questionDTOAddRequest);
            return Ok(response);
        }

        [HttpDelete("{formTemplateId}")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteFormTemplateById(Guid? formTemplateId)
        {
            var response = await _formTemplatesDeleterService.DeleteFormTemplateById(formTemplateId);
            return response;
        }

        [HttpDelete("{formTemplateId}/Translations/{language}")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteFormTemplateTranslation(Guid? formTemplateId, Language language)
        {
            var response = await _formTemplateTranslationsDeleterService.DeleteFormTemplateTranslationById(formTemplateId, language);
            return response;
        }
    }
}
