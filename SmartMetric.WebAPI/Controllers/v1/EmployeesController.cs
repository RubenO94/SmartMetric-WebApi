using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartMetric.Core.Domain.RepositoryContracts;

namespace SmartMetric.WebAPI.Controllers.v1
{
    [ApiVersion("1.0")]
    public class EmployeesController : CustomBaseController
    {
        private readonly ISmartTimeRepository _smartTimeRepository;

        public EmployeesController(ISmartTimeRepository smartTimeRepository)
        {
            _smartTimeRepository = smartTimeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployeesByChiefId(int chiefId)
        {
            var result = await _smartTimeRepository.GetEmployeesByChiefId(chiefId);

            return Ok(result);
        }
    }
}
