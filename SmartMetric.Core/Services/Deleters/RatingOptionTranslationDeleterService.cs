using Microsoft.Extensions.Logging;
using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.Enums;
using SmartMetric.Core.Exceptions;
using SmartMetric.Core.ServicesContracts.Deleters;
using SmartMetric.Core.ServicesContracts.Getters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.Services.Deleters
{
    public class RatingOptionTranslationDeleterService : IRatingOptionTranslationDeleterService
    {
        //variables
        private readonly IRatingOptionTranslationsRepository _ratingOptionTranslationsRepository;
        private readonly IRatingOptionGetterService _ratingOptionGetterService;
        private readonly ILogger<RatingOptionTranslationDeleterService> _logger;

        //constructor
        public RatingOptionTranslationDeleterService (IRatingOptionTranslationsRepository ratingOptionTranslationsRepository, IRatingOptionGetterService ratingOptionGetterService, ILogger<RatingOptionTranslationDeleterService> logger)
        {
            _ratingOptionTranslationsRepository = ratingOptionTranslationsRepository;
            _ratingOptionGetterService = ratingOptionGetterService;
            _logger = logger;
        }

        //deleters
        public async Task<ApiResponse<bool>> DeleteRatingOptionTranslationById(Guid? ratingOptionId, Language? language)
        {
            _logger.LogInformation($"{nameof(RatingOptionTranslationDeleterService)}.{nameof(DeleteRatingOptionTranslationById)} foi iniciado");

            if (ratingOptionId == null) throw new HttpStatusException(HttpStatusCode.BadRequest, "RatingOption can't be null!");

            var ratingOptionExist = await _ratingOptionGetterService.GetRatingOptionById(ratingOptionId) ?? throw new HttpStatusException(HttpStatusCode.NotFound, "RatingOption doesn't exist!");

            if (ratingOptionExist.Data!.Translations == null || ratingOptionExist.Data.Translations.Count < 2)
            {
                throw new HttpStatusException(HttpStatusCode.BadRequest, "RatingOption must have at least one translation, so can't execute your request!");
            }

            var translationToBeDeleted = ratingOptionExist.Data.Translations.FirstOrDefault(temp => temp.Language == language.ToString()) ?? throw new HttpStatusException(HttpStatusCode.BadRequest, $"RatingOption doesn't have a {language} translation!");
            await _ratingOptionTranslationsRepository.DeleteRatingOptionTranslationById(translationToBeDeleted.RatingOptionTranslationId);
            return new ApiResponse<bool>()
            {
                StatusCode = (int)HttpStatusCode.NoContent,
                Message = "Translation of RatingOption deleted with success!",
                Data = true
            };
        }
    }
}
