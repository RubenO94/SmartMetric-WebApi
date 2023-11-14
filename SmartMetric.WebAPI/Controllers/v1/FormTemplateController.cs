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

//IFormTemplateTranslationsAdderService formTemplateTranslationsAdderService, IQuestionAdderService questionAdderService, IQuestionTranslationAdderService questionTranslationAdderService, IRatingOptionAdderService ratingOptionAdderService, IRatingOptionTranslationAdderService ratingOptionTranslationAdderService, ISingleChoiceOptionsAdderService siSingleChoiceOptionsAdderService, ISingleChoiceOptionTranslationsAdderService singleChoiceOptionTranslationAdderService

namespace SmartMetric.WebAPI.Controllers.v1
{
    [ApiVersion("1.0")]
    public class FormTemplateController : CustomBaseController
    {
        private readonly IFormTemplatesGetterService _formTemplateGetterService;
        private readonly IFormTemplatesAdderService _formTemplatesAdderService;
        //private readonly IFormTemplateTranslationsAdderService _formTemplateTranslationsAdderService;
        //private readonly IQuestionAdderService _questionAdderService;
        //private readonly IQuestionTranslationAdderService _questionTranslationAdderService;
        //private readonly IRatingOptionAdderService _ratingOptionAdderService;
        //private readonly IRatingOptionTranslationAdderService _ratingOptionTranslationAdderService;
        //private readonly ISingleChoiceOptionsAdderService _siSingleChoiceOptionsAdderService;
        //private readonly ISingleChoiceOptionTranslationsAdderService _singleChoiceOptionTranslationAdderService;

        public FormTemplateController(IFormTemplatesGetterService formTemplateGetterService, IFormTemplatesAdderService formTemplatesAdderService)
        {
            _formTemplateGetterService = formTemplateGetterService;
            _formTemplatesAdderService = formTemplatesAdderService;
            //_formTemplateTranslationsAdderService = formTemplateTranslationsAdderService;
            //_questionAdderService = questionAdderService;
            //_questionTranslationAdderService = questionTranslationAdderService;
            //_ratingOptionAdderService = ratingOptionAdderService;
            //_ratingOptionTranslationAdderService = ratingOptionTranslationAdderService;
            //_siSingleChoiceOptionsAdderService = siSingleChoiceOptionsAdderService;
            //_singleChoiceOptionTranslationAdderService = singleChoiceOptionTranslationAdderService;
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
            var createdFormTemplate =  await _formTemplatesAdderService.AddFormTemplate(formTemplateDTOAddRequest);

            if(createdFormTemplate == null)
            {
                return BadRequest(new {message = "Something went worng!"});
            }
            return CreatedAtAction(nameof(GetFormTemplateById), new { id = createdFormTemplate.FormTemplateId, message = "FormTemplate created!" });
        }
    }
}
