﻿using Microsoft.Extensions.Logging;
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
        private readonly IQuestionGetterService _questionGetterService;
        private readonly ILogger<RatingOptionAdderService> _logger;

        public RatingOptionAdderService(IRatingOptionRepository ratingOptionRepository, IQuestionGetterService questionGetterService, ILogger<RatingOptionAdderService> logger)
        {
            _ratingOptionRepository = ratingOptionRepository;
            _questionGetterService = questionGetterService;
            _logger = logger;
        }

        public async Task<ApiResponse<RatingOptionDTOResponse?>> AddRatingOption(RatingOptionDTOAddRequest? request)
        {
            _logger.LogInformation($"{nameof(RatingOptionAdderService)}.{nameof(AddRatingOption)} foi iniciado");

            if (request == null)
            {
                throw new HttpStatusException(HttpStatusCode.BadRequest, "Request can't be null.");
            }

            ValidationHelper.ModelValidation(request);

            if (request.QuestionId == null)
            {
                throw new HttpStatusException(HttpStatusCode.BadRequest, "The 'questionId' parameter is required and must be a valid GUID.");
            }

            var question = await _questionGetterService.GetQuestionById(request.QuestionId);

            if (question.Data == null)
            {
                throw new HttpStatusException(HttpStatusCode.NotFound, "The 'questionId' provided does not exist.");
            }

            if (question.Data.ResponseType != ResponseType.Rating.ToString())
            {
                throw new HttpStatusException(HttpStatusCode.BadRequest, $"The question provided isn't of type {ResponseType.Rating}");
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
