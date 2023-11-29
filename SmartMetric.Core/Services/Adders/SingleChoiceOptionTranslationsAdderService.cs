using Microsoft.Extensions.Logging;
using SmartMetric.Core.Domain.Entities;
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
        private readonly ISingleChoiceOptionRepository _singleChoiceOptionRepository;
        private readonly ILogger<SingleChoiceOptionTranslationsAdderService> _logger;

        public SingleChoiceOptionTranslationsAdderService(
            ISingleChoiceOptionTranslationRepository translationsRepository, 
            ILogger<SingleChoiceOptionTranslationsAdderService> logger,
            ISingleChoiceOptionRepository singleChoiceOptionRepository)
        {
            _translationsRepository = translationsRepository;
            _logger = logger;
            _singleChoiceOptionRepository = singleChoiceOptionRepository;
        }

        public async Task<ApiResponse<TranslationDTOResponse?>> AddSingleChoiceOptionTranslation(Guid? singleChoiceOptionId, TranslationDTOAddRequest? request)
        {
            if(singleChoiceOptionId == null) throw new ArgumentNullException(nameof(singleChoiceOptionId));

            if (request == null) throw new ArgumentNullException(nameof(request));

            var ratingOptionExist = await _singleChoiceOptionRepository.GetSingleChoiceOptionById(singleChoiceOptionId.Value);

            if (ratingOptionExist == null) throw new HttpStatusException(HttpStatusCode.BadRequest, "Resource not found. The provided ID does not exist.");

            var existingTranslations = await _translationsRepository.GetTranslationsBySingleChoiceOptionId(singleChoiceOptionId!.Value);

            if (existingTranslations.Any())
            {
                foreach (var item in existingTranslations)
                {
                    if (item.Language == request.Language.ToString())
                    {
                        throw new HttpStatusException(HttpStatusCode.BadRequest, "This language already exists in the provided FormTemplate.");
                    }
                }
            }

            SingleChoiceOptionTranslation translation = request.ToSingleChoiceOptionTranslation();
            translation.SingleChoiceOptionId = singleChoiceOptionId;

            await _translationsRepository.AddSingleChoiceOptionTranslation(translation);

            return new ApiResponse<TranslationDTOResponse?>()
            {
                StatusCode = (int)HttpStatusCode.Created,
                Message = "Translation added successfully to the SingleChoiceOption.",
                Data = translation.ToTranslationDTOResponse()
            };
        }
    }
}

