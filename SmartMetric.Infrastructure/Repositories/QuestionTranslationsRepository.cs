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
    public class QuestionTranslationsRepository : IQuestionTranslationsRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<QuestionTranslationsRepository> _logger;

        QuestionTranslationsRepository(ApplicationDbContext dbContext, ILogger<QuestionTranslationsRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        #region Adders

        public async Task<QuestionTranslation> AddQuestionTranslation(QuestionTranslation questionTranslation)
        {
            _logger.LogInformation($"{nameof(QuestionTranslationsRepository)}.{nameof(AddQuestionTranslation)} foi iniciado.");

            _dbContext.QuestionTranslations.Add(questionTranslation);
            await _dbContext.SaveChangesAsync();
            return questionTranslation;
        }

        #endregion

        #region Getters

        public async Task<List<QuestionTranslation>> GetAllQuestionTranslations()
        {
            _logger.LogInformation($"{nameof(QuestionTranslationsRepository)}.{nameof(GetAllQuestionTranslations)} foi iniciado");
            return await _dbContext.QuestionTranslations.ToListAsync();
        }

        public async Task<QuestionTranslation?> GetQuestionTranslationsById(Guid questionTranslationId)
        {
            _logger.LogInformation($"{nameof(QuestionTranslationsRepository)}.{nameof(GetQuestionTranslationsById)} foi iniciado");
            return await _dbContext.QuestionTranslations.FirstOrDefaultAsync(temp => temp.QuestionTranslationId == questionTranslationId);
        }

        public async Task<List<QuestionTranslation>> GetQuestionTranslationsByQuestionId(Guid questionId)
        {
            _logger.LogInformation($"{nameof(QuestionTranslationsRepository)}.{nameof(GetQuestionTranslationsByQuestionId)} foi iniciado");
            return await _dbContext.QuestionTranslations.Where(temp => temp.QuestionId == questionId).ToListAsync();
        }

        #endregion
    }
}