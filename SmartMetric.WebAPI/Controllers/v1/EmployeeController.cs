using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.ServicesContracts;

namespace SmartMetric.WebAPI.Controllers.v1
{
    /// <summary>
    /// Controlador responsável por operações relacionadas a functionários.
    /// </summary>
    [ApiVersion("1.0")]
    public class EmployeeController : CustomBaseController
    {
        private readonly ISmartTimeRepository _smartTimeRepository;

        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="smartTimeRepository"></param>
        public EmployeeController(ISmartTimeRepository smartTimeRepository)
        {
            _smartTimeRepository = smartTimeRepository;
        }

        /// <summary>
        /// Metodo para recebr todos os funcionários ao encargo de um chefe específico
        /// </summary>
        /// <param name="chefiaId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetEmployeesByChefiaId(int chefiaId)
        {
            var employees = await _smartTimeRepository.GetEmployeesByChiefId(chefiaId);
            var response = new ApiResponse<List<EmployeeDTOResponse>?>() {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Employees list data retrieved successfully.",
                Data = employees.Select(temp => temp.ToEmployeeDTOResponse()).ToList(),
                TotalCount = employees.Count
            };
            return Ok(response);
        }

    }
}