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
    public class RatingOptionGetterService : IRatingOptionGetterService
    {
        private readonly IRatingOptionRepository _ratingOptionRepository;
        private readonly ILogger<RatingOptionGetterService> _logger;

        public RatingOptionGetterService(IRatingOptionRepository ratingOptionRepository, ILogger<RatingOptionGetterService> logger)
        {
            _ratingOptionRepository = ratingOptionRepository;
            _logger = logger;
        }

        #region Getters

        public async Task<List<RatingOptionDTOResponse>> GetAllRatingOption()
        {
            _logger.LogInformation($"{nameof(RatingOptionGetterService)}.{nameof(GetAllRatingOption)} foi iniciado");
            var ratingOption = await _ratingOptionRepository.GetAllRatingOption();

            return ratingOption.Select(temp => temp.ToRatingOptionDTOResponse()).ToList();
        }

        public async Task<RatingOptionDTOResponse?> GetRatingOptionById(Guid? ratingOptionId)
        {
            _logger.LogInformation($"{nameof(RatingOptionGetterService)}.{nameof(GetRatingOptionById)} foi iniciado");

            if (ratingOptionId == null) { throw new ArgumentNullException(nameof(ratingOptionId)); }

            RatingOption? ratingOption = await _ratingOptionRepository.GetRatingOptionById(ratingOptionId.Value);

            if (ratingOption == null) { return null; }

            return ratingOption.ToRatingOptionDTOResponse();
        }

        public async Task<List<RatingOptionDTOResponse>?> GetRatingOptionByQuestionId(Guid? questionId)
        {
            _logger.LogInformation($"{nameof(RatingOptionGetterService)}.{nameof(GetRatingOptionByQuestionId)} foi iniciado");

            if (questionId == null) { throw new ArgumentNullException(nameof(questionId)); }

            var ratingOption = await _ratingOptionRepository.GetRatingOptionByQuestionId(questionId.Value);

            return ratingOption.Select(temp => temp.ToRatingOptionDTOResponse()).ToList();
        }

        #endregion
    }
}
