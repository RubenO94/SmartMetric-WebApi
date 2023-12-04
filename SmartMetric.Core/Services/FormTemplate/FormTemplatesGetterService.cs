﻿using Microsoft.Extensions.Logging;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.Exceptions;
using SmartMetric.Core.ServicesContracts.FormTemplates;
using System.Net;

namespace SmartMetric.Core.Services.FormTemplates
{
    public class FormTemplatesGetterService : IFormTemplatesGetterService
    {
        private readonly IFormTemplateRepository _formTemplateRepository;
        private readonly ILogger<FormTemplatesGetterService> _logger;
        public FormTemplatesGetterService(IFormTemplateRepository formTemplateRepository, ILogger<FormTemplatesGetterService> logger)
        {
            _formTemplateRepository = formTemplateRepository;
            _logger = logger;
        }

        public async Task<ApiResponse<List<FormTemplateDTOResponse?>>> GetAllFormTemplates()
        {
            _logger.LogInformation($"{nameof(FormTemplatesGetterService)}.{nameof(GetAllFormTemplates)} foi iniciado");

            var formTemplates = await _formTemplateRepository.GetAllFormTemplates();

            var response = formTemplates.Select(temp => temp.ToFormTemplateDTOResponse()).ToList();

            return new ApiResponse<List<FormTemplateDTOResponse?>>()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Data retrieved successfully.",
                Data = response!
            };
        }

        public async Task<ApiResponse<FormTemplateDTOResponse?>> GetFormTemplateById(Guid? formTemplateId)
        {
            _logger.LogInformation($"{nameof(FormTemplatesGetterService)}.{nameof(GetFormTemplateById)} foi iniciado");

            if(formTemplateId == null ) throw new HttpStatusException(HttpStatusCode.BadRequest, "The 'formTemplateId' parameter is required and must be a valid GUID.");

            var formTemplate = await _formTemplateRepository.GetFormTemplateById(formTemplateId.Value);

            if(formTemplate == null ) throw new HttpStatusException(HttpStatusCode.NotFound, "Resource not found. The provided ID does not exist.");

            return new ApiResponse<FormTemplateDTOResponse?>()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Data retrieved successfully.",
                Data = formTemplate?.ToFormTemplateDTOResponse()!
            };
        }
    }
}