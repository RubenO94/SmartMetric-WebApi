using Microsoft.Extensions.Logging;
using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Core.DTO.AddRequest;
using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.Enums;
using SmartMetric.Core.Exceptions;
using SmartMetric.Core.Helpers;
using SmartMetric.Core.ServicesContracts.Adders;
using SmartMetric.Core.ServicesContracts.Getters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.Services.Adders
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

        public async Task<ApiResponse<RatingOptionDTOResponse?>> AddRatingOption(RatingOptionDTOAddRequest? request)
        {
            _logger.LogInformation($"{nameof(RatingOptionAdderService)}.{nameof(AddRatingOption)} foi iniciado");

            if (request == null) throw new ArgumentNullException(nameof(RatingOption));

            ValidationHelper.ModelValidation(request);

            //if (request.QuestionId == null) throw new HttpStatusException(HttpStatusCode.BadRequest, "The 'questionId' parameter is required and must be a valid GUID.");

            //if (request.NumericValue == 0) throw new HttpStatusException(HttpStatusCode.BadRequest, "The 'numericValue' parameter is required and must be a valid number.");

            //if (request.Translations == null || request.Translations.Count == 0) throw new HttpStatusException(HttpStatusCode.BadRequest, "The 'translation' parameter is required and must have at least one translation.");

            //foreach(var item in request.Translations) 
            //{
            //    if (item.Language == null || item.Description == null || item.Description == "") throw new HttpStatusException(HttpStatusCode.BadRequest, "The 'translation' parameter needs to have valid language and description parameters.");
            //}

            var question = await _questionRepository.GetQuestionById(request.QuestionId);

            if (question == null)
            {
                throw new HttpStatusException(HttpStatusCode.BadRequest, "The 'questionId' provided does not exist.");
            }

            if (question.ResponseType != ResponseType.Rating.ToString())
            {
                throw new HttpStatusException(HttpStatusCode.BadRequest, $"The question provided isn't of type {ResponseType.Rating}");
            }

            var ratingOptionId = Guid.NewGuid();
            RatingOption ratingOption = request.ToRatingOption();
            ratingOption.RatingOptionId = ratingOptionId;

            foreach (var translation in ratingOption.Translations!)
            {
                translation.RatingOptionTranslationId = Guid.NewGuid();
                translation.RatingOptionId = ratingOptionId;
            }

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
