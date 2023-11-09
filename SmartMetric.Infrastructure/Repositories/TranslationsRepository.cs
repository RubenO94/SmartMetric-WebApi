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
    public class TranslationsRepository : ITranslationsRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<TranslationsRepository> _logger;

        TranslationsRepository(ApplicationDbContext dbContext, ILogger<TranslationsRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        #region Adders

        public async Task<FormTemplateTranslation> AddFormTemplateTranslation(FormTemplateTranslation formTemplateTranslation)
        {
            _logger.LogInformation("FormTemplateTranslationRepository.AddFormTemplateTranslation foi iniciado.");

            _dbContext.FormTemplateTranslations.Add(formTemplateTranslation);
            await _dbContext.SaveChangesAsync();
            return formTemplateTranslation;
        }

        public async Task<QuestionTranslation> AddQuestionTranslation(QuestionTranslation questionTranslation)
        {
            _logger.LogInformation("FormTemplateTranslationRepository.AddQuestionTranslation foi iniciado.");

            _dbContext.QuestionTranslations.Add(questionTranslation);
            await _dbContext.SaveChangesAsync();
            return questionTranslation;
        }

        public async Task<RatingOptionTranslation> AddRatingOptionTranslation(RatingOptionTranslation ratingOptionTranslation)
        {
            _logger.LogInformation("FormTemplateTranslationRepository.AddRatingOptionTranslation foi iniciado.");

            _dbContext.RatingOptionTranslations.Add(ratingOptionTranslation);
            await _dbContext.SaveChangesAsync();
            return ratingOptionTranslation;
        }

        public async Task<SingleChoiceOptionTranslation> AddSingleChoiceOptionTranslation(SingleChoiceOptionTranslation singleChoiceOptionTranslation)
        {
            _logger.LogInformation("FormTemplateTranslationRepository.AddSingleChoiceOptionTranslation foi iniciado.");

            _dbContext.SingleChoiceOptionTranslations.Add(singleChoiceOptionTranslation);
            await _dbContext.SaveChangesAsync();
            return singleChoiceOptionTranslation;
        }

        #endregion

        #region Getters

        public async Task<List<FormTemplateTranslation>> GetAllFormTemplateTranslations()
        {
            _logger.LogInformation("FormTemplateTranslationRepository.GetAllFormTemplateTranslations foi iniciado.");
            return await _dbContext.FormTemplateTranslations.ToListAsync();
        }

        public async Task<List<FormTemplateTranslation>> GetFilteredTranslationsByFormTemplateId(Guid formTemplateId)
        {
            _logger.LogInformation("FormTemplateTranslationRepository.GetFilteredTranslationsByFormTemplateId foi iniciado.");
            return await _dbContext.FormTemplateTranslations.Where(temp => temp.FormTemplateId == formTemplateId).ToListAsync();
        }

        public async Task<FormTemplateTranslation?> GetFormTemplateTranslationById(Guid formTemplateTranslationId)
        {
            _logger.LogInformation("FormTemplateTranslationRepository.GetFormTemplateTranslationById foi iniciado.");
            return await _dbContext.FormTemplateTranslations.FirstOrDefaultAsync(temp => temp.FormTemplateTranslationId == formTemplateTranslationId);
        }

        #endregion
    }
}
