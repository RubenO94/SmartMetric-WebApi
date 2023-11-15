using Microsoft.Extensions.Logging;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Core.ServicesContracts.Deleters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.Services.Deleters
{
    public class FormTemplatesDeleterService : IFormTemplatesDeleterService
    {

        private IFormTemplateRepository _formTemplateRepository;
        private ILogger<FormTemplatesDeleterService> _logger;

        public FormTemplatesDeleterService(IFormTemplateRepository formTemplateRepository, ILogger<FormTemplatesDeleterService> logger)
        {
            _formTemplateRepository = formTemplateRepository;
            _logger = logger;
        }

        public async Task<bool> DeleteFormTemplateById(Guid? formTemplateId)
        {
            if(formTemplateId == null)
            {
                return false;
            }

            return await _formTemplateRepository.DeleteFormTemplateById(formTemplateId.Value);
        }
    }
}
