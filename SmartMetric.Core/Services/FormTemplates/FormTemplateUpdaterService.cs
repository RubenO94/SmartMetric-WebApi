using Microsoft.Extensions.Logging;
using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.DTO.UpdateRequest;
using SmartMetric.Core.Enums;
using SmartMetric.Core.Helpers;
using SmartMetric.Core.Services.FormTemplates;
using SmartMetric.Core.ServicesContracts.FormTemplates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.Services.FormTemplates
{
    public class FormTemplateUpdaterService : IFormTemplateUpdaterService
    {
        private readonly IFormTemplateRepository _formTemplateRepository;
        private readonly ILogger<FormTemplateUpdaterService> _logger;
        public FormTemplateUpdaterService(IFormTemplateRepository formTemplateRepository, ILogger<FormTemplateUpdaterService> logger)
        {
            _formTemplateRepository = formTemplateRepository;
            _logger = logger;
        }

        public async Task<ApiResponse<FormTemplateDTOResponse>> UpdateFormTemplate(Guid? formtemplateId, FormTemplateDTOUpdate? formTemplateUpdate)
        {
            _logger.LogInformation($"{nameof(FormTemplateUpdaterService)}.{nameof(UpdateFormTemplate)} foi iniciado");

            if (formTemplateUpdate == null) throw new ArgumentNullException(nameof(formTemplateUpdate));

            if (formtemplateId == null) throw new ArgumentNullException(nameof(formtemplateId));

            var matchingFormTemplate = await _formTemplateRepository.GetFormTemplateById(formtemplateId);

            if (matchingFormTemplate == null) throw new ArgumentException("The given formTemplateId does not exist", nameof(formtemplateId));

            matchingFormTemplate.ModifiedDate = DateTime.UtcNow;

            // FormTemplate Translations
            UpdateHelper.UpdateTranslations(matchingFormTemplate.Translations, formTemplateUpdate.Translations);

            // FormTemplate Questions
            UpdateHelper.UpdateQuestions(matchingFormTemplate.Questions, formTemplateUpdate.Questions);

            var result = await _formTemplateRepository.UpdateFormTemplate(matchingFormTemplate);

            return new ApiResponse<FormTemplateDTOResponse>()
            {
                StatusCode = 200,
                Message = "FormTemplate updated with success!",
                Data = result.ToFormTemplateDTOResponse(),
            };
        }

    }
}
