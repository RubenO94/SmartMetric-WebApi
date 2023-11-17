using Microsoft.Extensions.Logging;
using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.Exceptions;
using SmartMetric.Core.ServicesContracts.Getters;
using System.Net;

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

        public async Task<ApiResponse<List<QuestionDTOResponse>>> GetAllQuestions()
        {
            _logger.LogInformation($"{nameof(QuestionGetterService)}.{nameof(GetAllQuestions)} foi iniciado");

            var questions = await _questionRepository.GetAllQuestion();

            return new ApiResponse<List<QuestionDTOResponse>>()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Data retrieved successfully.",
                Data = questions.Select(temp => temp.ToQuestionDTOResponse()).ToList()
            };
        }

        public async Task<ApiResponse<List<QuestionDTOResponse>?>> GetQuestionsByFormTemplateId(Guid? formTemplateId)
        {
            _logger.LogInformation($"{nameof(QuestionGetterService)}.{nameof(GetQuestionsByFormTemplateId)} foi iniciado");

            if (formTemplateId == null)
            {
                throw new HttpStatusException(HttpStatusCode.BadRequest, "The 'formTemplateId' parameter is required and must be a valid GUID.");
            }

            var questions = await _questionRepository.GetQuestionByFormTemplateId(formTemplateId.Value);

            return new ApiResponse<List<QuestionDTOResponse>?>()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Data retrieved successfully.",
                Data = questions?.Select(temp => temp.ToQuestionDTOResponse()).ToList()
            };
        }

        public async Task<ApiResponse<QuestionDTOResponse?>> GetQuestionById(Guid? questionId)
        {
            _logger.LogInformation($"{nameof(QuestionGetterService)}.{nameof(GetQuestionById)} foi iniciado");

            if (questionId == null)
            {
                throw new HttpStatusException(HttpStatusCode.BadRequest, "The 'questionId' parameter is required and must be a valid GUID.");
            }
            Question? question = await _questionRepository.GetQuestionById(questionId.Value);

            if (question == null)
            {
                throw new HttpStatusException(HttpStatusCode.NotFound, "Resource not found. The provided ID does not exist.");
            }

            return new ApiResponse<QuestionDTOResponse?>()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Data retrieved successfully.",
                Data = question.ToQuestionDTOResponse()
            };
        }
    }
}
