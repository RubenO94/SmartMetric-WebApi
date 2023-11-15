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
    public class SingleChoiceOptionTranslationDeleterService : ISingleChoiceOptionTranslationDeleterService
    {
        //variables
        private readonly ISingleChoiceOptionTranslationsRepository _translationRepository;
        private readonly ILogger<SingleChoiceOptionTranslationDeleterService> _logger;

        //constructor
        public SingleChoiceOptionTranslationDeleterService (ISingleChoiceOptionTranslationsRepository translationRepository, ILogger<SingleChoiceOptionTranslationDeleterService> logger)
        {
            _translationRepository = translationRepository;
            _logger = logger;
        }

        //deleters
        public async Task<bool> DeleteSingleChoiceOptionTranslationById(Guid? singleChoiceOptionTranslationId)
        {
            _logger.LogInformation($"{nameof(SingleChoiceOptionTranslationDeleterService)}.{nameof(DeleteSingleChoiceOptionTranslationById)} foi iniciado");

            if (singleChoiceOptionTranslationId == null ) { return false; }

            return await _translationRepository.DeleteSingleChoiceOptionTranslationById(singleChoiceOptionTranslationId.Value);
        }
    }
}
