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

        public async Task<QuestionDTOResponse> AddQuestion(QuestionDTOAddRequest? request)
        {
            _logger.LogInformation($"{nameof(QuestionAdderService)}.{nameof(AddQuestion)} foi iniciado");

            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            ValidationHelper.ModelValidation(request);

            Question question = request.ToQuestion();

            question.QuestionId = Guid.NewGuid();

            await _questionRepository.AddQuestion(question);

            return question.ToQuestionDTOResponse();
        }
    }
}
