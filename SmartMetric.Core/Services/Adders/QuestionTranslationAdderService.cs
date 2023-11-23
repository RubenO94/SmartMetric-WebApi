using Microsoft.Extensions.Logging;
using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Core.DTO.AddRequest;
using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.Exceptions;
using SmartMetric.Core.Helpers;
using SmartMetric.Core.ServicesContracts.Adders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.Services.Adders
{
    public class QuestionTranslationAdderService : IQuestionTranslationAdderService
    {
        private readonly IQuestionTranslationRepository _translationsRepository;
        private readonly ILogger<QuestionTranslationAdderService> _logger;

        public QuestionTranslationAdderService(IQuestionTranslationRepository translationsRepository, ILogger<QuestionTranslationAdderService> logger)
        {
            _translationsRepository = translationsRepository;
            _logger = logger;
        }

        public async Task<ApiResponse<QuestionTranslationDTOResponse?>> AddQuestionTranslation(QuestionTranslationDTOAddRequest? request)
        {
            _logger.LogInformation($"{nameof(QuestionTranslationAdderService)}.{nameof(AddQuestionTranslation)} foi iniciado");

            if (request == null)
            {
                throw new HttpStatusException(HttpStatusCode.BadRequest, "Request can't be null");
            }

            ValidationHelper.ModelValidation(request);

            if (request.QuestionId == null)
            {
                throw new HttpStatusException(HttpStatusCode.BadRequest, "The 'questionId' parameter is required and must be a valid GUID.");
            }

            ValidationHelper.ModelValidation(request);

            var existenceTranslations = await _translationsRepository.GetQuestionTranslationsByQuestionId(request.QuestionId.Value);

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

            return new ApiResponse<QuestionTranslationDTOResponse?>()
            {
                StatusCode = (int)HttpStatusCode.Created,
                Message = "Translation added successfully to the Question.",
                Data = translation.ToQuestionTranslationDTOResponse(),
            };
        }
    }
}
