﻿using Microsoft.EntityFrameworkCore;
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
    public class RatingOptionTranslationRepository : IRatingOptionTranslationsRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<RatingOptionTranslationRepository> _logger;

        public RatingOptionTranslationRepository(ApplicationDbContext dbContext, ILogger<RatingOptionTranslationRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        #region Adders

        public async Task<RatingOptionTranslation> AddRatingOptionTranslation(RatingOptionTranslation ratingOptionTranslation)
        {
            _logger.LogInformation($"{nameof(RatingOptionTranslationRepository)}.{nameof(AddRatingOptionTranslation)} foi iniciado.");

            _dbContext.RatingOptionTranslations.Add(ratingOptionTranslation);
            await _dbContext.SaveChangesAsync();
            return ratingOptionTranslation;
        }

        #endregion

        #region Getters

        public async Task<List<RatingOptionTranslation>> GetAllRatingOptionTranslations()
        {
            _logger.LogInformation($"{nameof(RatingOptionTranslationRepository)}.{nameof(GetAllRatingOptionTranslations)} foi iniciado.");
            return await _dbContext.RatingOptionTranslations.ToListAsync();
        }

        public async Task<RatingOptionTranslation?> GetRatingOptionTranslationById(Guid ratingOptionTranslationId)
        {
            _logger.LogInformation($"{nameof(RatingOptionTranslationRepository)}.{nameof(GetRatingOptionTranslationById)} foi iniciado.");
            return await _dbContext.RatingOptionTranslations.FirstOrDefaultAsync(temp => temp.RatingOptionTranslationId == ratingOptionTranslationId);
        }

        public async Task<List<RatingOptionTranslation>> GetRatingOptionTranslationByRatingOptionId(Guid ratingOptionId)
        {
            _logger.LogInformation($"{nameof(RatingOptionTranslationRepository)}.{nameof(GetRatingOptionTranslationByRatingOptionId)} foi iniciado.");
            return await _dbContext.RatingOptionTranslations.Where(temp => temp.RatingOptionId == ratingOptionId).ToListAsync();
        }

        #endregion

        #region Deleters

        public async Task<bool> DeleteRatingOptionTranslationById(Guid ratingOptionTranslationId)
        {
            _dbContext.RatingOptionTranslations.RemoveRange(_dbContext.RatingOptionTranslations.Where(temp => temp.RatingOptionTranslationId == ratingOptionTranslationId));
            int rowsDeleted = await _dbContext.SaveChangesAsync();

            return rowsDeleted > 0;
        }

        #endregion
    }
}
