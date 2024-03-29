﻿using Microsoft.Extensions.Logging;
using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Core.DTO.AddRequest;
using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.Exceptions;
using SmartMetric.Core.ServicesContracts.RatingOptionTranslations;
using System.Net;

namespace SmartMetric.Core.Services.RatingOptionTranslations
{
    public class RatingOptionTranslationsAdderService : IRatingOptionTranslationAdderService
    {
        private readonly IRatingOptionTranslationsRepository _translationsRepository;
        private readonly IRatingOptionRepository _ratingOptionRepository;
        private readonly ILogger<RatingOptionTranslationsAdderService> _logger;

        public RatingOptionTranslationsAdderService(IRatingOptionTranslationsRepository translationsRepository, IRatingOptionRepository ratingOptionRepository, ILogger<RatingOptionTranslationsAdderService> logger)
        {
            _translationsRepository = translationsRepository;
            _ratingOptionRepository = ratingOptionRepository;
            _logger = logger;
        }

        public async Task<ApiResponse<TranslationDTOResponse?>> AddRatingOptionTranslation(Guid? ratingOptionId, TranslationDTOAddRequest? request)
        {
            _logger.LogInformation($"{nameof(RatingOptionTranslationsAdderService)}.{nameof(AddRatingOptionTranslation)} foi iniciado");

            if(ratingOptionId == null) throw new ArgumentNullException(nameof(ratingOptionId));
            
            if (request == null) throw new ArgumentNullException(nameof(RatingOptionTranslation));

            var ratingOptionExist = await _ratingOptionRepository.GetRatingOptionById(ratingOptionId.Value);

            if (ratingOptionExist == null) throw new HttpStatusException(HttpStatusCode.BadRequest, "Resource not found. The provided ID does not exist.");

            var existingTranslations = await _translationsRepository.GetRatingOptionTranslationByRatingOptionId(ratingOptionId!.Value);

            if (existingTranslations.Any())
            {
                foreach (var item in existingTranslations)
                {
                    if (item.Language == request.Language.ToString())
                    {
                        throw new HttpStatusException(HttpStatusCode.BadRequest, "This language already exists in the provided FormTemplate.");
                    }
                }
            }

            RatingOptionTranslation translation = request.ToRatingOptionTranslation();

            translation.RatingOptionId = ratingOptionId;

            await _translationsRepository.AddRatingOptionTranslation(translation);

            return new ApiResponse<TranslationDTOResponse?>()
            {
                StatusCode = (int)HttpStatusCode.Created,
                Message = "Translation added successfully to the RatingOption.",
                Data = translation.ToTranslationDTOResponse()
            };
        }
    }
}
