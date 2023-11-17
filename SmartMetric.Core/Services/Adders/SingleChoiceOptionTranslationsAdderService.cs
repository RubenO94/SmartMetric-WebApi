using Microsoft.Extensions.Logging;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Core.DTO.AddRequest;
using SmartMetric.Core.DTO.Response;
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
    public class SingleChoiceOptionTranslationsAdderService : ISingleChoiceOptionTranslationsAdderService
    {
        private readonly ISingleChoiceOptionTranslationsRepository _translationsRepository;
        private readonly ILogger<SingleChoiceOptionTranslationsAdderService> _logger;

        public SingleChoiceOptionTranslationsAdderService(ISingleChoiceOptionTranslationsRepository translationsRepository, ILogger<SingleChoiceOptionTranslationsAdderService> logger)
        {
            _translationsRepository = translationsRepository;
            _logger = logger;
        }

        public async Task<ApiResponse<SingleChoiceOptionTranslationDTOResponse?>> AddSingleChoiceOptionTranslation(SingleChoiceOptionTranslationDTOAddRequest? request)
        {
            try
            {
                if (request == null)
                {
                    throw new ArgumentNullException("Request can't be null");
                }

                ValidationHelper.ModelValidation(request);
            }
            catch (Exception ex)
            {
                return new ApiResponse<SingleChoiceOptionTranslationDTOResponse?>()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = ex.Message
                };
            }

            if (request.SingleChoiceOptionId == null)
            {
                return new ApiResponse<SingleChoiceOptionTranslationDTOResponse?>()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "The 'singleChoiceOptionId' parameter is required and must be a valid GUID."
                };
            }

            var translation = request.ToSingleChoiceOptionTranslation();
            translation.SingleChoiceOptionTranslationId = Guid.NewGuid();

            var result = await _translationsRepository.AddSingleChoiceOptionTranslation(translation);

            return new ApiResponse<SingleChoiceOptionTranslationDTOResponse?>()
            {
                StatusCode = (int)HttpStatusCode.Created,
                Message = "Translation added successfully to the SingleChoiceOption.",
                Data = result.ToSingleChoiceOptionTranslationDTOResponse()
            };
        }
    }
}

