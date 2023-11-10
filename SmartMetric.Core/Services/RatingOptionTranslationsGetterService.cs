using Microsoft.Extensions.Logging;
using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Core.DTO;
using SmartMetric.Core.ServicesContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.Services
{
    public class RatingOptionTranslationsGetterService : IRatingOptionTranslationGetterService
    {
        //variables
        private readonly IRatingOptionTranslationsRepository _translationsRepository;
        private readonly ILogger<RatingOptionTranslationsGetterService> _logger;

        //constructor
        public RatingOptionTranslationsGetterService(IRatingOptionTranslationsRepository translationsRepository, ILogger<RatingOptionTranslationsGetterService> logger)
        {
            _translationsRepository = translationsRepository;
            _logger = logger;
        }

        //implementing interface
        #region RatingOptionTranslation Getters

        public async Task<List<RatingOptionTranslationDTOResponse>> GetAllRatingOptionTranslations()
        {
            _logger.LogInformation($"{nameof(RatingOptionTranslationsGetterService)}.{nameof(GetAllRatingOptionTranslations)} foi iniciado");
            var translations = await _translationsRepository.GetAllRatingOptionTranslations();

            return translations.Select(temp => temp.ToRatingOptionTranslationDTOResponse()).ToList();
        }

        public async Task<RatingOptionTranslationDTOResponse?> GetRatingOptionTranslationById(Guid? ratingOptionTranslationId)
        {
            _logger.LogInformation($"{nameof(RatingOptionTranslationsGetterService)}.{nameof(GetRatingOptionTranslationById)} foi iniciado");

            if (ratingOptionTranslationId == null) { return null; }

            RatingOptionTranslation? translation = await _translationsRepository.GetRatingOptionTranslationById(ratingOptionTranslationId.Value);

            if (translation == null) { return null; }

            return translation.ToRatingOptionTranslationDTOResponse();
        }

        public async Task<List<RatingOptionTranslationDTOResponse>?> GetRatingOptiontranslationsByRatingOptionId(Guid? ratingOptionId)
        {
            _logger.LogInformation($"{nameof(RatingOptionTranslationsGetterService)}.{nameof(GetRatingOptiontranslationsByRatingOptionId)} foi iniciado");

            if (ratingOptionId == null) { return null; }

            var translation = await _translationsRepository.GetRatingOptionTranslationByRatingOptionId(ratingOptionId.Value);

            if (translation.Count <= 0) { return null; }

            return translation.Select(temp => temp.ToRatingOptionTranslationDTOResponse()).ToList();
        }

        #endregion
    }
}
