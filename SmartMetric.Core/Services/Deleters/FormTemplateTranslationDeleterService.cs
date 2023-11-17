using Microsoft.Extensions.Logging;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Core.Enums;
using SmartMetric.Core.ServicesContracts.Deleters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.Services.Deleters
{
    public class FormTemplateTranslationDeleterService : IFormTemplateTranslationsDeleterService
    {
        //VARIABLES
        private readonly IFormTemplateTranslationsRepository _formTemplateTranslationsRepository;
        private readonly ILogger<FormTemplateTranslationDeleterService> _logger;

        //CONSTRUCTOR
        public FormTemplateTranslationDeleterService (IFormTemplateTranslationsRepository formTemplateTranslationsRepository, ILogger<FormTemplateTranslationDeleterService> logger)
        {
            _formTemplateTranslationsRepository = formTemplateTranslationsRepository;
            _logger = logger;
        }

        //DELETERS
        public async Task<bool> DeleteFormTemplateTranslationById(Guid? formTemplate, Language language)
        {
            _logger.LogInformation($"{nameof(FormTemplateTranslationDeleterService)}.{nameof(DeleteFormTemplateTranslationById)} foi iniciado");

            if (formTemplate == null ) { return false; }
            return await _formTemplateTranslationsRepository.DeleteFormTemplateTranslationById(formTemplate.Value);
        }
    }
}
