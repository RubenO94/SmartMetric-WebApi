using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Infrastructure.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Infrastructure.Repositories
{
    public class RatingOptionTranslationsRepository : IRatingOptionTranslationsRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<RatingOptionTranslationsRepository> _logger;

        RatingOptionTranslationsRepository(ApplicationDbContext dbContext, ILogger<RatingOptionTranslationsRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        #region Adders

        public async Task<RatingOptionTranslation> AddRatingOptionTranslation(RatingOptionTranslation ratingOptionTranslation)
        {
            _logger.LogInformation($"{nameof(RatingOptionTranslationsRepository)}.{nameof(AddRatingOptionTranslation)} foi iniciado.");

            _dbContext.RatingOptionTranslations.Add(ratingOptionTranslation);
            await _dbContext.SaveChangesAsync();
            return ratingOptionTranslation;
        }

        #endregion

        #region Getters

        public async Task<List<RatingOptionTranslation>> GetAllRatingOptionTranslations()
        {
            _logger.LogInformation($"{nameof(RatingOptionTranslationsRepository)}.{nameof(GetAllRatingOptionTranslations)} foi iniciado.");
            return await _dbContext.RatingOptionTranslations.ToListAsync();
        }

        public async Task<RatingOptionTranslation?> GetRatingOptionTranslationById(Guid ratingOptionTranslationId)
        {
            _logger.LogInformation($"{nameof(RatingOptionTranslationsRepository)}.{nameof(GetRatingOptionTranslationById)} foi iniciado.");
            return await _dbContext.RatingOptionTranslations.FirstOrDefaultAsync(temp => temp.RatingOptionTranslationId == ratingOptionTranslationId);
        }

        public async Task<List<RatingOptionTranslation>> GetRatingOptionTranslationByRatingOptionId(Guid ratingOptionId)
        {
            _logger.LogInformation($"{nameof(RatingOptionTranslationsRepository)}.{nameof(GetRatingOptionTranslationByRatingOptionId)} foi iniciado.");
            return await _dbContext.RatingOptionTranslations.Where(temp => temp.RatingOptionId == ratingOptionId).ToListAsync();
        }

        #endregion
    }
}
