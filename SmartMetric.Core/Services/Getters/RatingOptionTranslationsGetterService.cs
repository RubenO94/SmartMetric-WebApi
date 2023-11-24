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
    public class RatingOptionTranslationsGetterService : IRatingOptionTranslationGetterService
    {
        private readonly IRatingOptionTranslationsRepository _translationsRepository;
        private readonly ILogger<RatingOptionTranslationsGetterService> _logger;

        public RatingOptionTranslationsGetterService(IRatingOptionTranslationsRepository translationsRepository, ILogger<RatingOptionTranslationsGetterService> logger)
        {
            _translationsRepository = translationsRepository;
            _logger = logger;
        }

        #region RatingOptionTranslation Getters

        public async Task<ApiResponse<List<RatingOptionTranslationDTOResponse>>> GetAllRatingOptionTranslations()
        {
            _logger.LogInformation($"{nameof(RatingOptionTranslationsGetterService)}.{nameof(GetAllRatingOptionTranslations)} foi iniciado");
            var translations = await _translationsRepository.GetAllRatingOptionTranslations();

            return new ApiResponse<List<RatingOptionTranslationDTOResponse>>()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Data retrieved successfully.",
                Data = translations.Select(temp => temp.ToRatingOptionTranslationDTOResponse()).ToList()
            };
        }

        public async Task<ApiResponse<RatingOptionTranslationDTOResponse?>> GetRatingOptionTranslationById(Guid? ratingOptionTranslationId)
        {
            _logger.LogInformation($"{nameof(RatingOptionTranslationsGetterService)}.{nameof(GetRatingOptionTranslationById)} foi iniciado");

            if (ratingOptionTranslationId == null)
            {
                throw new HttpStatusException(HttpStatusCode.BadRequest, "The 'ratingOptionTranslationId' parameter is required and must be a valid GUID.");
            }

            RatingOptionTranslation? translation = await _translationsRepository.GetRatingOptionTranslationById(ratingOptionTranslationId.Value);

            if (translation == null)
            {
                throw new HttpStatusException(HttpStatusCode.NotFound, "Resource not found. The provided ID does not exist.");
            }

            return new ApiResponse<RatingOptionTranslationDTOResponse?>()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Data retrieved successfully.",
                Data = translation.ToRatingOptionTranslationDTOResponse()
            };
        }

        public async Task<ApiResponse<List<RatingOptionTranslationDTOResponse>?>> GetRatingOptionTranslationsByRatingOptionId(Guid? ratingOptionId)
        {
            _logger.LogInformation($"{nameof(RatingOptionTranslationsGetterService)}.{nameof(GetRatingOptionTranslationsByRatingOptionId)} foi iniciado");

            if (ratingOptionId == null) throw new HttpStatusException(HttpStatusCode.BadRequest, "The 'ratingOptionId' parameter is required and must be a valid GUID.");

            var translations = await _translationsRepository.GetRatingOptionTranslationByRatingOptionId(ratingOptionId.Value);

            if (translations == null) throw new HttpStatusException(HttpStatusCode.BadRequest, "Resource not found. The provided ID does not exist.");

            return new ApiResponse<List<RatingOptionTranslationDTOResponse>?>()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Data retrieved successfully.",
                Data = translations.Select(temp => temp.ToRatingOptionTranslationDTOResponse()).ToList()
            };
        }

        #endregion
    }
}
