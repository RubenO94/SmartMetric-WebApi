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
            if (formTemplate == null)
            {
                return NotFound(new
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = $"FormTemplate with ID {formTemplateId} not found."
                });
            }

            return Ok(formTemplate);
        }

        [HttpPost]
        public async Task<IActionResult> AddFormTemplate([FromBody] FormTemplateDTOAddRequest formTemplateDTOAddRequest)
        {
            var formTemplate = await _formTemplateAdderService.AddFormTemplate(formTemplateDTOAddRequest);

            return CreatedAtAction(nameof(AddFormTemplate), new
            {
                StatusCode = (int)HttpStatusCode.Created,
                Message = "Success! FormTemplate Created",
                formTemplate,
            });
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFormTemplateById(Guid formTemplateId)
        {

            var formTemplate = _formTemplateGetterService.GetFormTemplateById(formTemplateId);

            if (formTemplate != null)
            {
                var hasDeleted = await _formTemplatesDeleterService.DeleteFormTemplateById(formTemplateId);
                if (hasDeleted)
                {
                    return NoContent();
                }
            }

            return NotFound(new
            {
                StatusCode = (int)HttpStatusCode.NotFound,
                Message = $"FormTemplate with ID {formTemplateId} not found."
            });
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
                    var translation = await _formTemplateTranslationsAdderService.AddFormTemplateTranslation(formTemplateTranslationDTOAddRequest);

                    return CreatedAtAction(nameof(AddFormTemplateTranslation), new
                    {
                        StatusCode = (int)HttpStatusCode.Created,
                        Message = "Success! FormTemplate Translation Created",
                        translation,
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

        [HttpDelete("Translation")]
        public async Task<IActionResult> DeleteFormTemplateTranslation([FromQuery] Guid formTemplateId, [FromQuery] Language language)
        {
            var formTemplate = await _formTemplateGetterService.GetFormTemplateById(formTemplateId);

            if (formTemplate == null)
            {
                return NotFound(new
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = $"FormTemplateTranslation with ID {formTemplateId} not found."
                });
            }

            if (formTemplate.Translations == null || formTemplate.Translations.Count() < 2)
            {
                return BadRequest(new
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = $"FormTemplate Translations must have at least one translation."
                });
            }

            var translationToBeDeleted = formTemplate.Translations.FirstOrDefault(temp => temp.Language == language.ToString());

            if (translationToBeDeleted == null)
            {
                return BadRequest(new
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = $"FormTemplate Translations does not have a {language} translation."
                });
            }

            var hasDeleted = await _formTemplateTranslationsDeleterService.DeleteFormTemplateTranslationById(translationToBeDeleted.FormTemplateTranslationId);

            return hasDeleted ? NoContent() : StatusCode((int)HttpStatusCode.InternalServerError);
        }


    }
}
