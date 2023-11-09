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
    public class TranslationsGetterService : ITranslationsGetterService
    {
        private readonly ITranslationsRepository _translationsRepository;
        private readonly ILogger<TranslationsGetterService> _logger;

        public TranslationsGetterService(ITranslationsRepository translationsRepository, ILogger<TranslationsGetterService> logger)
        {
            _translationsRepository = translationsRepository;
            _logger = logger;
        }

        #region FormTemplateTranslation Getters
        public async Task<List<FormTemplateTranslationDTOResponse>> GetAllFormTemplateTranslations()
        {
            _logger.LogInformation("TranslationsGetterService.GetAllFormTemplateTranslations foi iniciado");
            var translations = await _translationsRepository.GetAllFormTemplateTranslations();

            return translations.Select(temp => temp.ToFormTemplateTranslationDTOResponse()).ToList();
        }

        public async Task<List<FormTemplateTranslationDTOResponse>?> GetFilteredTranslationsByFormTemplateId(Guid? formTemplateId)
        {
            _logger.LogInformation("TranslationsGetterService.GetFilteredTranslationsByFormTemplateId foi iniciado");

            if(formTemplateId == null)
            {
                return null;
            }
            var translations = await _translationsRepository.GetFilteredTranslationsByFormTemplateId(formTemplateId.Value);
            
            if(translations.Count <= 0)
            {
                return null;
            }

            return translations.Select(temp => temp.ToFormTemplateTranslationDTOResponse()).ToList();
           
        }

        public async Task<FormTemplateTranslationDTOResponse?> GetFormTemplateTranslationById(Guid? formTemplateTranslationId)
        {
            _logger.LogInformation("TranslationsGetterService.GetFormTemplateTranslationById foi iniciado");

            if (formTemplateTranslationId == null)
            {
                return null;
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
