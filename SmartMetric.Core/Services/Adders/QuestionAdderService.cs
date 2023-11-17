﻿using Microsoft.Extensions.Logging;
using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Core.DTO.AddRequest;
using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.Exceptions;
using SmartMetric.Core.Helpers;
using SmartMetric.Core.ServicesContracts.Adders;
using System.Net;

namespace SmartMetric.Core.Services.Adders
{
    public class QuestionAdderService : IQuestionAdderService
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly ILogger<QuestionAdderService> _logger;

        public QuestionAdderService(IQuestionRepository questionRepository, ILogger<QuestionAdderService> logger)
        {
            _questionRepository = questionRepository;
            _logger = logger;
        }

        public async Task<ApiResponse<QuestionDTOResponse?>> AddQuestionToFormTemplate(QuestionDTOAddRequest? request)
        {
            _logger.LogInformation($"{nameof(QuestionAdderService)}.{nameof(AddQuestionToFormTemplate)} foi iniciado");

            if (request == null)
            {
                throw new HttpStatusException(HttpStatusCode.BadRequest, "Request can't be null.");
            }

            if (request.FormTemplateId == null)
            {
                throw new HttpStatusException(HttpStatusCode.BadRequest, "The question does not have a FormTemplateId to associate.");
            }

            ValidationHelper.ModelValidation(request);


            if (request.ReviewId != null)
            {
                request.ReviewId = null;
            }

           

            ValidationHelper.ModelValidation(request);

            Guid questionId = Guid.NewGuid();

            foreach (var translation in request.Translations!)
            {
                translation.QuestionId = questionId;
            }

            if (request.SingleChoiceOptions != null && request.SingleChoiceOptions.Any())
            {
                foreach (var sco in request.SingleChoiceOptions)
                {
                    sco.QuestionId = questionId;
                }
            }

            if (request.RatingOptions != null && request.RatingOptions.Any())
            {
                foreach (var rto in request.RatingOptions)
                {
                    rto.QuestionId = questionId;
                }
            }


            Question question = request.ToQuestion();
            question.QuestionId = questionId;

            foreach (var translation in question.Translations!)
            {
                translation.QuestionTranslationId = Guid.NewGuid();
            }

            if (question.SingleChoiceOptions != null && question.SingleChoiceOptions.Any())
            {
                foreach (var sco in question.SingleChoiceOptions)
                {
                    sco.SingleChoiceOptionId = Guid.NewGuid();

                    foreach (var scoTranslation in sco.Translations!)
                    {
                        scoTranslation.SingleChoiceOptionId = sco.SingleChoiceOptionId;
                        scoTranslation.SingleChoiceOptionTranslationId = Guid.NewGuid();
                    }
                }
            }

            if (question.RatingOptions != null && question.RatingOptions.Any())
            {
                foreach (var rto in question.RatingOptions)
                {
                    rto.RatingOptionId = Guid.NewGuid();

                    foreach (var rtoTranslation in rto.Translations!)
                    {
                        rtoTranslation.RatingOptionId = rto.RatingOptionId;
                        rtoTranslation.RatingOptionTranslationId = Guid.NewGuid();
                    }
                }
            }


           var result =  await _questionRepository.AddQuestion(question);

            return new ApiResponse<QuestionDTOResponse?>()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Question added successfully to the FormTemplate.",
                Data = result.ToQuestionDTOResponse()
            };
        }

        public Task<ApiResponse<QuestionDTOResponse?>> AddQuestionToReview(QuestionDTOAddRequest? request)
        {
            throw new NotImplementedException();
        }
    }
}
