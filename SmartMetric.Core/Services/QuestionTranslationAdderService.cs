using Microsoft.Extensions.Logging;
using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Core.DTO;
using SmartMetric.Core.Helpers;
using SmartMetric.Core.ServicesContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.Services
{
    public class QuestionTranslationAdderService : IQuestionTranslationAdderService
    {
        //variables
        private readonly IQuestionTranslationsRepository _translationsRepository;
        private readonly ILogger<QuestionTranslationAdderService> _logger;

        //constructor
        public QuestionTranslationAdderService(IQuestionTranslationsRepository translationsRepository, ILogger<QuestionTranslationAdderService> logger)
        {
            _translationsRepository = translationsRepository;
            _logger = logger;
        }

        //imnplementing interface
        public async Task<QuestionTranslationDTOResponse> AddQuestionTranslation(QuestionTranslationDTOAddRequest? request)
        {
            _logger.LogInformation($"{nameof(QuestionTranslationAdderService)}.{nameof(AddQuestionTranslation)} foi iniciado");

            //2º - Verifcar se é null, se sim lançar ArgumenteNullException
            if (request == null) { throw new ArgumentNullException(nameof(request)); }

            //3º - Validação do Modelo
            ValidationHelper.ModelValidation(request);

            //4º - Converter o Request em Modelo(Entity)
            QuestionTranslation translation = request.ToQuestionTranslation();

            //5º - Gerar novo Guid para a tradução.
            translation.QuestionTranslationId = Guid.NewGuid();

            // 6º - Enviar para o Repositorio
            await _translationsRepository.AddQuestionTranslation(translation);

            // 7º - Por ultimo, returnar a converção do modelo em DTOResponse
            return translation.ToQuestionTranslationDTOResponse();
        }
    }
}
