using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.DTO;
using SmartMetric.Core.DTO.AddRequest;
using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.Enums;
using SmartMetric.Core.Services.Adders;
using SmartMetric.Core.ServicesContracts.Adders;
using SmartMetric.Core.ServicesContracts.Deleters;
using SmartMetric.Core.ServicesContracts.Getters;
using SmartMetric.Infrastructure.DatabaseContext;
using System.Net;

namespace SmartMetric.WebAPI.Controllers.v1
{
    [ApiVersion("1.0")]
    public class FormTemplatesController : CustomBaseController
    {
        private readonly IFormTemplatesGetterService _formTemplateGetterService;
        private readonly IFormTemplatesAdderService _formTemplateAdderService;
        private readonly IFormTemplatesDeleterService _formTemplatesDeleterService;

        private readonly IFormTemplateTranslationsAdderService _formTemplateTranslationsAdderService;
        private readonly IFormTemplateTranslationsDeleterService _formTemplateTranslationsDeleterService;

        public FormTemplatesController(
            IFormTemplatesGetterService formTemplateGetterService,
            IFormTemplatesAdderService formTemplatesAdderService,
            IFormTemplatesDeleterService formTemplatesDeleterService,
            IFormTemplateTranslationsAdderService formTemplateTranslationsAdderService,
            IFormTemplateTranslationsDeleterService formTemplateTranslationsDeleterService
        )
        {
            _formTemplateGetterService = formTemplateGetterService;
            _formTemplateAdderService = formTemplatesAdderService;
            _formTemplatesDeleterService = formTemplatesDeleterService;

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
        public async Task<ActionResult<FormTemplateDTOResponse>> GetFormTemplateById(Guid formTemplateId)
        {
            var formTemplate = await _formTemplateGetterService.GetFormTemplateById(formTemplateId);
            return Ok(formTemplate);
        }

        [HttpPost]
        public async Task<IActionResult> AddFormTemplate([FromBody] FormTemplateDTOAddRequest formTemplateDTOAddRequest)
        {
            var response = await _formTemplateAdderService.AddFormTemplate(formTemplateDTOAddRequest);
            return CreatedAtAction(nameof(AddFormTemplate), response);
        }

        [HttpDelete]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteFormTemplateById(Guid? formTemplateId)
        {
            var response = await _formTemplatesDeleterService.DeleteFormTemplateById(formTemplateId);
            return response;
        }

        [HttpDelete("Translation")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteFormTemplateTranslation([FromQuery] Guid formTemplateId, [FromQuery] Language language)
        {
            var response = await _formTemplateTranslationsDeleterService.DeleteFormTemplateTranslationById(formTemplateId, language);
            return response;
        }

        [HttpPost]
        [Route("Translation")]
        public async Task<IActionResult> AddFormTemplateTranslation([FromQuery] Guid formTemplateId, [FromBody] FormTemplateTranslationDTOAddRequest formTemplateTranslationDTOAddRequest)
        {
            formTemplateTranslationDTOAddRequest.FormTemplateId = formTemplateId;
            var translation = await _formTemplateTranslationsAdderService.AddFormTemplateTranslation(formTemplateTranslationDTOAddRequest);

            return CreatedAtAction(nameof(AddFormTemplateTranslation),translation);
        }
    }
}
