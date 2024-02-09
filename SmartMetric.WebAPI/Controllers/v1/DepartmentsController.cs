using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SmartMetric.Core.Enums;
using SmartMetric.Core.ServicesContracts;
using SmartMetric.WebAPI.Filters.ActionFilter;
using SmartMetric.WebAPI.Filters.AutorizationFilter;
using System.Security.Claims;

namespace SmartMetric.WebAPI.Controllers.v1
{
    /// <summary>
    /// Controlador responsável por operações relacionadas a departamentos.
    /// </summary>
    [ApiVersion("1.0")]
    public class DepartmentsController : CustomBaseController
    {
        private readonly ISmartTimeService _smartTimeService;
        private readonly IJwtService _jwtService;

        /// <summary>
        /// Construtor do controlador Departments.
        /// </summary>
        /// <param name="smartTimeService">Serviço SmartTime para operações relacionadas a departamentos.</param>
        /// <param name="jwtService">Serviço JWT para autenticação e geração de tokens.</param>
        public DepartmentsController(ISmartTimeService smartTimeService, IJwtService jwtService)
        {
            _smartTimeService = smartTimeService;
            _jwtService = jwtService;
        }

        /// <summary>
        /// Obtém todos os departamentos com base no ID do perfil.
        /// </summary>
        /// <param name="page">Número da página.</param>
        /// <param name="pageSize">Tamanho da página.</param>
        /// <returns>Um IActionResult representando os departamentos obtidos.</returns>
        /// <exception cref="ArgumentException">Lançada quando o utilizador não tem um perfil associado.</exception>
        [HttpGet]
        public async Task<IActionResult> GetAllDepartmentsByProfileId(int page = 1, int pageSize = 20)
        {
            var principal = _GetPrincipal();

            if (principal != null)
            {
                var userName = principal.FindFirst("name");
                if (userName?.Value == "Administrador")
                {
                    var departments = await _smartTimeService.GetAllDepartments(page, pageSize);

                    return Ok(departments);
                }

                var userProfileId = principal.FindFirst("UserProfileId");
                if (userProfileId?.Value != string.Empty)
                {

                    var departments = await _smartTimeService.GetDepartmentsByProfileId(int.Parse(userProfileId?.Value!), page, pageSize);

                    return Ok(departments);
                }
            }

            throw new ArgumentException("User does not have an associated profile");
        }

        /// <summary>
        /// Obtém os funcionários de um departamento específico.
        /// </summary>
        /// <param name="departmentId">ID do departamento.</param>
        /// <returns>Um IActionResult representando os funcionários obtidos.</returns>
        /// <exception cref="ArgumentException">Lançada quando o utilizador não tem acesso ao departamento especificado ou o ID do departamento não existe.</exception>
        [HttpGet("{departmentId}/Employees")]
        public async Task<IActionResult> GetDepartmentEmployees(int departmentId)
        {
            var principal = _GetPrincipal();

            if (principal != null)
            {
                var userName = principal.FindFirst("name");
                if (userName?.Value == "Administrador")
                {
                    var response = await _smartTimeService.GetEmployeesByDepartmentId(departmentId);
                    return Ok(response);
                }

                var userProfileId = principal.FindFirst("UserProfileId");
                if (userProfileId?.Value != string.Empty)
                {

                    var profileDepartments = await _smartTimeService.GetDepartmentsByProfileId(int.Parse(userProfileId?.Value!), pageSize: int.MaxValue);
                    if (!profileDepartments.Data!.Any(temp => temp.DepartmentId == departmentId)) throw new ArgumentException("This user does not have access to the department given in the parameter or the department id does not exist", nameof(departmentId));

                    var response = await _smartTimeService.GetEmployeesByDepartmentId(departmentId);
                    return Ok(response);


                }
            }

            throw new ArgumentException("User does not have an associated profile");
        }


        private ClaimsPrincipal? _GetPrincipal()
        {
            var authorizationHeader = HttpContext.Request.Headers.Authorization;
            var token = authorizationHeader.ToString().Replace("Bearer ", string.Empty);

            if (!token.IsNullOrEmpty())
            {
                return _jwtService.GetPrincipalFromJwtToken(token);
            }

            return null;
        }
    }
}
