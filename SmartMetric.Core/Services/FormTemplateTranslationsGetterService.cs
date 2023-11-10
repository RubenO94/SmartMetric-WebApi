using Microsoft.Extensions.Logging;
using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Core.DTO;
using SmartMetric.Core.Enums;
using SmartMetric.Core.ServicesContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.Services
{
    public class FormTemplateTranslationsGetterService : IFormTemplateTranslationsGetterService
    {
        private readonly IFormTemplateTranslationsRepository _translationsRepository;
        private readonly ILogger<FormTemplateTranslationsGetterService> _logger;

        public FormTemplateTranslationsGetterService(IFormTemplateTranslationsRepository translationsRepository, ILogger<FormTemplateTranslationsGetterService> logger)
        {
            _translationsRepository = translationsRepository;
            _logger = logger;
        }

        #region FormTemplateTranslation Getters
        public async Task<List<FormTemplateTranslationDTOResponse>> GetAllFormTemplateTranslations()
        {
            _logger.LogInformation($"{nameof(FormTemplateTranslationsGetterService)}.{nameof(GetAllFormTemplateTranslations)} foi iniciado");
            var translations = await _translationsRepository.GetAllFormTemplateTranslations();

            return translations.Select(temp => temp.ToFormTemplateTranslationDTOResponse()).ToList();
        }

        public async Task<List<FormTemplateTranslationDTOResponse>?> GetFilteredTranslationsByFormTemplateId(Guid? formTemplateId)
        {
            _logger.LogInformation($"{nameof(FormTemplateTranslationsGetterService)}.{nameof(GetFilteredTranslationsByFormTemplateId)} foi iniciado");

            if (formTemplateId == null)
            {
                throw new ArgumentNullException(nameof(formTemplateId));
            }
            var translations = await _translationsRepository.GetFilteredTranslationsByFormTemplateId(formTemplateId.Value);
            

            return translations.Select(temp => temp.ToFormTemplateTranslationDTOResponse()).ToList();
           
        }

        public async Task<FormTemplateTranslationDTOResponse?> GetFormTemplateTranslationById(Guid? formTemplateTranslationId)
        {
            _logger.LogInformation($"{nameof(FormTemplateTranslationsGetterService)}.{nameof(GetFormTemplateTranslationById)} foi iniciado");

            if (formTemplateTranslationId == null)
            {
                throw new ArgumentNullException(nameof(formTemplateTranslationId));
            }

            FormTemplateTranslation? translation = await _translationsRepository.GetFormTemplateTranslationById(formTemplateTranslationId.Value);

            if (translation == null)
            {
                return null;
            }
            return translation.ToFormTemplateTranslationDTOResponse();
        }
        #endregion
    }
}
