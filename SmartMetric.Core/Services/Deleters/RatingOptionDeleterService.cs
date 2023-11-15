using Microsoft.Extensions.Logging;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Core.ServicesContracts.Deleters;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<bool> DeleteRatingOptionById(Guid? ratingOptionId)
        {
            _logger.LogInformation($"{nameof(RatingOptionDeleterService)}.{nameof(DeleteRatingOptionById)} foi iniciado");

            if ( ratingOptionId == null ) { return false; }

            return await _ratingOptionRepository.DeleteRatingOptionById(ratingOptionId.Value);
        }
    }
}
