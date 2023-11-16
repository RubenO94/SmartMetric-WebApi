using Microsoft.Extensions.Logging;
using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.ServicesContracts.Getters;

namespace SmartMetric.Core.Services.Getters
{
    public class QuestionGetterService : IQuestionGetterService
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly ILogger<QuestionGetterService> _logger;

        public QuestionGetterService(IQuestionRepository questionRepository, ILogger<QuestionGetterService> logger)
        {
            _questionRepository = questionRepository;
            _logger = logger;
        }

        public async Task<List<QuestionDTOResponse>> GetAllQuestion()
        {
            _logger.LogInformation($"{nameof(QuestionGetterService)}.{nameof(GetAllQuestion)} foi iniciado");

            var question = await _questionRepository.GetAllQuestion();
            return question.Select(temp => temp.ToQuestionDTOResponse()).ToList();
        }

        public async Task<List<QuestionDTOResponse>?> GetQuestionByFormTemplateId(Guid? formTemplateId)
        {
            _logger.LogInformation($"{nameof(QuestionGetterService)}.{nameof(GetQuestionByFormTemplateId)} foi iniciado");

            if (formTemplateId == null) { throw new ArgumentNullException(nameof(formTemplateId)); }
            var question = await _questionRepository.GetQuestionByFormTemplateId(formTemplateId.Value);
            return question.Select(temp => temp.ToQuestionDTOResponse()).ToList();
        }

        public async Task<QuestionDTOResponse?> GetQuestionById(Guid? questionId)
        {
            _logger.LogInformation($"{nameof(QuestionGetterService)}.{nameof(GetQuestionById)} foi iniciado");

            if (questionId == null) { throw new ArgumentNullException(nameof(questionId)); }
            Question? question = await _questionRepository.GetQuestionById(questionId.Value);
            if (question == null) { return null; }
            return question.ToQuestionDTOResponse();
        }
    }
}
