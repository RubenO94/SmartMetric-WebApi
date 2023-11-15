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
        private readonly IFormTemplateRepository _formTemplateRepository;
        private readonly ILogger<FormTemplatesAdderService> _logger;
        public FormTemplatesAdderService(IFormTemplateRepository formTemplateRepository, ILogger<FormTemplatesAdderService> logger)
        {
            _formTemplateRepository = formTemplateRepository;
            _logger = logger;
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

    }
}
