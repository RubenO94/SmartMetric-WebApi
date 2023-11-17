using Microsoft.Extensions.Logging;
using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.ServicesContracts.Getters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.Services.Getters
{
    public class SingleChoiceOptionGetterService : ISingleChoiceOptionGetterService
    {
        //VARIABLES
        private readonly ISingleChoiceOptionRepository _singleChoiceOptionRepository;
        private readonly ILogger<SingleChoiceOptionGetterService> _logger;

        //CONSTRUCTOR
        public SingleChoiceOptionGetterService (ISingleChoiceOptionRepository singleChoiceOptionRepository, ILogger<SingleChoiceOptionGetterService> logger)
        {
            _singleChoiceOptionRepository = singleChoiceOptionRepository;
            _logger = logger;
        }


        public Task<List<SingleChoiceOptionDTOResponse>> GetAllSingleChoiceOption()
        {
            throw new NotImplementedException();
        }

        public async Task<SingleChoiceOptionDTOResponse?> GetSingleChoiceOptionById(Guid? singleChoiceOptionId)
        {
            _logger.LogInformation($"{nameof(SingleChoiceOptionGetterService)}.{nameof(GetSingleChoiceOptionById)} foi iniciado");

            if (singleChoiceOptionId == null ) { throw new ArgumentNullException(nameof(singleChoiceOptionId)); }

            SingleChoiceOption? singleChoiceOption = await _singleChoiceOptionRepository.GetSingleChoiceOptionById(singleChoiceOptionId.Value);

            if (singleChoiceOption == null) { return null; }

            return singleChoiceOption.ToSingleChoiceOptionDTOResponse();
        }

        public Task<List<SingleChoiceOptionDTOResponse>?> GetSingleChoiceOptionByQuestionId(Guid? questionId)
        {
            throw new NotImplementedException();
        }
    }
}
