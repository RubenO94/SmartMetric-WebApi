﻿using Microsoft.Extensions.Logging;
using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.Exceptions;
using SmartMetric.Core.ServicesContracts.SingleChoiceOptions;
using SmartMetric.Core.ServicesContracts.SingleChoiceOptionTranslations;
using System.Net;

namespace SmartMetric.Core.Services.SingleChoiceOptionTranslations
{
    public class SingleChoiceOptionTranslationsGetterService : ISingleChoiceOptionTranslationsGetterService
    {
        private readonly ISingleChoiceOptionTranslationRepository _translationsRepository;
        private readonly ISingleChoiceOptionGetterService _singleChoiceOptionGetterService;
        private readonly ILogger<SingleChoiceOptionTranslationsGetterService> _logger;

        public SingleChoiceOptionTranslationsGetterService(ISingleChoiceOptionTranslationRepository translationsRepository, ISingleChoiceOptionGetterService singleChoiceOptionGetterService, ILogger<SingleChoiceOptionTranslationsGetterService> logger)
        {
            _translationsRepository = translationsRepository;
            _singleChoiceOptionGetterService = singleChoiceOptionGetterService;
            _logger = logger;
        }

        public async Task<ApiResponse<List<TranslationDTOResponse>>> GetAllSingleChoiceOptionTranslations()
        {
            _logger.LogInformation($"{nameof(SingleChoiceOptionTranslationsGetterService)}.{nameof(GetAllSingleChoiceOptionTranslations)} foi iniciado");

            var translations = await _translationsRepository.GetAllSingleChoiceOptionTranslations();
            return new ApiResponse<List<TranslationDTOResponse>>()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Get of all Translations of all SingleChoiceOption was made with success!",
                Data = translations.Select(temp => temp.ToTranslationDTOResponse()).ToList(),
            };
        }

        public async Task<ApiResponse<TranslationDTOResponse?>> GetSingleChoiceOptionTranslationById(Guid? singleChoiceOptionTranslationId)
        {
            _logger.LogInformation($"{nameof(SingleChoiceOptionTranslationsGetterService)}.{nameof(GetSingleChoiceOptionTranslationById)} foi iniciado");

            if (singleChoiceOptionTranslationId == null)
            {
                throw new HttpStatusException(HttpStatusCode.BadRequest, "SingleChoiceOptionTranslationId can't be null");
            }

            SingleChoiceOptionTranslation? translation = await _translationsRepository.GetSingleChoiceOptionTranslationById(singleChoiceOptionTranslationId.Value);

            if (translation == null)
            {
                throw new HttpStatusException(HttpStatusCode.NotFound, "A Translation with this Id doesn't exist");
            }

            return new ApiResponse<TranslationDTOResponse?>()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Get of the Translation request was made with success!",
                Data = translation.ToTranslationDTOResponse()
            };
        }

        public async Task<ApiResponse<List<TranslationDTOResponse>?>> GetTranslationsBySingleChoiceOptionId(Guid? singleChoiceOptionId)
        {
            _logger.LogInformation($"{nameof(SingleChoiceOptionTranslationsGetterService)}.{nameof(GetTranslationsBySingleChoiceOptionId)} foi iniciado");

            if (singleChoiceOptionId == null)
            {
                throw new HttpStatusException(HttpStatusCode.BadRequest, "SingleChoiceOptionId can't be null");
            }

            var singleChoiceOptionExist = await _singleChoiceOptionGetterService.GetSingleChoiceOptionById(singleChoiceOptionId.Value) ?? throw new HttpStatusException(HttpStatusCode.NotFound, "SingleChoiceOption doesn't exist");

            var translations = await _translationsRepository.GetTranslationsBySingleChoiceOptionId(singleChoiceOptionExist.Data!.SingleChoiceOptionId);
            return new ApiResponse<List<TranslationDTOResponse>?>()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Get of the Translations of the SingleChoiceOption requested, was made with success!",
                Data = translations.Select(temp => temp.ToTranslationDTOResponse()).ToList()
            };
        }
    }
}
