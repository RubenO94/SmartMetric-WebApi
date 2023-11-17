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
        private readonly ILogger<RatingOptionTranslationsAdderService> _logger;

        public RatingOptionTranslationsAdderService(IRatingOptionTranslationsRepository translationsRepository, ILogger<RatingOptionTranslationsAdderService> logger)
        {
            _translationsRepository = translationsRepository;
            _logger = logger;
        }

        public async Task<ApiResponse<RatingOptionTranslationDTOResponse?>> AddRatingOptionTranslation(RatingOptionTranslationDTOAddRequest? request)
        {
            _logger.LogInformation($"{nameof(RatingOptionTranslationsAdderService)}.{nameof(AddRatingOptionTranslation)} foi iniciado");

            if (request == null)
            {
                throw new HttpStatusException(HttpStatusCode.BadRequest, "Request can't be null");
            }

            ValidationHelper.ModelValidation(request);

            if (request.RatingOptionId == null)
            {
                throw new HttpStatusException(HttpStatusCode.BadRequest, "The 'ratingOptionId' parameter is required and must be a valid GUID.");
            }

            RatingOptionTranslation translation = request.ToRatingOptionTranslation();

            translation.RatingOptionTranslationId = Guid.NewGuid();

            var result = await _translationsRepository.AddRatingOptionTranslation(translation);

            return new ApiResponse<RatingOptionTranslationDTOResponse?>()
            {
                StatusCode = (int)HttpStatusCode.Created,
                Message = "Translation added successfully to the RatingOption.",
                Data = result.ToRatingOptionTranslationDTOResponse()
            };
        }
    }
}
