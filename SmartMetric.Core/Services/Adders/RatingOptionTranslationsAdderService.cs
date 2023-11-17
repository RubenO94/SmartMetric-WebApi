﻿using Microsoft.Extensions.Logging;
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
    public class RatingOptionTranslationsAdderService : IRatingOptionTranslationAdderService
    {
        //Variables
        private readonly IRatingOptionTranslationsRepository _translationsRepository;
        private readonly ILogger<RatingOptionTranslationsAdderService> _logger;

        //Constructor
        public RatingOptionTranslationsAdderService(IRatingOptionTranslationsRepository translationsRepository, ILogger<RatingOptionTranslationsAdderService> logger)
        {
            _translationsRepository = translationsRepository;
            _logger = logger;
        }

        //Implementing Interface
        public async Task<ApiResponse<RatingOptionTranslationDTOResponse?>> AddRatingOptionTranslation(RatingOptionTranslationDTOAddRequest? request)
        {
            //1º - Fazer log do Metodo.
            _logger.LogInformation($"{nameof(RatingOptionTranslationsAdderService)}.{nameof(AddRatingOptionTranslation)} foi iniciado");

            //2º - Verifcar se é null, se sim lançar ArgumenteNullException
            if (request == null) { throw new ArgumentNullException(nameof(request)); }

            //3º - Validação do Modelo
            ValidationHelper.ModelValidation(request);

            //4º - Converter o Request em Modelo(Entity)
            RatingOptionTranslation translation = request.ToRatingOptionTranslation();

            //5º - Gerar novo Guid para a tradução.
            translation.RatingOptionTranslationId = Guid.NewGuid();

            // 6º - Enviar para o Repositorio
            await _translationsRepository.AddRatingOptionTranslation(translation);

            // 7º - Por ultimo, returnar a converção do modelo em DTOResponse
            return translation.ToRatingOptionTranslationDTOResponse();
        }
    }
}
