using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.DTO;
using SmartMetric.Core.Enums;
using SmartMetric.Core.ServicesContracts.Getters;
using SmartMetric.Infrastructure.DatabaseContext;

namespace SmartMetric.WebAPI.Controllers.v1
{
    [ApiVersion("1.0")]
    public class FormTemplateController : CustomBaseController
    {
        private readonly IFormTemplatesGetterService _formTemplateGetterService;

        public FormTemplateController(IFormTemplatesGetterService formTemplateGetterService)
        {
            _formTemplateGetterService = formTemplateGetterService;
        }


        /// <summary>
        /// Retorna todos os FormTemplates
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetAllFormTemplates()
        {
            var formTemplates = await _formTemplateGetterService.GetAllFormTemplates();

            //var response = formTemplates.Select(temp => new
            //{
            //    formTemplateId = temp?.FormTemplateId,
            //    createdDate = temp?.CreatedDate,
            //    modifiedDate = temp?.ModifiedDate,
            //    translations = temp?.Translations?.Select(translation => new
            //    {
            //        language = translation.Language,
            //        title = translation.Title,
            //        description = translation.Description,
            //    }),
            //    questions = temp?.Questions?.Select(question => new
            //    {
            //        questionId = question.QuestionId,
            //        responseType = question.ResponseType,
            //        translations = question.Translations?.Select(translation => new
            //        {
            //            language = translation.Language,
            //            title = translation?.Title,
            //            description = translation?.Description, 
            //        }),
            //        ratingOptions = question.RatingOptions?.Select(rating => new
            //        {
            //            numericValue = rating.NumericValue,
            //            translations =  rating.Translations?.Select(translation => new
            //            {
            //                language = translation.Language,
            //                description = translation.Description
            //            }),
            //        }) ?? null,
            //        singleChoiceOptions = question.SingleChoiceOptions?.Select(sco => new
            //        {
            //            translations = sco.Translations?.Select(translation => new
            //            {
            //                language = translation.Language,
            //                description = translation.Description
            //            }),
            //        }) ?? null,

            //    })
            //});

            return Ok(formTemplates);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetFormTemplateById(Guid id)
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


            //var response = new
            //{
            //    formTemplateId = formTemplate?.FormTemplateId,
            //    createdDate = formTemplate?.CreatedDate,
            //    modifiedDate = formTemplate?.ModifiedDate,
            //    translations = formTemplate?.Translations?.Select(translation => new
            //    {
            //        language = translation.Language,
            //        title = translation.Title,
            //        description = translation.Description,
            //    }),
            //    questions = formTemplate?.Questions?.Select(question => new
            //    {
            //        questionId = question.QuestionId,
            //        responseType = question.ResponseType,
            //        translations = question.Translations?.Select(translation => new
            //        {
            //            language = translation.Language,
            //            title = translation?.Title,
            //            description = translation?.Description,
            //        }),
            //        ratingOptions = question.RatingOptions?.Select(rating => new
            //        {
            //            numericValue = rating.NumericValue,
            //            translations = rating.Translations?.Select(translation => new
            //            {
            //                language = translation.Language,
            //                description = translation.Description
            //            }),
            //        }) ?? null,
            //        singleChoiceOptions = question.SingleChoiceOptions?.Select(sco => new
            //        {
            //            translations = sco.Translations?.Select(translation => new
            //            {
            //                language = translation.Language,
            //                description = translation.Description
            //            }),
            //        }) ?? null,

            //    })
            //};

            return Ok(formTemplate);
        }

        //[HttpPost]
        //public IActionResult AddFormTemplate([FromBody] FormTemplateDTOAddRequest formTemplateDTOAddRequest)
        //{
        //    var createdFormTemplate = _formTemplateService.AddFormTemplate(formTemplateDTOAddRequest);
        //    return CreatedAtAction(nameof(GetFormTemplateById), new { id = createdFormTemplate.FormTemplateId }, createdFormTemplate);
        //}
    }
}
