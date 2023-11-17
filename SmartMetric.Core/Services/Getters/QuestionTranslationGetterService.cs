using Microsoft.Extensions.Logging;
using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.Exceptions;
using SmartMetric.Core.ServicesContracts.Getters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.Services.Getters
{
    public class QuestionTranslationGetterService : IQuestionTranslationGetterService
    {
        private readonly IQuestionTranslationsRepository _translationsRepository;
        private readonly ILogger<QuestionTranslationGetterService> _logger;

        public QuestionTranslationGetterService(IQuestionTranslationsRepository translationsRepository, ILogger<QuestionTranslationGetterService> logger)
        {
            _translationsRepository = translationsRepository;
            _logger = logger;
        }

        #region Getters

        public async Task<ApiResponse<List<QuestionTranslationDTOResponse>>> GetAllQuestionTranslations()
        {
            _logger.LogInformation($"{nameof(QuestionTranslationGetterService)}.{nameof(GetAllQuestionTranslations)} foi iniciado");

            var translations = await _translationsRepository.GetAllQuestionTranslations();

            return new ApiResponse<List<QuestionTranslationDTOResponse>>()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Data retrieved successfully.",
                Data = translations.Select(temp => temp.ToQuestionTranslationDTOResponse()).ToList()
            };
        }

        public async Task<ApiResponse<QuestionTranslationDTOResponse?>> GetQuestionTranslationById(Guid? questionTranslationId)
        {
            _logger.LogInformation($"{nameof(QuestionTranslationGetterService)}.{nameof(GetQuestionTranslationById)} foi iniciado");

            if (questionTranslationId == null)
            {
                throw new HttpStatusException(HttpStatusCode.BadRequest, "The 'questionTranslationId' parameter is required and must be a valid GUID.");
            }

            QuestionTranslation? translation = await _translationsRepository.GetQuestionTranslationsById(questionTranslationId.Value);

            if (translation == null)
            {
                throw new HttpStatusException(HttpStatusCode.NotFound, "Resource not found. The provided ID does not exist.");
            }

            return new ApiResponse<QuestionTranslationDTOResponse?>()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Data retrieved successfully.",
                Data = translation.ToQuestionTranslationDTOResponse()
            };

        }

        public async Task<ApiResponse<List<QuestionTranslationDTOResponse>?>> GetQuestionTranslationsByQuestionId(Guid? questionId)
        {
            _logger.LogInformation($"{nameof(QuestionTranslationGetterService)}.{nameof(GetQuestionTranslationsByQuestionId)} foi iniciado");

            if (questionId == null)
            {
                throw new HttpStatusException(HttpStatusCode.BadRequest, "The 'questionId' parameter is required and must be a valid GUID.");
            }

            var translations = await _translationsRepository.GetQuestionTranslationsByQuestionId(questionId.Value);

            return new ApiResponse<List<QuestionTranslationDTOResponse>?>()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Data retrieved successfully.",
                Data = translations.Select(temp => temp.ToQuestionTranslationDTOResponse()).ToList()
            };
        }

        #endregion
    }
}
