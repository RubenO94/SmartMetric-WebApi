using Microsoft.Extensions.Logging;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Core.DTO;
using SmartMetric.Core.ServicesContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.Services
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

        public Task<SingleChoiceOptionTranslationDTOResponse?> GetSingleChoiceOptionTranslationById(Guid? singleChoiceOptionTranslationId)
        {
            throw new NotImplementedException();
        }

        public Task<List<SingleChoiceOptionTranslationDTOResponse>?> GetTranslationsBySingleChoiceOptionId(Guid? singleChoiceOptionId)
        {
            throw new NotImplementedException();
        }
    }
}
