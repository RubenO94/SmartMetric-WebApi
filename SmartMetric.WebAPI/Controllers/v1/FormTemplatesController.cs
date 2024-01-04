using Microsoft.AspNetCore.Mvc;
using SmartMetric.Core.DTO.AddRequest;
using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.DTO.UpdateRequest;
using SmartMetric.Core.Enums;
using SmartMetric.Core.ServicesContracts.FormTemplates;
using SmartMetric.Core.ServicesContracts.FormTemplateTranslations;
using SmartMetric.Core.ServicesContracts.Questions;
using SmartMetric.WebAPI.Filters.ActionFilter;

namespace SmartMetric.WebAPI.Controllers.v1
{
    /// <summary>
    /// 
    /// </summary>
    [ApiVersion("1.0")]
    //[RequestValidation]
    public class FormTemplatesController : CustomBaseController
    {
        private readonly IFormTemplateGetterService _formTemplateGetterService;
        private readonly IFormTemplateAdderService _formTemplateAdderService;
        private readonly IFormTemplateDeleterService _formTemplatesDeleterService;
        private readonly IFormTemplateUpdaterService _formTemplatesUpdaterService;

        private readonly IQuestionAdderService _questionAdderService;

        private readonly IFormTemplateTranslationsAdderService _formTemplateTranslationsAdderService;
        private readonly IFormTemplateTranslationsDeleterService _formTemplateTranslationsDeleterService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="formTemplateGetterService"></param>
        /// <param name="formTemplatesAdderService"></param>
        /// <param name="formTemplatesDeleterService"></param>
        /// <param name="formTemplatesUpdaterService"></param>
        /// <param name="questionAdderService"></param>
        /// <param name="formTemplateTranslationsAdderService"></param>
        /// <param name="formTemplateTranslationsDeleterService"></param>
        public FormTemplatesController(
            IFormTemplateGetterService formTemplateGetterService,
            IFormTemplateAdderService formTemplatesAdderService,
            IFormTemplateDeleterService formTemplatesDeleterService,
            IFormTemplateUpdaterService formTemplatesUpdaterService,
            IQuestionAdderService questionAdderService,
            IFormTemplateTranslationsAdderService formTemplateTranslationsAdderService,
            IFormTemplateTranslationsDeleterService formTemplateTranslationsDeleterService
        )
        {
            _formTemplateGetterService = formTemplateGetterService;
            _formTemplateAdderService = formTemplatesAdderService;
            _formTemplatesDeleterService = formTemplatesDeleterService;
            _formTemplatesUpdaterService = formTemplatesUpdaterService;

            _questionAdderService = questionAdderService;

            _formTemplateTranslationsAdderService = formTemplateTranslationsAdderService;
            _formTemplateTranslationsDeleterService = formTemplateTranslationsDeleterService;
        }


        /// <summary>
        /// Retorna todos os FormTemplates
        /// </summary>
        /// <returns></returns>
        [PermissionRequired(WindowType.FormTemplates, PermissionType.Read)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FormTemplateDTOResponse>>> GetAllFormTemplates(int page = 1, int pageSize = 20, Language? language = null)
        {
            var formTemplates = await _formTemplateGetterService.GetAllFormTemplates(page, pageSize, language);

            return Ok(formTemplates);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="formTemplateId"></param>
        /// <returns></returns>
        [HttpGet("{formTemplateId}")]
        [PermissionRequired(WindowType.FormTemplates, PermissionType.Read)]
        public async Task<ActionResult<FormTemplateDTOResponse>> GetFormTemplateById(Guid? formTemplateId)
        {
            var formTemplate = await _formTemplateGetterService.GetFormTemplateById(formTemplateId);
            return Ok(formTemplate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="formTemplateDTOAddRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [PermissionRequired(WindowType.FormTemplates, PermissionType.Create)]
        public async Task<IActionResult> AddFormTemplate([FromBody] FormTemplateDTOAddRequest? formTemplateDTOAddRequest)
        {
            var response = await _formTemplateAdderService.AddFormTemplate(formTemplateDTOAddRequest);
            return CreatedAtAction(nameof(AddFormTemplate), response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="formTemplateId"></param>
        /// <param name="formTemplateTranslationDTOAddRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{formTemplateId}/Translations")]
        [PermissionRequired(WindowType.FormTemplates, PermissionType.Create)]
        public async Task<IActionResult> AddFormTemplateTranslation(Guid? formTemplateId, [FromBody] TranslationDTOAddRequest? formTemplateTranslationDTOAddRequest)
        {
            var translation = await _formTemplateTranslationsAdderService.AddFormTemplateTranslation(formTemplateId, formTemplateTranslationDTOAddRequest);

            return CreatedAtAction(nameof(AddFormTemplateTranslation), translation);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="formTemplateId"></param>
        /// <param name="questionDTOAddRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{formTemplateId}/Questions")]
        [PermissionRequired(WindowType.FormTemplates, PermissionType.Create)]
        public async Task<IActionResult> AddQuestionToFormTemplate(Guid? formTemplateId, [FromBody] QuestionDTOAddRequest questionDTOAddRequest)
        {

            var response = await _questionAdderService.AddQuestionToFormTemplate(formTemplateId, questionDTOAddRequest);
            return Ok(response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="formTemplateId"></param>
        /// <returns></returns>
        [HttpDelete("{formTemplateId}")]
        [PermissionRequired(WindowType.FormTemplates, PermissionType.Delete)]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteFormTemplateById(Guid? formTemplateId)
        {
            var response = await _formTemplatesDeleterService.DeleteFormTemplateById(formTemplateId);
            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="formTemplateId"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        [HttpDelete("{formTemplateId}/Translations")]
        [PermissionRequired(WindowType.FormTemplates, PermissionType.Delete)]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteFormTemplateTranslation(Guid? formTemplateId, [FromQuery] Language language)
        {
            var response = await _formTemplateTranslationsDeleterService.DeleteFormTemplateTranslationById(formTemplateId, language);
            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="formTemplateId"></param>
        /// <param name="formTemplate"></param>
        /// <returns></returns>
        [HttpPut("{formTemplateId}")]
        [PermissionRequired(WindowType.FormTemplates, PermissionType.Update)]
        public async Task<IActionResult> UpdateFormTemplate(Guid? formTemplateId, [FromBody] FormTemplateDTOUpdate formTemplate)
        {
            var response = await _formTemplatesUpdaterService.UpdateFormTemplate(formTemplateId, formTemplate);
            return Ok(response);
        }

    }
}
