using Microsoft.Extensions.Logging;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.Exceptions;
using SmartMetric.Core.ServicesContracts.SingleChoiceOptions;
using System.Net;

namespace SmartMetric.Core.Services.SingleChoiceOptions
{
    public class SingleChoiceOptionDeleterService : ISingleChoiceOptionDeleterService
    {
        //variables
        private readonly ISingleChoiceOptionRepository _singleChoiceOptionRepository;
        private readonly ISingleChoiceOptionGetterService _singleChoiceOptionGetterService;
        private readonly ILogger<SingleChoiceOptionDeleterService> _logger;

        //constructor
        public SingleChoiceOptionDeleterService (ISingleChoiceOptionRepository singleChoiceOptionRepository, ISingleChoiceOptionGetterService singleChoiceOptionGetterService, ILogger<SingleChoiceOptionDeleterService> logger)
        {
            _singleChoiceOptionRepository = singleChoiceOptionRepository;
            _singleChoiceOptionGetterService = singleChoiceOptionGetterService;
            _logger = logger;
        }

        //deleters
        public async Task<ApiResponse<bool>> DeleteSingleChoiceOptionById(Guid? singleChoiceOptionId)
        {
            _logger.LogInformation($"{nameof(SingleChoiceOptionDeleterService)}.{nameof(DeleteSingleChoiceOptionById)} foi iniciado");

            if (singleChoiceOptionId == null) throw new HttpStatusException(HttpStatusCode.BadRequest, "SingleChoiceOptionId can't be null!");

            var singleChoiceOptionExist = _singleChoiceOptionGetterService.GetSingleChoiceOptionById(singleChoiceOptionId) ?? throw new HttpStatusException(HttpStatusCode.NotFound, "SingleChoiceOption doesn't exist!");

            var response = await _singleChoiceOptionRepository.DeleteSingleChoiceOptionById(singleChoiceOptionId.Value);
            return new ApiResponse<bool>()
            {
                StatusCode = (int)HttpStatusCode.NoContent,
                Message = "SingleChoiceOption deleted with success!",
                Data = response
            };
        }
    }
}
