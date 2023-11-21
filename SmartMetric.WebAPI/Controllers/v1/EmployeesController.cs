using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SmartMetric.Core.Domain.RepositoryContracts;

namespace SmartMetric.WebAPI.Controllers.v1
{
    [ApiVersion("1.0")]
    public class EmployeesController : CustomBaseController
    {
        private readonly ISmartTimeRepository _employeeRepository;

        public EmployeesController(ISmartTimeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var response = await _employeeRepository.GetAllEmployees();

            return Ok(response);
        }
    }
}
