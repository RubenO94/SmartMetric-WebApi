using Microsoft.Extensions.Logging;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Core.Services.Getters;
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

        private IFormTemplatesRepository _formTemplateRepository;
        private ILogger<FormTemplatesDeleterService> _logger;

        public FormTemplatesDeleterService(IFormTemplatesRepository formTemplateRepository, ILogger<FormTemplatesDeleterService> logger)
        {
            _formTemplateRepository = formTemplateRepository;
            _logger = logger;
        }

        public async Task<bool> DeleteFormTemplateById(Guid? formTemplateId)
        {
            _logger.LogInformation($"{nameof(FormTemplatesDeleterService)}.{nameof(DeleteFormTemplateById)} foi iniciado");

            if (formTemplateId == null)
            {
                return false;
            }

            return await _formTemplateRepository.DeleteFormTemplateById(formTemplateId.Value);
        }
    }
}
