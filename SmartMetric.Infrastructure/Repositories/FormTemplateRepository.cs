using Azure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Infrastructure.DatabaseContext;
using SmartMetric.Infrastructure.Repositories.Common;
using System.Linq.Expressions;

namespace SmartMetric.Infrastructure.Repositories
{
    public class FormTemplateRepository : BaseRepository, IFormTemplateRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<FormTemplateRepository> _logger;
        public FormTemplateRepository(ApplicationDbContext dbContext, ILogger<FormTemplateRepository> logger) : base(dbContext)
        {
            _dbContext = dbContext;
            _logger = logger;
        }



        public async Task<FormTemplate> AddFormTemplate(FormTemplate formTemplate)
        {
            _logger.LogInformation($"{nameof(FormTemplateRepository)}.{nameof(AddFormTemplate)} foi iniciado");

            _dbContext.FormTemplates.Add(formTemplate);
            await _dbContext.SaveChangesAsync();

            return formTemplate;
        }

        public async Task<bool> DeleteFormTemplateById(Guid formTemplateId)
        {
            var formTemplateToDelete = await _dbContext.FormTemplates.FindAsync(formTemplateId);
            if (formTemplateToDelete != null)
            {
                _dbContext.FormTemplates.Remove(formTemplateToDelete);

                var rowsDeleted = await _dbContext.SaveChangesAsync();

                return rowsDeleted > 0;
            }

            return false;

        }

        public async Task<List<FormTemplate>> GetAllFormTemplates(int page = 1, int pageSize = 20, string? language = null)
        {
            _logger.LogInformation($"{nameof(FormTemplateRepository)}.{nameof(GetAllFormTemplates)} foi iniciado");

            IQueryable<FormTemplate> query = _dbContext.FormTemplates
                .Include(temp => temp.Translations)
                .Include(temp => temp.Questions).ThenInclude(q => q!.Translations)
                .Include(temp => temp.Questions).ThenInclude(q => q.RatingOptions).ThenInclude(rt => rt.Translations)
                .Include(temp => temp.Questions).ThenInclude(q => q.SingleChoiceOptions).ThenInclude(sco => sco.Translations);

            // Aplica a filtragem por idioma, se fornecido
            if (!string.IsNullOrEmpty(language))
            {
                query = query.Where(temp => temp.Translations!.Any(tr => tr.Language == language));
            }

            // Aplica a paginação
            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            return await query.ToListAsync();
        }


        public async Task<FormTemplate?> GetFormTemplateById(Guid? formTemplateId)
        {
            _logger.LogInformation($"{nameof(FormTemplateRepository)}.{nameof(GetFormTemplateById)} foi iniciado");



            var response = await _dbContext.FormTemplates
                .Include(temp => temp.Translations)
                .Include(temp => temp.Questions)!.ThenInclude(q => q!.Translations)
                .Include(temp => temp.Questions)!.ThenInclude(q => q.RatingOptions).ThenInclude(rt => rt.Translations)
                .Include(temp => temp.Questions)!.ThenInclude(q => q.SingleChoiceOptions).ThenInclude(sco => sco.Translations)
                .FirstOrDefaultAsync(template => template.FormTemplateId == formTemplateId);

            return response;
        }

        public async Task<int> GetTotalRecords(Expression<Func<FormTemplate, bool>>? filter = null)
        {
            return await base.CountRecords<FormTemplate>(filter);
        }

        public async Task<FormTemplate> UpdateFormTemplate(FormTemplate formTemplate)
        {
            var matchingFormTemplate = await _dbContext.FormTemplates.FirstOrDefaultAsync(temp => temp.FormTemplateId == formTemplate.FormTemplateId);

            if (matchingFormTemplate == null) return formTemplate;

            matchingFormTemplate.ModifiedDate = formTemplate.ModifiedDate;
            matchingFormTemplate.Questions = formTemplate.Questions;
            matchingFormTemplate.Translations = formTemplate.Translations;

            await _dbContext.SaveChangesAsync();

            return matchingFormTemplate;

        }
    }
}
