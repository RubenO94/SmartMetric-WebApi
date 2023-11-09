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
    public class TranslationsAdderService : ITranslationsAdderService
    {
        private readonly ITranslationsRepository _translationsRepository;
        private readonly ILogger<TranslationsAdderService> _logger;

        public TranslationsAdderService(ITranslationsRepository translationsRepository, ILogger<TranslationsAdderService> logger)
        {
            _translationsRepository = translationsRepository;
            _logger = logger;
        }

        public async Task<FormTemplateTranslationDTOResponse> AddFormTemplateTranslation(FormTemplateTranslationDTOAddRequest? request)
        {
            _logger.LogInformation("TranslationsAdderService.AddFormTemplateTranslation foi iniciado");

            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            //Validação do Modelo
            ValidationHelper.ModelValidation(request);

            FormTemplateTranslation translation = request.ToFormTemplateTranslation();

            //Gerar novo Guid para a traduçãao.
            translation.FormTemplateTranslationId = Guid.NewGuid();

            await _translationsRepository.AddFormTemplateTranslation(translation);

            return translation.ToFormTemplateTranslationDTOResponse();
        }
    }
}
