using Microsoft.Extensions.Logging;
using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Core.DTO.AddRequest;
using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.Exceptions;
using SmartMetric.Core.ServicesContracts.FormTemplateTranslations;
using System.Net;

namespace SmartMetric.Core.Services.FormTemplateTranslations
{
    public class FormTemplateTranslationsAdderService : IFormTemplateTranslationsAdderService
    {
        private readonly IFormTemplateTranslationRepository _translationsRepository;
        private readonly IFormTemplateRepository _formTemplatesRepository;
        private readonly ILogger<FormTemplateTranslationsAdderService> _logger;

        public FormTemplateTranslationsAdderService(IFormTemplateTranslationRepository translationsRepository, IFormTemplateRepository formTemplatesRepository, ILogger<FormTemplateTranslationsAdderService> logger)
        {
            _translationsRepository = translationsRepository;
            _formTemplatesRepository = formTemplatesRepository;
            _logger = logger;
        }

        public async Task<ApiResponse<TranslationDTOResponse?>> AddFormTemplateTranslation(Guid? formTemplateId, TranslationDTOAddRequest? request)
        {
            _logger.LogInformation($"{nameof(FormTemplateTranslationsAdderService)}.{nameof(AddFormTemplateTranslation)} foi iniciado");

            if(formTemplateId == null) throw new ArgumentNullException(nameof(formTemplateId));

            if (request == null) throw new HttpStatusException(HttpStatusCode.BadRequest, "Request can't be null");

            var existenceFormTemplate = await _formTemplatesRepository.GetFormTemplateById(formTemplateId.Value);

            if (existenceFormTemplate == null) throw new HttpStatusException(HttpStatusCode.BadRequest, "Resource not found. The provided ID does not exist.");

            var existenceTranslations = await _translationsRepository.GetTranslationsByFormTemplateId(formTemplateId.Value);

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
            translation.FormTemplateId = formTemplateId.Value;

            //translation.FormTemplateTranslationId = Guid.NewGuid();

            await _translationsRepository.AddFormTemplateTranslation(translation);

            return new ApiResponse<TranslationDTOResponse?>()
            {
                StatusCode = (int)HttpStatusCode.Created,
                Message = "Translation added successfully to the FormTemplate.",
                Data = translation.ToTranslationDTOResponse(),
            };
        }
    }
}
