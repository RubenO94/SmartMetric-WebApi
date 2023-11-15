using Microsoft.Extensions.Logging;
using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Core.DTO.AddRequest;
using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.Helpers;
using SmartMetric.Core.ServicesContracts.Adders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<QuestionDTOResponse?> AddQuestion(QuestionDTOAddRequest? request)
        {
            _logger.LogInformation($"{nameof(QuestionAdderService)}.{nameof(AddQuestion)} foi iniciado");

            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            Guid questionId = Guid.NewGuid();

            var translations = request.Translations?.Select(temp =>
             {
                 temp.QuestionId = questionId;
                 ValidationHelper.ModelValidation(temp);
                 return temp.ToQuestionTranslation();
             });

            
            //TODO Restantes validações...

            Question question = request.ToQuestion();

            question.QuestionId = questionId;



            if (question.SingleChoiceOptions != null && question.SingleChoiceOptions.Any())
            {
                foreach (var sco in question.SingleChoiceOptions)
                {
                    sco.QuestionId = question.QuestionId;
                    sco.SingleChoiceOptionId = Guid.NewGuid();

                    if (sco.Translations != null && sco.Translations.Any())
                    {
                        foreach (var item in sco.Translations)
                        {
                            item.SingleChoiceOptionId = sco.SingleChoiceOptionId;
                            item.SingleChoiceOptionTranslationId = Guid.NewGuid();
                        }
                    }

                }
            }

            if (question.RatingOptions != null && question.RatingOptions.Any())
            {
                foreach (var rto in question.RatingOptions)
                {
                    rto.QuestionId = question.QuestionId;
                    rto.RatingOptionId = Guid.NewGuid();

                    if (rto.Translations != null && rto.Translations.Any())
                    {
                        foreach (var item in rto.Translations)
                        {
                            item.RatingOptionId = rto.RatingOptionId;
                            item.RatingOptionTranslationId = Guid.NewGuid();
                        }
                    }
                }
            }

            await _questionRepository.AddQuestion(question);

            return question.ToQuestionDTOResponse();
        }
    }
}
