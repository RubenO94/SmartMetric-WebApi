using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Core.DTO;
using SmartMetric.Infrastructure.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Infrastructure.Repositories
{
    public class FormTemplatesRepository : IFormTemplateRepository
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

        public async Task<List<FormTemplate>> GetAllFormTemplates()
        {
            _logger.LogInformation($"{nameof(FormTemplatesRepository)}.{nameof(GetAllFormTemplates)} foi iniciado");

            return await _dbContext.FormTemplates.Include("FormTemplateTranslations").ToListAsync();
        }

        public async Task<FormTemplate?> GetFormTemplateById(Guid formTemplateId)
        {
            _logger.LogInformation($"{nameof(FormTemplatesRepository)}.{nameof(GetFormTemplateById)} foi iniciado");
            return await _dbContext.FormTemplates.Include("FormTemplateTranslations").FirstOrDefaultAsync(tempalte =>  tempalte.FormTemplateId == formTemplateId);
        }
    }
}
