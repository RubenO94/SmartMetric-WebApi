using Microsoft.Extensions.Logging;
using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.Exceptions;
using SmartMetric.Core.ServicesContracts.QuestionTranslations;
using System.Net;

namespace SmartMetric.Core.Services.QuestionTranslations
{
    public class QuestionTranslationGetterService : IQuestionTranslationGetterService
    {
        private readonly IQuestionTranslationRepository _translationsRepository;
        private readonly ILogger<QuestionTranslationGetterService> _logger;

        public QuestionTranslationGetterService(IQuestionTranslationRepository translationsRepository, ILogger<QuestionTranslationGetterService> logger)
        {
            _translationsRepository = translationsRepository;
            _logger = logger;
        }

        #region Getters

        public async Task<ApiResponse<List<TranslationDTOResponse>>> GetAllQuestionTranslations()
        {
            _logger.LogInformation($"{nameof(QuestionTranslationGetterService)}.{nameof(GetAllQuestionTranslations)} foi iniciado");

            var translations = await _translationsRepository.GetAllQuestionTranslations();

            return new ApiResponse<List<TranslationDTOResponse>>()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Data retrieved successfully.",
                Data = translations.Select(temp => temp.ToTranslationDTOResponse()).ToList()
            };
        }

        public async Task<ApiResponse<TranslationDTOResponse?>> GetQuestionTranslationById(Guid? questionTranslationId)
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

            return new ApiResponse<TranslationDTOResponse?>()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Data retrieved successfully.",
                Data = translation.ToTranslationDTOResponse()
            };

        }

        public async Task<ApiResponse<List<TranslationDTOResponse>?>> GetQuestionTranslationsByQuestionId(Guid? questionId)
        {
            _logger.LogInformation($"{nameof(QuestionTranslationGetterService)}.{nameof(GetQuestionTranslationsByQuestionId)} foi iniciado");

            if (questionId == null)
            {
                throw new HttpStatusException(HttpStatusCode.BadRequest, "The 'questionId' parameter is required and must be a valid GUID.");
            }

            var translations = await _translationsRepository.GetQuestionTranslationsByQuestionId(questionId.Value);

            return new ApiResponse<List<TranslationDTOResponse>?>()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Data retrieved successfully.",
                Data = translations.Select(temp => temp.ToTranslationDTOResponse()).ToList()
            };
        }

        #endregion
    }
}
