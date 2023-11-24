using Microsoft.Extensions.Logging;
using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.Enums;
using SmartMetric.Core.Exceptions;
using SmartMetric.Core.Helpers;
using SmartMetric.Core.ServicesContracts.Getters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.Services.Getters
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
        public async Task<ApiResponse<List<FormTemplateTranslationDTOResponse>>> GetAllFormTemplateTranslations()
        {
            _logger.LogInformation($"{nameof(FormTemplateTranslationsGetterService)}.{nameof(GetAllFormTemplateTranslations)} foi iniciado");
            var translations = await _translationsRepository.GetAllFormTemplateTranslations();

            return new ApiResponse<List<FormTemplateTranslationDTOResponse>>()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Data retrieved successfully.",
                Data = translations.Select(temp => temp.ToFormTemplateTranslationDTOResponse()).ToList()
            };
        }

        public async Task<ApiResponse<List<FormTemplateTranslationDTOResponse>?>> GetTranslationsByFormTemplateId(Guid? formTemplateId)
        {
            _logger.LogInformation($"{nameof(FormTemplateTranslationsGetterService)}.{nameof(GetTranslationsByFormTemplateId)} foi iniciado");

            if (formTemplateId == null)
            {
                throw new HttpStatusException(HttpStatusCode.BadRequest, "Request can't be null.");
            }

            var translations = await _translationsRepository.GetTranslationsByFormTemplateId(formTemplateId.Value);

            if (translations == null) throw new HttpStatusException(HttpStatusCode.BadRequest, "Resource not found. The provided ID does not exist.");

            return new ApiResponse<List<FormTemplateTranslationDTOResponse>?>()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Data retrieved successfully.",
                Data = translations.Select(temp => temp.ToFormTemplateTranslationDTOResponse()).ToList()
            };

        }

        public async Task<ApiResponse<FormTemplateTranslationDTOResponse?>> GetFormTemplateTranslationById(Guid? formTemplateTranslationId)
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

            return new ApiResponse<FormTemplateTranslationDTOResponse?>()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Data retrieved successfully.",
                Data = translation.ToFormTemplateTranslationDTOResponse()
            };
        }
        #endregion
    }
}
