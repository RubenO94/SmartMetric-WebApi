using Microsoft.Extensions.Logging;
using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Core.DTO.AddRequest;
using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.Exceptions;
using SmartMetric.Core.ServicesContracts.QuestionTranslations;
using System.Net;

namespace SmartMetric.Core.Services.QuestionTranslations
{
    public class QuestionTranslationAdderService : IQuestionTranslationAdderService
    {
        private readonly IQuestionTranslationRepository _translationsRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly ILogger<QuestionTranslationAdderService> _logger;

        public QuestionTranslationAdderService(IQuestionTranslationRepository translationsRepository, ILogger<QuestionTranslationAdderService> logger, IQuestionRepository questionRepository)
        {
            _translationsRepository = translationsRepository;
            _logger = logger;
            _questionRepository = questionRepository;
        }

        public async Task<ApiResponse<TranslationDTOResponse?>> AddQuestionTranslation(Guid? questionId, TranslationDTOAddRequest? request)
        {
            _logger.LogInformation($"{nameof(QuestionTranslationAdderService)}.{nameof(AddQuestionTranslation)} foi iniciado");

            if(questionId == null) throw new ArgumentNullException(nameof(questionId));

            if (request == null) throw new HttpStatusException(HttpStatusCode.BadRequest, "Request can't be null");

            var questionExist = await _questionRepository.GetQuestionById(questionId.Value);

            if (questionExist == null) throw new HttpStatusException(HttpStatusCode.BadRequest, "Resource not found. The provided ID does not exist.");

            var existenceTranslations = await _translationsRepository.GetQuestionTranslationsByQuestionId(questionId!.Value);

            if (existenceTranslations.Any())
            {
                foreach (var item in existenceTranslations)
                {
                    if (item.Language == request.Language.ToString())
                    {
                        throw new HttpStatusException(HttpStatusCode.BadRequest, "This language already exists in the provided Question.");
                    }
                }
            }

            QuestionTranslation translation = request.ToQuestionTranslation();

            translation.QuestionTranslationId = Guid.NewGuid();

            await _translationsRepository.AddQuestionTranslation(translation);

            return new ApiResponse<TranslationDTOResponse?>()
            {
                StatusCode = (int)HttpStatusCode.Created,
                Message = "Translation added successfully to the Question.",
                Data = translation.ToTranslationDTOResponse(),
            };
        }
    }
}
