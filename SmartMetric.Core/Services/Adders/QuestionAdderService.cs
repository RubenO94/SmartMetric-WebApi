using Microsoft.Extensions.Logging;
using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Core.DTO.AddRequest;
using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.Helpers;
using SmartMetric.Core.ServicesContracts.Adders;

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

        public async Task<QuestionDTOResponse?> AddQuestionToFormTemplate(QuestionDTOAddRequest? request)
        {
            _logger.LogInformation($"{nameof(QuestionAdderService)}.{nameof(AddQuestionToFormTemplate)} foi iniciado");

            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
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


            await _questionRepository.AddQuestion(question);

            return question.ToQuestionDTOResponse();
        }

        public Task<QuestionDTOResponse?> AddQuestionToReview(QuestionDTOAddRequest? request)
        {
            throw new NotImplementedException();
        }
    }
}
