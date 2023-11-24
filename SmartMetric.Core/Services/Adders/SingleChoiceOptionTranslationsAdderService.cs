using Microsoft.Extensions.Logging;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Core.DTO.AddRequest;
using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.Exceptions;
using SmartMetric.Core.Helpers;
using SmartMetric.Core.ServicesContracts.Adders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.Services.Adders
{
    public class SingleChoiceOptionTranslationsAdderService : ISingleChoiceOptionTranslationsAdderService
    {
        private readonly ISingleChoiceOptionTranslationRepository _translationsRepository;
        private readonly ILogger<SingleChoiceOptionTranslationsAdderService> _logger;

        public SingleChoiceOptionTranslationsAdderService(ISingleChoiceOptionTranslationRepository translationsRepository, ILogger<SingleChoiceOptionTranslationsAdderService> logger)
        {
            _translationsRepository = translationsRepository;
            _logger = logger;
        }

        public async Task<ApiResponse<SingleChoiceOptionTranslationDTOResponse?>> AddSingleChoiceOptionTranslation(SingleChoiceOptionTranslationDTOAddRequest? request)
        {
            if (request == null)
            {
                throw new HttpStatusException(HttpStatusCode.BadRequest, "Request can't be null");
            }

            ValidationHelper.ModelValidation(request);

            if (request.SingleChoiceOptionId == null)
            {
                throw new HttpStatusException(HttpStatusCode.BadRequest, "The 'singleChoiceOptionId' parameter is required and must be a valid GUID.");
            }

            var translation = request.ToSingleChoiceOptionTranslation();
            translation.SingleChoiceOptionTranslationId = Guid.NewGuid();

            var result = await _translationsRepository.AddSingleChoiceOptionTranslation(translation);

            return new ApiResponse<SingleChoiceOptionTranslationDTOResponse?>()
            {
                StatusCode = (int)HttpStatusCode.Created,
                Message = "Translation added successfully to the SingleChoiceOption.",
                Data = result.ToSingleChoiceOptionTranslationDTOResponse()
            };
        }
    }
}

