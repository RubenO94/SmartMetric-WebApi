using Microsoft.Extensions.Logging;
using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.Exceptions;
using SmartMetric.Core.ServicesContracts.Deleters;
using System.Net;

namespace SmartMetric.Core.Services.Deleters
{
    public class FormTemplatesDeleterService : IFormTemplatesDeleterService
    {
        private readonly IFormTemplateRepository _formTemplateRepository;
        private readonly ILogger<FormTemplatesDeleterService> _logger;

        public FormTemplatesDeleterService(IFormTemplateRepository formTemplateRepository, ILogger<FormTemplatesDeleterService> logger)
        {
            _formTemplateRepository = formTemplateRepository;
            _logger = logger;
        }

        public async Task<ApiResponse<bool>> DeleteFormTemplateById(Guid? formTemplateId)
        {
            _logger.LogInformation($"{nameof(FormTemplatesDeleterService)}.{nameof(DeleteFormTemplateById)} foi iniciado");

            if (formTemplateId == null) throw new HttpStatusException(HttpStatusCode.BadRequest, "The 'formTemplateId' parameter is required and must be a valid GUID.");

            FormTemplate? formTemplateExist =  await _formTemplateRepository.GetFormTemplateById(formTemplateId) ?? throw new HttpStatusException(HttpStatusCode.NotFound, "Resource not found. The provided ID does not exist.");

            var response = await _formTemplateRepository.DeleteFormTemplateById(formTemplateExist.FormTemplateId);
            return new ApiResponse<bool>()
            {
                StatusCode = (int)HttpStatusCode.NoContent,
                Message = "FormTemplate deleted successfully.",
                Data = response
            };
        }
    }
}
