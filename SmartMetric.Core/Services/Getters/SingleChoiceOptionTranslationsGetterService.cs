using Microsoft.Extensions.Logging;
using SmartMetric.Core.Domain.Entities;
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
    public class SingleChoiceOptionTranslationsGetterService : ISingleChoiceOptionTranslationsGetterService
    {
        private readonly ISingleChoiceOptionTranslationsRepository _translationsRepository;
        private readonly ILogger<SingleChoiceOptionTranslationsGetterService> _logger;

        public SingleChoiceOptionTranslationsGetterService(ISingleChoiceOptionTranslationsRepository translationsRepository, ILogger<SingleChoiceOptionTranslationsGetterService> logger)
        {
            _translationsRepository = translationsRepository;
            _logger = logger;
        }
        public async Task<List<SingleChoiceOptionTranslationDTOResponse>> GetAllSingleChoiceOptionTranslations()
        {
            _logger.LogInformation($"{nameof(SingleChoiceOptionTranslationsGetterService)}.{nameof(GetAllSingleChoiceOptionTranslations)} foi iniciado");
            var translations = await _translationsRepository.GetAllSingleChoiceOptionTranslations();

            return translations.Select(temp => temp.ToSingleChoiceOptionTranslationDTOResponse()).ToList();
        }

        public async Task<SingleChoiceOptionTranslationDTOResponse?> GetSingleChoiceOptionTranslationById(Guid? singleChoiceOptionTranslationId)
        {
            _logger.LogInformation($"{nameof(SingleChoiceOptionTranslationsGetterService)}.{nameof(GetSingleChoiceOptionTranslationById)} foi iniciado");

            if (singleChoiceOptionTranslationId == null)
            {
                throw new ArgumentNullException(nameof(singleChoiceOptionTranslationId));
            }

            SingleChoiceOptionTranslation? translation = await _translationsRepository.GetSingleChoiceOptionTranslationById(singleChoiceOptionTranslationId.Value);

            if (translation == null)
            {
                return null;
            }

            return translation.ToSingleChoiceOptionTranslationDTOResponse();
        }

        public async Task<List<SingleChoiceOptionTranslationDTOResponse>?> GetTranslationsBySingleChoiceOptionId(Guid? singleChoiceOptionId)
        {
            _logger.LogInformation($"{nameof(SingleChoiceOptionTranslationsGetterService)}.{nameof(GetTranslationsBySingleChoiceOptionId)} foi iniciado");

            if (singleChoiceOptionId == null)
            {
                throw new ArgumentNullException(nameof(singleChoiceOptionId));
            }
            var translations = await _translationsRepository.GetTranslationsBySingleChoiceOptionId(singleChoiceOptionId.Value);


            return translations.Select(temp => temp.ToSingleChoiceOptionTranslationDTOResponse()).ToList();
        }
    }
}
