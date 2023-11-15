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
    public class SingleChoiceOptionTranslationsRepository : ISingleChoiceOptionTranslationsRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<RatingOptionTranslationsRepository> _logger;

        SingleChoiceOptionTranslationsRepository(ApplicationDbContext dbContext, ILogger<RatingOptionTranslationsRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        #region Adders

        public async Task<SingleChoiceOptionTranslation> AddSingleChoiceOptionTranslation(SingleChoiceOptionTranslation singleChoiceOptionTranslation)
        {
            _logger.LogInformation($"{nameof(SingleChoiceOptionTranslationsRepository)}.{nameof(AddSingleChoiceOptionTranslation)} foi iniciado.");

            _dbContext.SingleChoiceOptionTranslations.Add(singleChoiceOptionTranslation);
            await _dbContext.SaveChangesAsync();
            return singleChoiceOptionTranslation;
        }

        #endregion

        #region Getters

        public async Task<List<SingleChoiceOptionTranslation>> GetAllSingleChoiceOptionTranslations()
        {
            _logger.LogInformation($"{nameof(SingleChoiceOptionTranslationsRepository)}.{nameof(AddSingleChoiceOptionTranslation)} foi iniciado.");
            return await _dbContext.SingleChoiceOptionTranslations.ToListAsync();
        }

        public async Task<List<SingleChoiceOptionTranslation>> GetTranslationsBySingleChoiceOptionId(Guid formTemplateId)
        {
            _logger.LogInformation($"{nameof(SingleChoiceOptionTranslationsRepository)}.{nameof(GetTranslationsBySingleChoiceOptionId)} foi iniciado.");
            return await _dbContext.SingleChoiceOptionTranslations.Where(temp => temp.SingleChoiceOptionId == formTemplateId).ToListAsync();
        }

        public async Task<SingleChoiceOptionTranslation?> GetSingleChoiceOptionTranslationById(Guid singleChoiceOptionTranslationId)
        {
            _logger.LogInformation($"{nameof(SingleChoiceOptionTranslationsRepository)}.{nameof(GetSingleChoiceOptionTranslationById)} foi iniciado.");
            return await _dbContext.SingleChoiceOptionTranslations.FirstOrDefaultAsync(temp => temp.SingleChoiceOptionTranslationId == singleChoiceOptionTranslationId);
        }

        #endregion

        #region Deleters

        public async Task<bool> DeleteSingleChoiceOptionTranslationById(Guid singleChoiceOptionTranslationId)
        {
            _dbContext.SingleChoiceOptionTranslations
                .RemoveRange(_dbContext.SingleChoiceOptionTranslations.Where(temp => temp.SingleChoiceOptionTranslationId == singleChoiceOptionTranslationId));
            int rowsDeleted = await _dbContext.SaveChangesAsync();

            return rowsDeleted > 0;
        }

        #endregion
    }
}
