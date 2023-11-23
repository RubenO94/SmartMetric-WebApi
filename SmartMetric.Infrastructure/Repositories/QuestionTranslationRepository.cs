using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Core.Enums;
using SmartMetric.Infrastructure.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Infrastructure.Repositories
{
    public class QuestionTranslationRepository : IQuestionTranslationRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<QuestionTranslationRepository> _logger;

        public QuestionTranslationRepository(ApplicationDbContext dbContext, ILogger<QuestionTranslationRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        #region Adders

        public async Task<QuestionTranslation> AddQuestionTranslation(QuestionTranslation questionTranslation)
        {
            _logger.LogInformation($"{nameof(QuestionTranslationRepository)}.{nameof(AddQuestionTranslation)} foi iniciado.");

            _dbContext.QuestionTranslations.Add(questionTranslation);
            await _dbContext.SaveChangesAsync();
            return questionTranslation;
        }

        #endregion

        #region Getters

        public async Task<List<QuestionTranslation>> GetAllQuestionTranslations()
        {
            _logger.LogInformation($"{nameof(QuestionTranslationRepository)}.{nameof(GetAllQuestionTranslations)} foi iniciado");
            return await _dbContext.QuestionTranslations.ToListAsync();
        }

        public async Task<QuestionTranslation?> GetQuestionTranslationsById(Guid questionTranslationId)
        {
            _logger.LogInformation($"{nameof(QuestionTranslationRepository)}.{nameof(GetQuestionTranslationsById)} foi iniciado");
            return await _dbContext.QuestionTranslations.FirstOrDefaultAsync(temp => temp.QuestionTranslationId == questionTranslationId);
        }

        public async Task<List<QuestionTranslation>> GetQuestionTranslationsByQuestionId(Guid questionId)
        {
            _logger.LogInformation($"{nameof(QuestionTranslationRepository)}.{nameof(GetQuestionTranslationsByQuestionId)} foi iniciado");
            return await _dbContext.QuestionTranslations.Where(temp => temp.QuestionId == questionId).ToListAsync();
        }

        #endregion

        #region Deleters

        public async Task<bool> DeleteQuestionTranslationById(Guid questionTranslationId)
        {
            _dbContext.QuestionTranslations.RemoveRange(_dbContext.QuestionTranslations.Where(temp => temp.QuestionTranslationId == questionTranslationId));
            int rowsDeleted = await _dbContext.SaveChangesAsync();

            return rowsDeleted > 0;
        }

        #endregion
    }
}
