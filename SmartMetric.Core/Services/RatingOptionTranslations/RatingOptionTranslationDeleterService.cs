using Microsoft.Extensions.Logging;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.Enums;
using SmartMetric.Core.Exceptions;
using SmartMetric.Core.ServicesContracts.RatingOptionTranslations;
using System.Net;

namespace SmartMetric.Core.Services.RatingOptionTranslations
{
    public class RatingOptionTranslationDeleterService : IRatingOptionTranslationDeleterService 
    {
        //variables
        private readonly IRatingOptionTranslationsRepository _ratingOptionTranslationsRepository;
        private readonly IRatingOptionRepository _ratingOptionRepository;
        private readonly ILogger<RatingOptionTranslationDeleterService> _logger;

        //constructor
        public RatingOptionTranslationDeleterService (IRatingOptionTranslationsRepository ratingOptionTranslationsRepository, IRatingOptionRepository ratingOptionRepository, ILogger<RatingOptionTranslationDeleterService> logger)
        {
            _ratingOptionTranslationsRepository = ratingOptionTranslationsRepository;
            _ratingOptionRepository = ratingOptionRepository;
            _logger = logger;
        }

        //deleters
        public async Task<ApiResponse<bool>> DeleteRatingOptionTranslationById(Guid? ratingOptionId, Language? language)
        {
            _logger.LogInformation($"{nameof(RatingOptionTranslationDeleterService)}.{nameof(DeleteRatingOptionTranslationById)} foi iniciado");

            if (ratingOptionId == null) throw new HttpStatusException(HttpStatusCode.BadRequest, "RatingOptionId can't be null!");

            var ratingOptionExist = await _ratingOptionRepository.GetRatingOptionById(ratingOptionId);

            if (ratingOptionExist == null) throw new HttpStatusException(HttpStatusCode.BadRequest, "Resource not found. The provided ID doesn't exist.");

            if (ratingOptionExist.Translations == null || ratingOptionExist.Translations.Count < 2)
            {
                throw new HttpStatusException(HttpStatusCode.BadRequest, "RatingOption must have at least one translation, so can't execute your request!");
            }

            var translationToBeDeleted = ratingOptionExist.Translations.FirstOrDefault(temp => temp.Language == language.ToString()) ?? throw new HttpStatusException(HttpStatusCode.BadRequest, $"RatingOption doesn't have a {language} translation!");

            var response = await _ratingOptionTranslationsRepository.DeleteRatingOptionTranslationById(translationToBeDeleted.RatingOptionTranslationId);
            return new ApiResponse<bool>()
            {
                StatusCode = (int)HttpStatusCode.NoContent,
                Message = "Translation of RatingOption deleted with success!",
                Data = response
            };
        }
    }
}
