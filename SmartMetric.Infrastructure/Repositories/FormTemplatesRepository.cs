using Azure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Infrastructure.DatabaseContext;

namespace SmartMetric.Infrastructure.Repositories
{
    public class FormTemplatesRepository : IFormTemplatesRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<FormTemplatesRepository> _logger;
        public FormTemplatesRepository(ApplicationDbContext dbContext, ILogger<FormTemplatesRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<FormTemplate> AddFormTemplate(FormTemplate formTemplate)
        {
            _logger.LogInformation($"{nameof(FormTemplatesRepository)}.{nameof(AddFormTemplate)} foi iniciado");

            _dbContext.FormTemplates.Add(formTemplate);
            await _dbContext.SaveChangesAsync();

            return formTemplate;
        }

        public async Task<bool> DeleteFormTemplateById(Guid formTemplateId)
        {
            _dbContext.FormTemplates.RemoveRange(_dbContext.FormTemplates.Where(temp => temp.FormTemplateId == formTemplateId));
            int rowsDeleted = await _dbContext.SaveChangesAsync();

            return rowsDeleted > 0;
        }

        public async Task<List<FormTemplate>> GetAllFormTemplates()
        {
            _logger.LogInformation($"{nameof(FormTemplatesRepository)}.{nameof(GetAllFormTemplates)} foi iniciado");


            return await _dbContext.FormTemplates
                .Include(temp => temp.Translations)
                .Include(temp => temp.FormTemplateQuestions)!.ThenInclude(ftq => ftq.Question).ThenInclude(q => q!.Translations)
                .Include(temp => temp.FormTemplateQuestions)!.ThenInclude(ftq => ftq.Question).ThenInclude(q => q.RatingOptions).ThenInclude(rt => rt.Translations)
                .Include(temp => temp.FormTemplateQuestions)!.ThenInclude(ftq => ftq.Question).ThenInclude(q => q.SingleChoiceOptions).ThenInclude(sco => sco.Translations)
                .ToListAsync();
        }


        public async Task<FormTemplate?> GetFormTemplateById(Guid formTemplateId)
        {
            _logger.LogInformation($"{nameof(FormTemplatesRepository)}.{nameof(GetFormTemplateById)} foi iniciado");



            var response = await _dbContext.FormTemplates
                .Include(temp => temp.Translations)
                .Include(temp => temp.FormTemplateQuestions)!.ThenInclude(ftq => ftq.Question).ThenInclude(q => q!.Translations)
                .Include(temp => temp.FormTemplateQuestions)!.ThenInclude(ftq => ftq.Question).ThenInclude(q => q.RatingOptions).ThenInclude(rt => rt.Translations)
                .Include(temp => temp.FormTemplateQuestions)!.ThenInclude(ftq => ftq.Question).ThenInclude(q => q.SingleChoiceOptions).ThenInclude(sco => sco.Translations)
                .FirstOrDefaultAsync(template => template.FormTemplateId == formTemplateId);

            return response;
        }
            
    }
}
