using Microsoft.Extensions.Logging;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.Enums;
using SmartMetric.Core.Exceptions;
using SmartMetric.Core.ServicesContracts.FormTemplateTranslations;
using System.Net;

namespace SmartMetric.Core.Services.FormTemplateTranslations
{
    public class FormTemplateTranslationDeleterService : IFormTemplateTranslationsDeleterService
    {
        //VARIABLES
        private readonly IFormTemplateTranslationRepository _formTemplateTranslationsRepository;
        private readonly IFormTemplateRepository _formTemplatesRepository;
        private readonly ILogger<FormTemplateTranslationDeleterService> _logger;

        //CONSTRUCTOR
        public FormTemplateTranslationDeleterService (IFormTemplateTranslationRepository formTemplateTranslationsRepository, IFormTemplateRepository formTemplatesRepository, ILogger<FormTemplateTranslationDeleterService> logger)
        {
            _formTemplateTranslationsRepository = formTemplateTranslationsRepository;
            _formTemplatesRepository = formTemplatesRepository;
            _logger = logger;
        }

        //DELETERS
        public async Task<ApiResponse<bool>> DeleteFormTemplateTranslationById(Guid? formTemplateId, Language? language)
        {
            _logger.LogInformation($"{nameof(FormTemplateTranslationDeleterService)}.{nameof(DeleteFormTemplateTranslationById)} foi iniciado");

            if (formTemplateId == null) throw new HttpStatusException(HttpStatusCode.BadRequest, "FormTemplateId can't be null!");

            var formTemplateExist = await _formTemplatesRepository.GetFormTemplateById(formTemplateId);
            
            if (formTemplateExist == null) throw new HttpStatusException(HttpStatusCode.NotFound, "Resource not found. The provided ID doesn't exist.");

            if (formTemplateExist.Translations == null || formTemplateExist.Translations.Count < 2) throw new HttpStatusException(HttpStatusCode.BadRequest, $"This form template only has the {formTemplateExist.Translations!.First().Language} language. It is not possible to delete a language if the form template only contains one language");

            var translationToBeDeleted = formTemplateExist.Translations.FirstOrDefault(temp => temp.Language == language.ToString()) ?? throw new HttpStatusException(HttpStatusCode.BadRequest, $"Resource not found. The provided Language doesn't exist.");

            var response = await _formTemplateTranslationsRepository.DeleteFormTemplateTranslationById(translationToBeDeleted.FormTemplateTranslationId);
            return new ApiResponse<bool>()
            {
                StatusCode = (int)HttpStatusCode.NoContent,
                Message = "Translation of FormTemplate deleted with success!",
                Data = response,
            };
        }
    }
}
