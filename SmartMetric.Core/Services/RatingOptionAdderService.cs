using Microsoft.Extensions.Logging;
using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Core.DTO;
using SmartMetric.Core.Helpers;
using SmartMetric.Core.ServicesContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.Services
{
    public class RatingOptionAdderService : IRatingOptionAdderService
    {
        private readonly IRatingOptionRepository _translationsRepository;
        private readonly ILogger<RatingOptionAdderService> _logger;

        public RatingOptionAdderService(IRatingOptionRepository translationsRepository, ILogger<RatingOptionAdderService> logger)
        {
            _translationsRepository = translationsRepository;
            _logger = logger;
        }

        public async Task<RatingOptionDTOResponse> AddRatingOption(RatingOptionDTOAddRequest? request)
        {
            _logger.LogInformation($"{nameof(RatingOptionAdderService)}.{nameof(AddRatingOption)} foi iniciado");

            if (request == null ) { throw new ArgumentNullException(nameof(request)); }

            ValidationHelper.ModelValidation(request);

            RatingOption translation = request.ToRatingOption();

            translation.RatingOptionId = Guid.NewGuid();

            await _translationsRepository.AddRatingOption(translation);

            return translation.ToRatingOptionDTOResponse();
        }
    }
}
