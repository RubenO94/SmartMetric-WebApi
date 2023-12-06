using Microsoft.Extensions.Logging;
using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Core.DTO.AddRequest;
using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.Enums;
using SmartMetric.Core.Exceptions;
using SmartMetric.Core.ServicesContracts.RatingOptions;
using System.Net;

namespace SmartMetric.Core.Services.RatingOptions
{
    public class RatingOptionAdderService : IRatingOptionAdderService
    {
        private readonly IRatingOptionRepository _ratingOptionRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly ILogger<RatingOptionAdderService> _logger;

        public RatingOptionAdderService(IRatingOptionRepository ratingOptionRepository, IQuestionRepository questionRepository, ILogger<RatingOptionAdderService> logger)
        {
            _ratingOptionRepository = ratingOptionRepository;
            _questionRepository = questionRepository;
            _logger = logger;
        }

        public async Task<ApiResponse<RatingOptionDTOResponse?>> AddRatingOption(Guid? questionId, RatingOptionDTOAddRequest? request)
        {
            _logger.LogInformation($"{nameof(RatingOptionAdderService)}.{nameof(AddRatingOption)} foi iniciado");

            if(questionId  == null) throw new ArgumentNullException(nameof(questionId));
            if (request == null) throw new ArgumentNullException(nameof(RatingOption));

            var question = await _questionRepository.GetQuestionById(questionId.Value);

            if (question == null) throw new HttpStatusException(HttpStatusCode.BadRequest, "The 'questionId' provided does not exist.");

            if (question.ResponseType != ResponseType.Rating.ToString())
            {
                throw new HttpStatusException(HttpStatusCode.BadRequest, $"The question provided isn't of type {ResponseType.Rating}");
            }

            var ratingOptionId = Guid.NewGuid();
            RatingOption ratingOption = request.ToRatingOption();
            ratingOption.RatingOptionId = ratingOptionId;

            //foreach (var translation in ratingOption.Translations!)
            //{
            //    translation.RatingOptionTranslationId = Guid.NewGuid();
            //    translation.RatingOptionId = ratingOptionId;
            //}

            await _ratingOptionRepository.AddRatingOption(ratingOption);

            return new ApiResponse<RatingOptionDTOResponse?>()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "RatingOption added successfully to the Question.",
                Data = ratingOption.ToRatingOptionDTOResponse()
            };
        }
    }
}
