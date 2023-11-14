using Microsoft.Extensions.Logging;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.ServicesContracts.Getters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.Services.Getters
{
    public class FormTemplatesGetterService : IFormTemplatesGetterService
    {
        private readonly IFormTemplatesRepository _formTemplateRepository;
        private readonly ILogger<FormTemplatesGetterService> _logger;
        public FormTemplatesGetterService(IFormTemplatesRepository formTemplateRepository, ILogger<FormTemplatesGetterService> logger)
        {
            _formTemplateRepository = formTemplateRepository;
            _logger = logger;
        }

        public async Task<List<FormTemplateDTOResponse?>> GetAllFormTemplates()
        {
            _logger.LogInformation($"{nameof(FormTemplatesGetterService)}.{nameof(GetAllFormTemplates)} foi iniciado");

            var formTemplates = await _formTemplateRepository.GetAllFormTemplates();

            var response = formTemplates.Select(temp => temp.ToFormTemplateDTOResponse()).ToList();

            if (response != null)
            {
                return response!;
            }
            return new List<FormTemplateDTOResponse?>();
        }

        public async Task<FormTemplateDTOResponse?> GetFormTemplateById(Guid formTemplateId)
        {
            _logger.LogInformation($"{nameof(FormTemplatesGetterService)}.{nameof(GetFormTemplateById)} foi iniciado");

            var formTemplate = await _formTemplateRepository.GetFormTemplateById(formTemplateId);

            if (formTemplate != null)
            {
                var response = formTemplate.ToFormTemplateDTOResponse();
                return response!;
            }

            return null;
        }
    }
}
