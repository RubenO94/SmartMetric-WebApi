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
    public class RatingOptionTranslationsAdderService : IRatingOptionTranslationAdderService
    {
        private readonly IRatingOptionTranslationsRepository _translationsRepository;
        private readonly IRatingOptionRepository _ratingOptionRepository;
        private readonly ILogger<RatingOptionTranslationsAdderService> _logger;

        public RatingOptionTranslationsAdderService(IRatingOptionTranslationsRepository translationsRepository, IRatingOptionRepository ratingOptionRepository, ILogger<RatingOptionTranslationsAdderService> logger)
        {
            _translationsRepository = translationsRepository;
            _ratingOptionRepository = ratingOptionRepository;
            _logger = logger;
        }

        public async Task<ApiResponse<RatingOptionTranslationDTOResponse?>> AddRatingOptionTranslation(RatingOptionTranslationDTOAddRequest? request)
        {
            _logger.LogInformation($"{nameof(RatingOptionTranslationsAdderService)}.{nameof(AddRatingOptionTranslation)} foi iniciado");

            if (request == null) throw new ArgumentNullException(nameof(RatingOptionTranslation));

            var ratingOptionExist = await _ratingOptionRepository.GetRatingOptionById(request.RatingOptionId);

            if (ratingOptionExist == null) throw new HttpStatusException(HttpStatusCode.BadRequest, "Resource not found. The provided ID does not exist.");

            var existingTranslations = await _translationsRepository.GetRatingOptionTranslationByRatingOptionId(request.RatingOptionId!.Value);

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

            RatingOptionTranslation translation = request.ToRatingOptionTranslation();

            translation.RatingOptionTranslationId = Guid.NewGuid();

            await _translationsRepository.AddRatingOptionTranslation(translation);

            return new ApiResponse<RatingOptionTranslationDTOResponse?>()
            {
                StatusCode = (int)HttpStatusCode.Created,
                Message = "Translation added successfully to the RatingOption.",
                Data = translation.ToRatingOptionTranslationDTOResponse()
            };
        }
    }
}
