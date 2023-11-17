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
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.Services.Adders
{
    public class FormTemplateTranslationsAdderService : IFormTemplateTranslationsAdderService 
    { 

        private readonly IFormTemplateTranslationsRepository _translationsRepository;
        private readonly ILogger<FormTemplateTranslationsAdderService> _logger;

        public FormTemplateTranslationsAdderService(IFormTemplateTranslationsRepository translationsRepository, ILogger<FormTemplateTranslationsAdderService> logger)
        {
            _translationsRepository = translationsRepository;
            _logger = logger;
        }

        public async Task<ApiResponse<FormTemplateTranslationDTOResponse?>> AddFormTemplateTranslation(FormTemplateTranslationDTOAddRequest? request)
        {
            _logger.LogInformation($"{nameof(FormTemplateTranslationsAdderService)}.{nameof(AddFormTemplateTranslation)} foi iniciado");

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
                return new ApiResponse<FormTemplateTranslationDTOResponse?>()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = ex.Message
                };
            }

            if (request.FormTemplateId == null)
            {
                return new ApiResponse<FormTemplateTranslationDTOResponse?>()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "The 'formTemplateId' parameter is required and must be a valid GUID."
                };
            }

            ValidationHelper.ModelValidation(request);

            var existenceTranslations =  await _translationsRepository.GetTranslationsByFormTemplateId(request.FormTemplateId.Value);

            if (existenceTranslations.Any())
            {
                foreach (var item in existenceTranslations)
                {
                    if (item.Language == request.Language.ToString())
                    {
                        return new ApiResponse<FormTemplateTranslationDTOResponse?>()
                        {
                            StatusCode = (int)HttpStatusCode.BadRequest,
                            Message = "This language already exists in the provided FormTemplate."
                        };
                    }
                }
            }
            
            FormTemplateTranslation translation = request.ToFormTemplateTranslation();

            translation.FormTemplateTranslationId = Guid.NewGuid();

            await _translationsRepository.AddFormTemplateTranslation(translation);

            return new ApiResponse<FormTemplateTranslationDTOResponse?>()
            {
                StatusCode = (int)HttpStatusCode.Created,
                Message = "Translation added successfully to the FormTemplate.",
                Data = translation.ToFormTemplateTranslationDTOResponse(),
            };
        }
    }
}
