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
    internal class FormTemplatesRepository : IFormTemplateRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<FormTemplatesRepository> _logger;
        public FormTemplatesRepository(ApplicationDbContext dbContext, ILogger<FormTemplatesRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public Task<FormTemplate?> AddFormTemplate(FormTemplate formTemplate)
        {
            throw new NotImplementedException();
        }

        public Task<List<FormTemplate>?> GetAllFormTemplates()
        {
            throw new NotImplementedException();
        }

        public async Task<FormTemplate?> GetFormTemplateById(Guid formTemplateId)
        {
            _logger.LogInformation("GetFormTemplateByID foi iniciado");
            return await _dbContext.FormTemplates.Include("FormTemplateTranslations").FirstOrDefaultAsync(tempalte =>  tempalte.FormTemplateId == formTemplateId);
        }
    }
}
