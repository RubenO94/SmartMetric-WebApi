using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Core.DTO.AddRequest;
using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.Helpers;
using SmartMetric.Core.ServicesContracts.Adders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.Services.Adders
{
    public class FormTemplatesAdderService : IFormTemplatesAdderService
    {
        private readonly IFormTemplatesRepository _formTemplateRepository;
        private readonly IFormTemplateTranslationsAdderService _formTemplateTranslationsAdderService;
        private readonly ILogger<FormTemplatesAdderService> _logger;
        public FormTemplatesAdderService(IFormTemplatesRepository formTemplateRepository, ILogger<FormTemplatesAdderService> logger, IFormTemplateTranslationsAdderService formTemplateTranslationsAdderService)
        {
            _formTemplateRepository = formTemplateRepository;
            _logger = logger;
            _formTemplateTranslationsAdderService = formTemplateTranslationsAdderService;
        }

        public async Task<FormTemplateDTOResponse?> AddFormTemplate(FormTemplateDTOAddRequest? addFormTemplateRequest)
        {
            _logger.LogInformation($"{nameof(FormTemplatesAdderService)}.{nameof(AddFormTemplate)} foi iniciado");


            if (addFormTemplateRequest == null)
            {
                throw new ArgumentNullException(nameof(addFormTemplateRequest));
            }

            ValidationHelper.ModelValidation(addFormTemplateRequest);


            FormTemplate formTemplate = addFormTemplateRequest.ToFormTemplate();

            formTemplate.FormTemplateId = Guid.NewGuid();

           
            //TRANSLATIONS
            if(formTemplate.Translations != null && addFormTemplateRequest?.Translations?.Count() > 0)
            {
                foreach (var translationRequest in addFormTemplateRequest.Translations)
                {
                    translationRequest.FormTemplateId = formTemplate.FormTemplateId;
                    var translationResponse = await  _formTemplateTranslationsAdderService.AddFormTemplateTranslation(translationRequest);
                }
            }

            //QUESTIONS
            formTemplate.FormTemplateQuestions = new List<FormTemplateQuestion>();

            if (addFormTemplateRequest?.Questions != null && addFormTemplateRequest.Questions.Count() > 0)
            {
                foreach (var question in addFormTemplateRequest.Questions)
                {
                    formTemplate.FormTemplateQuestions.Add(new FormTemplateQuestion()
                    {
                        FormTemplateId = formTemplate.FormTemplateId,
                        //QuestionId = question.QuestionId
                    });
                    //TODO: Add Questions
                }
            }
            else
            {
                throw new ArgumentException(nameof(addFormTemplateRequest.Questions));
            }


            await _formTemplateRepository.AddFormTemplate(formTemplate);

            return formTemplate.ToFormTemplateDTOResponse();

            throw new NotImplementedException();
        }

    }
}
