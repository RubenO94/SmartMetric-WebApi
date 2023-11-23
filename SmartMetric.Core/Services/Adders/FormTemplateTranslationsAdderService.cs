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
    public class FormTemplateTranslationsAdderService : IFormTemplateTranslationsAdderService
    {
        private readonly IFormTemplateTranslationsRepository _translationsRepository;
        private readonly IFormTemplatesRepository _formTemplatesRepository;
        private readonly ILogger<FormTemplateTranslationsAdderService> _logger;

        public FormTemplateTranslationsAdderService(IFormTemplateTranslationsRepository translationsRepository, IFormTemplatesRepository formTemplatesRepository, ILogger<FormTemplateTranslationsAdderService> logger)
        {
            _translationsRepository = translationsRepository;
            _formTemplatesRepository = formTemplatesRepository;
            _logger = logger;
        }

        public async Task<ApiResponse<FormTemplateTranslationDTOResponse?>> AddFormTemplateTranslation(FormTemplateTranslationDTOAddRequest? request)
        {
            _logger.LogInformation($"{nameof(FormTemplateTranslationsAdderService)}.{nameof(AddFormTemplateTranslation)} foi iniciado");

            if (request == null) throw new HttpStatusException(HttpStatusCode.BadRequest, "Request can't be null");

            if (request.Language == null) throw new HttpStatusException(HttpStatusCode.BadRequest, "The FormTemplateTranslation must have a 'language' field.");

            if (request.Title == null || request.Title == "") throw new HttpStatusException(HttpStatusCode.BadRequest, "The FormTemplateTranslation must have a 'title' field.");

            if (request.FormTemplateId == null) throw new HttpStatusException(HttpStatusCode.BadRequest, "The 'formTemplateId' parameter is required and must be a valid GUID.");

            var existenceFormTemplate = await _formTemplatesRepository.GetFormTemplateById(request.FormTemplateId);

            if (existenceFormTemplate == null) throw new HttpStatusException(HttpStatusCode.BadRequest, "Resource not found. The provided ID does not exist.");

            var existenceTranslations = await _translationsRepository.GetTranslationsByFormTemplateId(request.FormTemplateId.Value);

            if (existenceTranslations.Any())
            {
                foreach (var item in existenceTranslations)
                {
                    if (item.Language == request.Language.ToString())
                    {
                        throw new HttpStatusException(HttpStatusCode.BadRequest, "This language already exists in the provided FormTemplate.");
                    }
                }
            }

            FormTemplateTranslation translation = request.ToFormTemplateTranslation();

            translation.FormTemplateTranslationId = Guid.NewGuid();

            await _translationsRepository.AddFormTemplateTranslation(translation);

            return new ApiResponse<FormTemplateTranslationDTOResponse?>()
            {
                StatusCode = (int)HttpStatusCode.Created,
                Message = "Translation added successfully to the FormTemplate.",
                Data = translation.ToFormTemplateTranslationDTOResponse(),
            };
        }
    }
}
