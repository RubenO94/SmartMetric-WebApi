using Microsoft.Extensions.Logging;
using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Core.DTO.AddRequest;
using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.Enums;
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
        private readonly IQuestionGetterService _questionGetterService;
        private readonly ILogger<RatingOptionAdderService> _logger;

        public RatingOptionAdderService(IRatingOptionRepository ratingOptionRepository, IQuestionGetterService questionGetterService ILogger<RatingOptionAdderService> logger)
        {
            _ratingOptionRepository = ratingOptionRepository;
            _questionGetterService = questionGetterService;
            _logger = logger;
        }

        public async Task<ApiResponse<RatingOptionDTOResponse?>> AddRatingOption(RatingOptionDTOAddRequest? request)
        {
            _logger.LogInformation($"{nameof(RatingOptionAdderService)}.{nameof(AddRatingOption)} foi iniciado");

            try
            {
                if (request == null)
                {
                    throw new ArgumentNullException("Request can't be null");
                }

                ValidationHelper.ModelValidation(request);
            }
            catch (Exception ex)
            {
                return new ApiResponse<RatingOptionDTOResponse?>()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = ex.Message
                };
            }

            if(request.QuestionId == null)
            {
                return new ApiResponse<RatingOptionDTOResponse?>()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "The 'questionId' parameter is required and must be a valid GUID."
                };
            }

            var question = await _questionGetterService.GetQuestionById(request.QuestionId);

            if(question.Data == null)
            {
                return new ApiResponse<RatingOptionDTOResponse?>()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "The 'questionId' provided does not exist."
                };
            }

            if(question.Data.ResponseType != ResponseType.Rating.ToString())
            {
                return new ApiResponse<RatingOptionDTOResponse?>()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = $"The question provided isn't of type {ResponseType.Rating}"
                };
            }

            var ratingOptionId = Guid.NewGuid();

            foreach (var translationRequest in request.Translations!)
            {
                translationRequest.RatingOptionId = ratingOptionId;

            }

            RatingOption ratingOption = request.ToRatingOption();
            ratingOption.RatingOptionId = ratingOptionId;

            foreach (var translation in ratingOption.Translations!)
            {
                translation.RatingOptionTranslationId = Guid.NewGuid();
            }

            var result = await _ratingOptionRepository.AddRatingOption(ratingOption);

            return new ApiResponse<RatingOptionDTOResponse?>()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "RatingOption added successfully to the Question.",
                Data = result.ToRatingOptionDTOResponse()
            };
        }
    }
}
