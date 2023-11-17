using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Core.DTO.AddRequest;
using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.Exceptions;
using SmartMetric.Core.Helpers;
using SmartMetric.Core.ServicesContracts.Adders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        }

        public async Task<ApiResponse<FormTemplateDTOResponse?>> AddFormTemplate(FormTemplateDTOAddRequest? addFormTemplateRequest)
        {
            _logger.LogInformation($"{nameof(FormTemplatesAdderService)}.{nameof(AddFormTemplate)} foi iniciado");

            if (addFormTemplateRequest == null)
            {
                throw new HttpStatusException(HttpStatusCode.BadRequest, "Request can't be null");
            }

            ValidationHelper.ModelValidation(addFormTemplateRequest);

            var formTemplateId = Guid.NewGuid();

            foreach (var translation in addFormTemplateRequest.Translations!)
            {
                translation.FormTemplateId = formTemplateId;
            }

            FormTemplate formTemplate = addFormTemplateRequest.ToFormTemplate();
            formTemplate.FormTemplateId = formTemplateId;

            //foreach (var translation in formTemplate.Translations!)
            //{
            //    translation.FormTemplateTranslationId = Guid.NewGuid();
            //}

            await _formTemplateRepository.AddFormTemplate(formTemplate);

            return new ApiResponse<FormTemplateDTOResponse?>()
            {
                StatusCode = (int)System.Net.HttpStatusCode.Created,
                Message = "FormTemplate create with success!",
                Data = formTemplate.ToFormTemplateDTOResponse()
            };
        }

    }
}
