using Microsoft.Extensions.Logging;
using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Core.DTO;
using SmartMetric.Core.ServicesContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.Services
{
    public class QuestionTranslationGetterService : IQuestionTranslationGetterService
    {
        //variables
        private readonly IQuestionTranslationsRepository _translationsRepository;
        private readonly ILogger<QuestionTranslationGetterService> _logger; 

        //constructor
        public QuestionTranslationGetterService(IQuestionTranslationsRepository translationsRepository, ILogger<QuestionTranslationGetterService> logger)
        {
            _translationsRepository = translationsRepository;
            _logger = logger;
        }

        //implementing interface
        #region Getters

        public async Task<List<QuestionTranslationDTOResponse>> GetAllQuestionTranslations()
        {
            _logger.LogInformation($"{nameof(QuestionTranslationGetterService)}.{nameof(GetAllQuestionTranslations)} foi iniciado");

            var translations = await _translationsRepository.GetAllQuestionTranslations();

            return translations.Select(temp => temp.ToQuestionTranslationDTOResponse()).ToList();
        }

        public async Task<QuestionTranslationDTOResponse> GetQuestionTranslationById(Guid? questionTranslationId)
        {
            _logger.LogInformation($"{nameof(QuestionTranslationGetterService)}.{nameof(GetQuestionTranslationById)} foi iniciado");

            if (questionTranslationId == null) { throw new ArgumentNullException(nameof(questionTranslationId)); }

            QuestionTranslation? translation = await _translationsRepository.GetQuestionTranslationsById(questionTranslationId.Value);

            if (translation == null) { return null; }

            return translation.ToQuestionTranslationDTOResponse();
        }

        public async Task<List<QuestionTranslationDTOResponse>?> GetQuestionTranslationByQuestionId(Guid? questionId)
        {
            _logger.LogInformation($"{nameof(QuestionTranslationGetterService)}.{nameof(GetQuestionTranslationByQuestionId)} foi iniciado");

            if (questionId == null) { throw new ArgumentNullException(nameof(questionId)); }

            var translation = await _translationsRepository.GetQuestionTranslationsByQuestionId(questionId.Value);

            return translation.Select(temp => temp.ToQuestionTranslationDTOResponse()).ToList();
        }

        #endregion
    }
}
