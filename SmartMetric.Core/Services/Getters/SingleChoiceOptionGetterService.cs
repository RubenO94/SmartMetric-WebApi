using Microsoft.Extensions.Logging;
using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.Exceptions;
using SmartMetric.Core.ServicesContracts.Getters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.Services.Getters
{
    public class SingleChoiceOptionGetterService : ISingleChoiceOptionGetterService
    {
        private readonly ISingleChoiceOptionRepository _singleChoiceOptionRepository;
        private readonly ILogger<SingleChoiceOptionGetterService> _logger;

        public SingleChoiceOptionGetterService (ISingleChoiceOptionRepository singleChoiceOptionRepository, ILogger<SingleChoiceOptionGetterService> logger)
        {
            _singleChoiceOptionRepository = singleChoiceOptionRepository;
            _logger = logger;
        }


        public async Task<ApiResponse<List<SingleChoiceOptionDTOResponse>>> GetAllSingleChoiceOption()
        {
            _logger.LogInformation($"{nameof(SingleChoiceOptionGetterService)}.{nameof(GetAllSingleChoiceOption)} foi iniciado");

            var sco = await _singleChoiceOptionRepository.GetAllSingleChoiceOptions();

            return new ApiResponse<List<SingleChoiceOptionDTOResponse>>()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Data retrieved successfully.",
                Data = sco.Select(temp => temp.ToSingleChoiceOptionDTOResponse()).ToList()
            };
        }

        public async Task<ApiResponse<SingleChoiceOptionDTOResponse?>> GetSingleChoiceOptionById(Guid? singleChoiceOptionId)
        {
            _logger.LogInformation($"{nameof(SingleChoiceOptionGetterService)}.{nameof(GetSingleChoiceOptionById)} foi iniciado");

            if (singleChoiceOptionId == null ) 
            {
                throw new HttpStatusException(HttpStatusCode.BadRequest, "The 'singleChoiceOptionId' parameter is required and must be a valid GUID.");
            }

            SingleChoiceOption? singleChoiceOption = await _singleChoiceOptionRepository.GetSingleChoiceOptionById(singleChoiceOptionId.Value);

            if (singleChoiceOption == null) 
            {
                throw new HttpStatusException(HttpStatusCode.NotFound, "Resource not found. The provided ID does not exist.");
            }

            return new ApiResponse<SingleChoiceOptionDTOResponse?>()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Data retrieved successfully.",
                Data = singleChoiceOption.ToSingleChoiceOptionDTOResponse()
            };
        }

        public async Task<ApiResponse<List<SingleChoiceOptionDTOResponse>?>> GetSingleChoiceOptionByQuestionId(Guid? questionId)
        {
            _logger.LogInformation($"{nameof(SingleChoiceOptionGetterService)}.{nameof(GetSingleChoiceOptionByQuestionId)} foi iniciado");

            if (questionId == null)
            {
                throw new HttpStatusException(HttpStatusCode.BadRequest, "The 'questionId' parameter is required and must be a valid GUID.");
            }

            var sco = await _singleChoiceOptionRepository.GetSingleChoiceOptionsByQuestionId(questionId.Value);
            if (sco == null) throw new HttpStatusException(HttpStatusCode.NotFound, "Resource not found. The provided ID doesn't exist.");


            return new ApiResponse<List<SingleChoiceOptionDTOResponse>?>()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Data retrieved successfully.",
                Data = sco?.Select(temp => temp.ToSingleChoiceOptionDTOResponse()).ToList()
            };

        }
    }
}
