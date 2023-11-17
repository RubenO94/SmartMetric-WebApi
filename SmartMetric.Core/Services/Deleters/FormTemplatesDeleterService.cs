using Microsoft.Extensions.Logging;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.Exceptions;
using SmartMetric.Core.Services.Getters;
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
    public class FormTemplatesDeleterService : IFormTemplatesDeleterService
    {

        private IFormTemplatesRepository _formTemplateRepository;
        private IFormTemplatesGetterService _formTemplatesGetterService;
        private ILogger<FormTemplatesDeleterService> _logger;

        public FormTemplatesDeleterService(IFormTemplatesRepository formTemplateRepository, IFormTemplatesGetterService formTemplatesGetterService, ILogger<FormTemplatesDeleterService> logger)
        {
            _formTemplateRepository = formTemplateRepository;
            _formTemplatesGetterService = formTemplatesGetterService;
            _logger = logger;
        }

        public async Task<ApiResponse<bool>> DeleteFormTemplateById(Guid? formTemplateId)
        {
            _logger.LogInformation($"{nameof(FormTemplatesDeleterService)}.{nameof(DeleteFormTemplateById)} foi iniciado");

            if (formTemplateId == null) throw new HttpStatusException(HttpStatusCode.BadRequest, "FormTemplateId can't be null!");

            var formTemplateExist = _formTemplatesGetterService.GetFormTemplateById(formTemplateId) ?? throw new HttpStatusException(HttpStatusCode.NotFound, "FormTemplate doesn't exist");

            await _formTemplateRepository.DeleteFormTemplateById(formTemplateId.Value);
            return new ApiResponse<bool>()
            {
                StatusCode = (int)HttpStatusCode.NoContent,
                Message = "FormTemplate deleted with success!",
                Data = true
            };
        }
    }
}
