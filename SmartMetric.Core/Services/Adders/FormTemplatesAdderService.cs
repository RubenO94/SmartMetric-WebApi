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
        private readonly ILogger<FormTemplatesAdderService> _logger;
        public FormTemplatesAdderService(IFormTemplatesRepository formTemplateRepository, ILogger<FormTemplatesAdderService> logger)
        {
            _formTemplateRepository = formTemplateRepository;
            _logger = logger;
            //_formTemplateTranslationsAdderService = formTemplateTranslationsAdderService;
            //_questionAdderService = questionAdderService;
            //_singleChoiceOptionsAdderService = singleChoiceOptionsAdderService;
            //_ratingOptionAdderService = ratingOptionAdderService;
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

            await _formTemplateRepository.AddFormTemplate(formTemplate);

            return formTemplate.ToFormTemplateDTOResponse();
        }

        //private async Task<FormTemplateQuestion> AddQuestionWithAssociations(QuestionDTOAddRequest questionRequest)
        //{
        //    var addedQuestion = await _questionAdderService.AddQuestion(questionRequest);

        //    // Adiciona as SINGLE CHOICE OPTIONS, se houver
        //    if (questionRequest.SingleChoiceOptions != null && questionRequest.SingleChoiceOptions.Any())
        //    {
        //        foreach (var singleChoiceOptionRequest in questionRequest.SingleChoiceOptions)
        //        {
        //            singleChoiceOptionRequest.QuestionId = addedQuestion.QuestionId;
        //            await _singleChoiceOptionsAdderService.AddSingleChoiceOption(singleChoiceOptionRequest);
        //        }
        //    }

        //    // Adiciona as RATING OPTIONS, se houver
        //    if (questionRequest.RatingOptions != null && questionRequest.RatingOptions.Any())
        //    {
        //        foreach (var ratingOptionRequest in questionRequest.RatingOptions)
        //        {
        //            ratingOptionRequest.QuestionId = addedQuestion.QuestionId;
        //            await _ratingOptionAdderService.AddRatingOption(ratingOptionRequest);
        //        }
        //    }

        //    return addedQuestion.ToFormTemplateQuestion();
        //}

    }
}
