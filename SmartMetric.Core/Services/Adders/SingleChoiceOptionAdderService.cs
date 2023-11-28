using Microsoft.Extensions.Logging;
using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Core.DTO.AddRequest;
using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.Enums;
using SmartMetric.Core.Exceptions;
using SmartMetric.Core.Helpers;
using SmartMetric.Core.ServicesContracts.Adders;
using SmartMetric.Core.ServicesContracts.Getters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.Services.Adders
{
    public class SingleChoiceOptionAdderService : ISingleChoiceOptionsAdderService
    {
        private readonly ISingleChoiceOptionRepository _singleChoiceOptionRepository;
        private readonly IQuestionGetterService _questionGetterService;
        private readonly ILogger<SingleChoiceOptionAdderService> _logger;

        public SingleChoiceOptionAdderService(ISingleChoiceOptionRepository singleChoiceOptionRepository, IQuestionGetterService questionGetterService, ILogger<SingleChoiceOptionAdderService> logger)
        {
            _singleChoiceOptionRepository = singleChoiceOptionRepository;
            _questionGetterService = questionGetterService;
            _logger = logger;
        }
        public async Task<ApiResponse<SingleChoiceOptionDTOResponse?>> AddSingleChoiceOption(Guid? questionId, SingleChoiceOptionDTOAddRequest? request)
        {
            _logger.LogInformation($"{nameof(SingleChoiceOptionAdderService)}.{nameof(AddSingleChoiceOption)} foi iniciado");

            if(questionId == null) throw new ArgumentNullException(nameof(questionId));

            if (request == null) throw new ArgumentNullException(nameof(SingleChoiceOption));

            var question = await _questionGetterService.GetQuestionById(questionId.Value);

            if (question.Data == null) throw new HttpStatusException(HttpStatusCode.BadRequest, "The 'questionId' provided does not exist.");

            if (question.Data.ResponseType != ResponseType.SingleChoice.ToString())
            {
                throw new HttpStatusException(HttpStatusCode.BadRequest, $"The question provided isn't of type {ResponseType.SingleChoice}");
            }

            //var singleChoiceOptionId = Guid.NewGuid();
            SingleChoiceOption singleChoiceOption = request.ToSingleChoiceOption();
            singleChoiceOption.QuestionId = questionId;

            //foreach (var translation in singleChoiceOption.Translations!)
            //{
            //    translation.SingleChoiceOptionTranslationId = Guid.NewGuid();
            //    translation.SingleChoiceOptionId = singleChoiceOptionId;
            //}

            await _singleChoiceOptionRepository.AddSingleChoiceOption(singleChoiceOption);

            return new ApiResponse<SingleChoiceOptionDTOResponse?>()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "SingleChoiceOption added successfully to the Question.",
                Data = singleChoiceOption.ToSingleChoiceOptionDTOResponse()
            };
        }

    }
}

