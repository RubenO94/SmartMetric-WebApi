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
    public class RatingOptionGetterService : IRatingOptionGetterService
    {
        private readonly IRatingOptionRepository _ratingOptionRepository;
        private readonly ILogger<RatingOptionGetterService> _logger;

        public RatingOptionGetterService(IRatingOptionRepository ratingOptionRepository, ILogger<RatingOptionGetterService> logger)
        {
            _ratingOptionRepository = ratingOptionRepository;
            _logger = logger;
        }

        #region Getters

        public async Task<ApiResponse<List<RatingOptionDTOResponse>>> GetAllRatingOptions()
        {
            _logger.LogInformation($"{nameof(RatingOptionGetterService)}.{nameof(GetAllRatingOptions)} foi iniciado");
            var ratingOption = await _ratingOptionRepository.GetAllRatingOption();

            return new ApiResponse<List<RatingOptionDTOResponse>>()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Data retrieved successfully.",
                Data = ratingOption.Select(temp => temp.ToRatingOptionDTOResponse()).ToList()
            };
        }

        public async Task<ApiResponse<RatingOptionDTOResponse?>> GetRatingOptionById(Guid? ratingOptionId)
        {
            _logger.LogInformation($"{nameof(RatingOptionGetterService)}.{nameof(GetRatingOptionById)} foi iniciado");

            if (ratingOptionId == null)
            {
                throw new HttpStatusException(HttpStatusCode.BadRequest, "The 'ratingOptionId' parameter is required and must be a valid GUID.");
            }

            RatingOption? ratingOption = await _ratingOptionRepository.GetRatingOptionById(ratingOptionId.Value);

            if (ratingOption == null)
            {
                throw new HttpStatusException(HttpStatusCode.NotFound, "Resource not found. The provided ID does not exist.");
            }

            return new ApiResponse<RatingOptionDTOResponse?>()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Data retrieved successfully.",
                Data = ratingOption.ToRatingOptionDTOResponse()
            };

        }

        public async Task<ApiResponse<List<RatingOptionDTOResponse>?>> GetRatingOptionsByQuestionId(Guid? questionId)
        {
            _logger.LogInformation($"{nameof(RatingOptionGetterService)}.{nameof(GetRatingOptionsByQuestionId)} foi iniciado");

            if (questionId == null) 
            {
                throw new HttpStatusException(HttpStatusCode.BadRequest, "The 'questionId' parameter is required and must be a valid GUID.");
            }

            var ratingOption = await _ratingOptionRepository.GetRatingOptionByQuestionId(questionId.Value);

            if (ratingOption == null || ratingOption.Count == 0) throw new HttpStatusException(HttpStatusCode.NotFound, "Resource not found. The provided ID does not exist.");

            return new ApiResponse<List<RatingOptionDTOResponse>?>()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Data retrieved successfully.",
                Data = ratingOption?.Select(temp => temp.ToRatingOptionDTOResponse()).ToList()
            };
        }

        #endregion
    }
}
