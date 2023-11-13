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
    public class FormTemplateTranslationsAdderService : IFormTemplateTranslationsAdderService //Chamar respectiva Interface e implementar o seu contrato
    {

        private readonly IFormTemplateTranslationsRepository _translationsRepository; // Chamar o seu repositorio
        private readonly ILogger<FormTemplateTranslationsAdderService> _logger; //Chamar o Logger

        //Injectar Dependencias
        public FormTemplateTranslationsAdderService(IFormTemplateTranslationsRepository translationsRepository, ILogger<FormTemplateTranslationsAdderService> logger)
        {
            _translationsRepository = translationsRepository;
            _logger = logger;
        }

        public async Task<FormTemplateTranslationDTOResponse> AddFormTemplateTranslation(FormTemplateTranslationDTOAddRequest? request)
        {
            //1º - Fazer log do Metodo.
            _logger.LogInformation($"{nameof(FormTemplateTranslationsAdderService)}.{nameof(AddFormTemplateTranslation)} foi iniciado");

            //2º - Verifcar se é null, se sim lançar ArgumenteNullException
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            //3º - Validação do Modelo
            ValidationHelper.ModelValidation(request);

            //4º - Converter o Request em Modelo(Entity)
            FormTemplateTranslation translation = request.ToFormTemplateTranslation();

            //5º - Gerar novo Guid para a traduçãao.
            translation.FormTemplateTranslationId = Guid.NewGuid();


            // 6º - Enviar para o Repositorio
            await _translationsRepository.AddFormTemplateTranslation(translation);

            // 7º - Por ultimo, returnar a converção do modelo em DTOResponse
            return translation.ToFormTemplateTranslationDTOResponse();
        }
    }
}
