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
    public class FormTemplateTranslationRepository : IFormTemplateTranslationRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<FormTemplateTranslationRepository> _logger;

        public FormTemplateTranslationRepository(ApplicationDbContext dbContext, ILogger<FormTemplateTranslationRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        #region Adders

        public async Task<FormTemplateTranslation> AddFormTemplateTranslation(FormTemplateTranslation formTemplateTranslation)
        {
            _logger.LogInformation($"{nameof(FormTemplateTranslationRepository)}.{nameof(AddFormTemplateTranslation)} foi iniciado.");

            _dbContext.FormTemplateTranslations.Add(formTemplateTranslation);
            await _dbContext.SaveChangesAsync();
            return formTemplateTranslation;
        }

        #endregion

        #region Getters

        public async Task<List<FormTemplateTranslation>> GetAllFormTemplateTranslations()
        {
            _logger.LogInformation($"{nameof(FormTemplateTranslationRepository)}.{nameof(GetAllFormTemplateTranslations)} foi iniciado.");
            return await _dbContext.FormTemplateTranslations.ToListAsync();
        }

        public async Task<List<FormTemplateTranslation>> GetTranslationsByFormTemplateId(Guid formTemplateId)
        {
            _logger.LogInformation($"{nameof(FormTemplateTranslationRepository)}.{nameof(GetTranslationsByFormTemplateId)} foi iniciado.");
            return await _dbContext.FormTemplateTranslations.Where(temp => temp.FormTemplateId == formTemplateId).ToListAsync();
        }

        public async Task<FormTemplateTranslation?> GetFormTemplateTranslationById(Guid formTemplateTranslationId)
        {
            _logger.LogInformation($"{nameof(FormTemplateTranslationRepository)}.{nameof(GetFormTemplateTranslationById)} foi iniciado.");
            return await _dbContext.FormTemplateTranslations.FirstOrDefaultAsync(temp => temp.FormTemplateTranslationId == formTemplateTranslationId);
        }

        #endregion

        #region Deleters

        public async Task<bool> DeleteFormTemplateTranslationById(Guid formTemplateTranslationId)
        {
            _dbContext.FormTemplateTranslations.RemoveRange(_dbContext.FormTemplateTranslations.Where(temp => temp.FormTemplateTranslationId.Equals(formTemplateTranslationId)));
            int rowsDeleted = await _dbContext.SaveChangesAsync();

            return rowsDeleted > 0;
        }

        #endregion
    }
}
