using Microsoft.Extensions.Logging;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Core.DTO;
using SmartMetric.Core.Helpers;
using SmartMetric.Core.ServicesContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.Services
{
    public class SingleChoiceOptionTranslationsAdderService : ISingleChoiceOptionTranslationsAdderService
    {
        private readonly ISingleChoiceOptionTranslationsRepository _translationsRepository;
        private readonly ILogger<SingleChoiceOptionTranslationsAdderService> _logger;

        public SingleChoiceOptionTranslationsAdderService(ISingleChoiceOptionTranslationsRepository translationsRepository, ILogger<SingleChoiceOptionTranslationsAdderService> logger)
        {
            _translationsRepository = translationsRepository;
            _logger = logger;
        }

        public async Task<SingleChoiceOptionTranslationDTOResponse> AddSingleChoiceOptionTranslation(SingleChoiceOptionTranslationDTOAddRequest? request)
        {
            if(request == null) throw new ArgumentNullException(nameof(request));

            ValidationHelper.ModelValidation(request);

            var translation = request.ToSingleChoiceOptionTranslation();
            translation.SingleChoiceOptionTranslationId = Guid.NewGuid();

            await _translationsRepository.AddSingleChoiceOptionTranslation(translation);

            return translation.ToSingleChoiceOptionTranslationDTOResponse();
        }
    }
}
