﻿using Microsoft.Extensions.Logging;
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
    public class FormTemplateTranslationDeleterService : IFormTemplateTranslationsDeleterService
    {
        //VARIABLES
        private readonly IFormTemplateTranslationsRepository _formTemplateTranslationsRepository;
        private readonly IFormTemplatesRepository _formTemplatesRepository;
        private readonly ILogger<FormTemplateTranslationDeleterService> _logger;

        //CONSTRUCTOR
        public FormTemplateTranslationDeleterService (IFormTemplateTranslationsRepository formTemplateTranslationsRepository, IFormTemplatesRepository formTemplatesRepository, ILogger<FormTemplateTranslationDeleterService> logger)
        {
            _formTemplateTranslationsRepository = formTemplateTranslationsRepository;
            _formTemplatesRepository = formTemplatesRepository;
            _logger = logger;
        }

        //DELETERS
        public async Task<ApiResponse<bool>> DeleteFormTemplateTranslationById(Guid? formTemplateId, Language language)
        {
            _logger.LogInformation($"{nameof(FormTemplateTranslationDeleterService)}.{nameof(DeleteFormTemplateTranslationById)} foi iniciado");

            if (formTemplateId == null) throw new HttpStatusException(HttpStatusCode.BadRequest, "FormTemplateId can't be null!");

            var formTemplateExist = await _formTemplatesRepository.GetFormTemplateById(formTemplateId);
            
            if (formTemplateExist == null) throw new HttpStatusException(HttpStatusCode.NotFound, "Resource not found. The provided ID doesn't exist.");

            if (formTemplateExist.Translations == null || formTemplateExist.Translations.Count < 2) throw new HttpStatusException(HttpStatusCode.BadRequest, "FormTemplate must have at least one translation, so can't execute your request!");

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
