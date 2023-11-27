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

            //if (addFormTemplateRequest.CreatedByUserId == null || addFormTemplateRequest.CreatedByUserId == 0) throw new HttpStatusException(HttpStatusCode.BadRequest, "The formTemplate needs a User to be created.");

            //if (addFormTemplateRequest.Translations == null || addFormTemplateRequest.Translations.Count < 1) throw new HttpStatusException(HttpStatusCode.BadRequest, "The formTemplate needs at least one translation.");

            //foreach (var translation in addFormTemplateRequest.Translations)
            //{
            //    if (translation.Language == null || translation.Title == null || translation.Title == "") throw new HttpStatusException(HttpStatusCode.BadRequest, "The Translations field are missing values.");
            //}

            addFormTemplateRequest.CreatedDate = DateTime.Now;
            var formTemplateId = Guid.NewGuid();

            foreach (var translation in addFormTemplateRequest.Translations!)
            {
                translation.FormTemplateId = formTemplateId;
            }

            FormTemplate formTemplate = addFormTemplateRequest.ToFormTemplate();
            formTemplate.FormTemplateId = formTemplateId;

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
