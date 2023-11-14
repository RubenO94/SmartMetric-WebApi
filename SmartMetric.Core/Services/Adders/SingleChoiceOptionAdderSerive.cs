using Microsoft.Extensions.Logging;
using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Core.DTO.AddRequest;
using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.Helpers;
using SmartMetric.Core.ServicesContracts.Adders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.Services.Adders
{
    public class SingleChoiceOptionAdderSerive : ISingleChoiceOptionsAdderService
    {
        private readonly ISingleChoiceOptionRepository _singleChoiceOptionRepository;
        private readonly ILogger<SingleChoiceOptionAdderSerive> _logger;

        public SingleChoiceOptionAdderSerive(ISingleChoiceOptionRepository singleChoiceOptionRepository, ILogger<SingleChoiceOptionAdderSerive> logger)
        {
            _singleChoiceOptionRepository = singleChoiceOptionRepository;
            _logger = logger;
        }
        public async Task<SingleChoiceOptionDTOResponse?> AddSingleChoiceOption(SingleChoiceOptionDTOAddRequest? request)
        {
            _logger.LogInformation($"{nameof(SingleChoiceOptionAdderSerive)}.{nameof(AddSingleChoiceOption)} foi iniciado");

            if (request == null) throw new ArgumentNullException(nameof(request));

            ValidationHelper.ModelValidation(request);

            var singleChoiceOption = request.ToSingleChoiceOption();

            singleChoiceOption.SingleChoiceOptionId = Guid.NewGuid();

            var response = await _singleChoiceOptionRepository.AddSingleChoiceOption(singleChoiceOption);

            return response.ToSingleChoiceOptionDTOResponse();

        }
    }
}
