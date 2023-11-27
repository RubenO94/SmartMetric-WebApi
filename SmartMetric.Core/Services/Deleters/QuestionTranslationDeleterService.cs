using Microsoft.Extensions.Logging;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.Enums;
using SmartMetric.Core.Exceptions;
using SmartMetric.Core.ServicesContracts.Deleters;
using SmartMetric.Core.ServicesContracts.Getters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.Services.Deleters
{
    public class QuestionTranslationDeleterService : IQuestionTranslationDeleterService
    {
        //VARIABLES
        private readonly IQuestionTranslationRepository _questionTranslationsRepository;
        private readonly IQuestionGetterService _questionGetterService;
        private readonly ILogger<QuestionTranslationDeleterService> _logger;

        //CONSTRUCTOR
        public QuestionTranslationDeleterService(IQuestionTranslationRepository questionTranslationsRepository, IQuestionGetterService questionGetterService, ILogger<QuestionTranslationDeleterService> logger)
        {
            _questionTranslationsRepository = questionTranslationsRepository;
            _questionGetterService = questionGetterService;
            _logger = logger;
        }

        //DELETERS
        public async Task<ApiResponse<bool>> DeleteQuestionTranslationById(Guid? questionId, Language? language)
        {
            _logger.LogInformation($"{nameof(QuestionTranslationDeleterService)}.{nameof(DeleteQuestionTranslationById)} foi iniciado");

            if (questionId == null || language == null || language.ToString() == "") { throw new HttpStatusException(HttpStatusCode.BadRequest, "QuestionId can't be null!"); }

            var questionExist = await _questionGetterService.GetQuestionById(questionId);
                
            if (questionExist == null) throw new HttpStatusException(HttpStatusCode.NotFound, "Question doesn't exist");

            if (questionExist.Data!.Translations == null || questionExist.Data!.Translations.Count < 2)
            {
                throw new HttpStatusException(HttpStatusCode.BadRequest, "Question must have at least one translation, so can't execute your request");
            }

            var translationToBeDeleted = questionExist.Data.Translations.FirstOrDefault(temp => temp.Language == language.ToString()) ?? throw new HttpStatusException(HttpStatusCode.BadRequest, $"Question doesn't have a {language} translation");

            var response = await _questionTranslationsRepository.DeleteQuestionTranslationById(translationToBeDeleted.QuestionTranslationId);
            return new ApiResponse<bool>()
            {
                StatusCode = (int)HttpStatusCode.NoContent,
                Message = "Translation of Question deleted with success!",
                Data = response,
            };
        }
    }
}
