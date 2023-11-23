using Microsoft.Extensions.Logging;
using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Core.DTO.Response;
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
    public class RatingOptionDeleterService : IRatingOptionDeleterService
    {
        //variables
        private readonly IRatingOptionRepository _ratingOptionRepository;
        private readonly ILogger<RatingOptionDeleterService> _logger;

        //constructor
        public RatingOptionDeleterService (IRatingOptionRepository ratingOptionRepository, ILogger<RatingOptionDeleterService> logger)
        {
            _ratingOptionRepository = ratingOptionRepository;
            _logger = logger;
        }

        //deleters
        public async Task<ApiResponse<bool>> DeleteRatingOptionById(Guid? ratingOptionId)
        {
            _logger.LogInformation($"{nameof(RatingOptionDeleterService)}.{nameof(DeleteRatingOptionById)} foi iniciado");

            if (ratingOptionId == null) throw new HttpStatusException(HttpStatusCode.BadRequest, "The 'ratingOptionId' parameter is required and must be a valid GUID.");

            var ratingOptionExist = await _ratingOptionRepository.GetRatingOptionById(ratingOptionId);
            if (ratingOptionExist == null) throw new HttpStatusException(HttpStatusCode.BadRequest, "Resource not found. The provided ID does not exist.");
            
            var response = await _ratingOptionRepository.DeleteRatingOptionById(ratingOptionId.Value);
            return new ApiResponse<bool>()
            {
                StatusCode = (int)HttpStatusCode.NoContent,
                Message = "RatingOption deleted with success!",
                Data = response
            };
        }
    }
}
