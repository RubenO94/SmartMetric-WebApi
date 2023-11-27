using Microsoft.Extensions.Logging;
using SmartMetric.Core.Domain.Entities;
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
    public class SingleChoiceOptionTranslationDeleterService : ISingleChoiceOptionTranslationDeleterService
    {
        //variables
        private readonly ISingleChoiceOptionTranslationRepository _translationRepository;
        private readonly ISingleChoiceOptionGetterService _singleChoiceOptionGetterService;
        private readonly ILogger<SingleChoiceOptionTranslationDeleterService> _logger;

        //constructor
        public SingleChoiceOptionTranslationDeleterService (ISingleChoiceOptionTranslationRepository translationRepository, ISingleChoiceOptionGetterService singleChoiceOptionGetterService, ILogger<SingleChoiceOptionTranslationDeleterService> logger)
        {
            _translationRepository = translationRepository;
            _singleChoiceOptionGetterService = singleChoiceOptionGetterService;
            _logger = logger;
        }

        //deleters
        public async Task<ApiResponse<bool>> DeleteSingleChoiceOptionTranslationById(Guid? singleChoiceOptionId, Language? language)
        {
            _logger.LogInformation($"{nameof(SingleChoiceOptionTranslationDeleterService)}.{nameof(DeleteSingleChoiceOptionTranslationById)} foi iniciado");

            if (singleChoiceOptionId == null) throw new HttpStatusException(HttpStatusCode.BadRequest, "SingleChoiceOption can't be null!");

            var singleChoiceOptionExist = await _singleChoiceOptionGetterService.GetSingleChoiceOptionById(singleChoiceOptionId);
                
            if (singleChoiceOptionExist == null) throw new HttpStatusException(HttpStatusCode.NotFound, "SingleChoiceOption doesn't exist");

            if (singleChoiceOptionExist.Data!.Translations == null || singleChoiceOptionExist.Data.Translations.Count < 2)
            {
                throw new HttpStatusException(HttpStatusCode.BadRequest, "SingleChoiceOption must have at least one translation, so can't execute your request!");
            }

            var translationToBeDeleted = singleChoiceOptionExist.Data.Translations.FirstOrDefault(temp => temp.Language == language.ToString()) ?? throw new HttpStatusException(HttpStatusCode.BadRequest, $"SingleChoiceOption doesn't have a {language} translation!");

            var response = await _translationRepository.DeleteSingleChoiceOptionTranslationById(translationToBeDeleted.SingleChoiceOptionTranslationId);
            return new ApiResponse<bool>()
            {
                StatusCode = (int)HttpStatusCode.NoContent,
                Message = "Translation of SingleChoiceOption deleted with success!",
                Data = response
            };
        }
    }
}
