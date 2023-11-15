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

        public FormTemplatesController(IFormTemplatesGetterService formTemplateGetterService, IFormTemplatesAdderService formTemplatesAdderService, IFormTemplatesDeleterService formTemplatesDeleterService, IFormTemplateTranslationsAdderService formTemplateTranslationsAdderService)
        {
            _formTemplateGetterService = formTemplateGetterService;
            _formTemplateAdderService = formTemplatesAdderService;
            _formTemplatesDeleterService = formTemplatesDeleterService;

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
                    statusCode = (int)HttpStatusCode.NotFound,
                    errorMessage = "FormTemplate not found."
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

            return CreatedAtAction(nameof(AddFormTemplate), new
            {
                StatusCode = (int)HttpStatusCode.Created,
                Message = "FormTemplate Created",
                FormTemplateId = createdFormTemplate?.FormTemplateId.ToString(),
            });
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFormTemplateById(Guid formTemplateId)
        {

            var hasDeleted = await _formTemplatesDeleterService.DeleteFormTemplateById(formTemplateId);
            if (hasDeleted)
            {
                return NoContent();
            }

            return BadRequest();
            
        }

        [HttpPost]
        [Route("Translation")]
        public async Task<IActionResult> AddFormTemplateTranslation([FromQuery] Guid formTemplateId, [FromBody] FormTemplateTranslationDTOAddRequest formTemplateTranslationDTOAddRequest)
        {
            var formTemplateExist = await _formTemplateGetterService.GetFormTemplateById(formTemplateId);
            if (formTemplateExist != null)
            {
                try
                {
                    formTemplateTranslationDTOAddRequest.FormTemplateId = formTemplateId;
                    var response = await _formTemplateTranslationsAdderService.AddFormTemplateTranslation(formTemplateTranslationDTOAddRequest);

                    return CreatedAtAction(nameof(AddFormTemplateTranslation), new
                    {
                        StatusCode = (int)HttpStatusCode.Created,
                        Message = "FormTemplate Translation Created",
                        FormTemplateTranslationId = response.FormTemplateTranslationId.ToString(),
                    });
                }
                catch (Exception ex)
                {
                    return BadRequest(new
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Message = ex.Message.ToString(),
                    });
                }

            }

            return BadRequest(new
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Message = "FormTemplateId does not exist"
            });

        }
    }
}
