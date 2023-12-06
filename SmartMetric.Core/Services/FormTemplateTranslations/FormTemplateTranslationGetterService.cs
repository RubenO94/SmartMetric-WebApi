using Microsoft.Extensions.Logging;
using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.Exceptions;
using SmartMetric.Core.ServicesContracts.FormTemplateTranslations;
using System.Net;

namespace SmartMetric.Core.Services.FormTemplateTranslations
{
    public class FormTemplateTranslationsGetterService : IFormTemplateTranslationsGetterService
    {
        private readonly IFormTemplateTranslationRepository _translationsRepository;
        private readonly ILogger<FormTemplateTranslationsGetterService> _logger;

        public FormTemplateTranslationsGetterService(IFormTemplateTranslationRepository translationsRepository, ILogger<FormTemplateTranslationsGetterService> logger)
        {
            _translationsRepository = translationsRepository;
            _logger = logger;
        }

        #region FormTemplateTranslation Getters
        public async Task<ApiResponse<List<TranslationDTOResponse>>> GetAllFormTemplateTranslations()
        {
            _logger.LogInformation($"{nameof(FormTemplateTranslationsGetterService)}.{nameof(GetAllFormTemplateTranslations)} foi iniciado");
            var translations = await _translationsRepository.GetAllFormTemplateTranslations();

            return new ApiResponse<List<TranslationDTOResponse>>()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Data retrieved successfully.",
                Data = translations.Select(temp => temp.ToTranslationDTOResponse()).ToList()
            };
        }

        public async Task<ApiResponse<List<TranslationDTOResponse>?>> GetTranslationsByFormTemplateId(Guid? formTemplateId)
        {
            _logger.LogInformation($"{nameof(FormTemplateTranslationsGetterService)}.{nameof(GetTranslationsByFormTemplateId)} foi iniciado");

            if (formTemplateId == null)
            {
                throw new HttpStatusException(HttpStatusCode.BadRequest, "Request can't be null.");
            }

            var translations = await _translationsRepository.GetTranslationsByFormTemplateId(formTemplateId.Value);

            if (translations == null) throw new HttpStatusException(HttpStatusCode.BadRequest, "Resource not found. The provided ID does not exist.");

            return new ApiResponse<List<TranslationDTOResponse>?>()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Data retrieved successfully.",
                Data = translations.Select(temp => temp.ToTranslationDTOResponse()).ToList()
            };

        }

        public async Task<ApiResponse<TranslationDTOResponse?>> GetFormTemplateTranslationById(Guid? formTemplateTranslationId)
        {
            _logger.LogInformation($"{nameof(FormTemplateTranslationsGetterService)}.{nameof(GetFormTemplateTranslationById)} foi iniciado");

            if (formTemplateTranslationId == null)
            {
                throw new HttpStatusException(HttpStatusCode.BadRequest, "Request can't be null.");
            }

            FormTemplateTranslation? translation = await _translationsRepository.GetFormTemplateTranslationById(formTemplateTranslationId.Value);

            if (translation == null)
            {
                throw new HttpStatusException(HttpStatusCode.NotFound, "Resource not found. The provided ID does not exist.");
            }

            return new ApiResponse<TranslationDTOResponse?>()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Data retrieved successfully.",
                Data = translation.ToTranslationDTOResponse()
            };
        }
        #endregion
    }
}
