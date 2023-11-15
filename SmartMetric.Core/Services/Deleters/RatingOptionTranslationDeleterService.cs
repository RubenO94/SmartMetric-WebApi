using Microsoft.Extensions.Logging;
using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Core.ServicesContracts.Deleters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.Services.Deleters
{
    public class RatingOptionTranslationDeleterService : IRatingOptionTranslationDeleterService
    {
        //variables
        private readonly IRatingOptionTranslationsRepository _ratingOptionTranslationsRepository;
        private readonly ILogger<RatingOptionTranslationDeleterService> _logger;

        //constructor
        public RatingOptionTranslationDeleterService (IRatingOptionTranslationsRepository ratingOptionTranslationsRepository, ILogger<RatingOptionTranslationDeleterService> logger)
        {
            _ratingOptionTranslationsRepository = ratingOptionTranslationsRepository;
            _logger = logger;
        }

        //deleters
        public async Task<bool> DeleteRatingOptionTranslationById(Guid? ratingOptionTranslationId)
        {
            _logger.LogInformation($"{nameof(RatingOptionTranslationDeleterService)}.{nameof(DeleteRatingOptionTranslationById)} foi iniciado");

            if (ratingOptionTranslationId == null) { return false; }

            return await _ratingOptionTranslationsRepository.DeleteRatingOptionTranslationById(ratingOptionTranslationId.Value);
        }
    }
}
