using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Core.DTO;
using SmartMetric.Core.ServicesContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.Services
{
    public class FormTemplatesService : IFormTemplateService
    {
        private readonly IFormTemplateRepository _formTemplateRepository;
        private readonly ILogger<FormTemplatesService> _logger;
        public FormTemplatesService(IFormTemplateRepository formTemplateRepository, ILogger<FormTemplatesService> logger)
        {
            _formTemplateRepository = formTemplateRepository;
            _logger = logger;
        }

        public Task<FormTemplateDTOResponse?> AddFormTemplate(FormTemplateDTOAddRequest? addFormTemplateRequest)
        {
            //_logger.LogInformation("FormTemplateService.AddFormTemplate foi iniciado");

            //if (addFormTemplateRequest == null)
            //{
            //    throw new ArgumentNullException(nameof(addFormTemplateRequest));
            //}

            ////Model Validation
            ////TODO: Validation logic

            //FormTemplate formTemplate = addFormTemplateRequest.ToFormTemplate();

            //formTemplate.FormTemplateId = Guid.NewGuid();

            throw new NotImplementedException();
        }

        public Task<List<FormTemplateDTOResponse?>> GetAllFormTemplates()
        {
            throw new NotImplementedException();
        }

        public Task<FormTemplateDTOResponse?> GetFormTemplateById(Guid formTemplateId)
        {
            throw new NotImplementedException();
        }
    }
}
