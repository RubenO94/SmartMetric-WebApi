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
    public class SingleChoiceOptionDeleterService : ISingleChoiceOptionDeleterService
    {
        //variables
        private readonly ISingleChoiceOptionRepository _singleChoiceOptionRepository;
        private readonly ILogger<SingleChoiceOptionDeleterService> _logger;

        //constructor
        public SingleChoiceOptionDeleterService (ISingleChoiceOptionRepository singleChoiceOptionRepository, ILogger<SingleChoiceOptionDeleterService> logger)
        {
            _singleChoiceOptionRepository = singleChoiceOptionRepository;
            _logger = logger;
        }

        //deleters
        public async Task<bool> DeleteSingleChoiceOptionById(Guid? singleChoiceOptionId)
        {
            _logger.LogInformation($"{nameof(SingleChoiceOptionDeleterService)}.{nameof(DeleteSingleChoiceOptionById)} foi iniciado");

            if ( singleChoiceOptionId == null ) { return false; }

            return await _singleChoiceOptionRepository.DeleteSingleChoiceOptionById(singleChoiceOptionId.Value);
        }
    }
}
