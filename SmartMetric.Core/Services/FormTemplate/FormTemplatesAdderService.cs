using Microsoft.Extensions.Logging;
using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Core.DTO.AddRequest;
using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.Helpers;
using SmartMetric.Core.ServicesContracts.FormTemplates;
using System.Net;

namespace SmartMetric.Core.Services.FormTemplates
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

        public async Task<ApiResponse<FormTemplateDTOResponse?>> AddFormTemplate(FormTemplateDTOAddRequest? addFormTemplateRequest)
        {
            _logger.LogInformation($"{nameof(FormTemplatesAdderService)}.{nameof(AddFormTemplate)} foi iniciado");

            if (addFormTemplateRequest == null) throw new ArgumentNullException(nameof(FormTemplate));

            ValidationHelper.ModelValidation(addFormTemplateRequest);

            FormTemplate formTemplate = addFormTemplateRequest.ToFormTemplate();

            await _formTemplateRepository.AddFormTemplate(formTemplate);

            return new ApiResponse<FormTemplateDTOResponse?>()
            {
                StatusCode = (int)HttpStatusCode.Created,
                Message = "FormTemplate created successfully!",
                Data = formTemplate.ToFormTemplateDTOResponse()
            };
        }

    }
}
