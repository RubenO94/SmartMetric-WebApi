using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.DTO;
using SmartMetric.Core.DTO.AddRequest;
using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.Enums;
using SmartMetric.Core.Services.Adders;
using SmartMetric.Core.ServicesContracts.Adders;
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
        private readonly IFormTemplateTranslationsAdderService _formTemplateTranslationsAdderService;

        public FormTemplatesController(IFormTemplatesGetterService formTemplateGetterService, IFormTemplatesAdderService formTemplatesAdderService, IFormTemplateTranslationsAdderService formTemplateTranslationsAdderService)
        {
            _formTemplateGetterService = formTemplateGetterService;
            _formTemplateAdderService = formTemplatesAdderService;
            _formTemplateTranslationsAdderService = formTemplateTranslationsAdderService;
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

        [HttpGet("{id}")]
        public async Task<ActionResult<FormTemplateDTOResponse>> GetFormTemplateById(Guid id)
        {
            var formTemplate = await _formTemplateGetterService.GetFormTemplateById(id);
            if (formTemplate == null)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    errorMessage = "FormTemplateId can't be null"
                });
            }

            return Ok(formTemplate);
        }

        [HttpPost]
        public async Task<IActionResult> AddFormTemplate([FromBody] FormTemplateDTOAddRequest formTemplateDTOAddRequest)
        {
            var createdFormTemplate = await _formTemplateAdderService.AddFormTemplate(formTemplateDTOAddRequest);

            // Adicionar traduções
            if (formTemplateDTOAddRequest.Translations != null && formTemplateDTOAddRequest.Translations.Any() && createdFormTemplate != null)
            {
                foreach (var translationDTO in formTemplateDTOAddRequest.Translations)
                {
                    translationDTO.FormTemplateId = createdFormTemplate.FormTemplateId;
                    await _formTemplateTranslationsAdderService.AddFormTemplateTranslation(translationDTO);
                }
            }

            return CreatedAtAction(nameof(GetFormTemplateById), new
            {
                StatusCode = HttpStatusCode.Created,
                Message = "FormTemplate Created",
                FormTemplateId = createdFormTemplate?.FormTemplateId.ToString(),
            });
        }
    }
}
