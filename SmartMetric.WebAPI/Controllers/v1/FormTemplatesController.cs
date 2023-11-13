using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.DTO;
using SmartMetric.Core.DTO.AddRequest;
using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.Enums;
using SmartMetric.Core.ServicesContracts.Adders;
using SmartMetric.Core.ServicesContracts.Getters;
using SmartMetric.Infrastructure.DatabaseContext;

namespace SmartMetric.WebAPI.Controllers.v1
{
    [ApiVersion("1.0")]
    public class FormTemplatesController : CustomBaseController
    {
        private readonly IFormTemplatesGetterService _formTemplateGetterService;
        private readonly IFormTemplatesAdderService _formTemplateAdderService;

        public FormTemplatesController(IFormTemplatesGetterService formTemplateGetterService, IFormTemplatesAdderService formTemplatesAdderService)
        {
            _formTemplateGetterService = formTemplateGetterService;
            _formTemplateAdderService = formTemplatesAdderService;
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
            var formTemplate =  await _formTemplateGetterService.GetFormTemplateById(id);
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
        public IActionResult AddFormTemplate([FromBody] FormTemplateDTOAddRequest formTemplateDTOAddRequest)
        {
            var createdFormTemplate = _formTemplateAdderService.AddFormTemplate(formTemplateDTOAddRequest);
            return CreatedAtAction(nameof(GetFormTemplateById), createdFormTemplate);
        }
    }
}
