using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.ServicesContracts;
using SmartMetric.Infrastructure.Models;

namespace SmartMetric.WebAPI.Controllers.v1
{
    [ApiVersion("1.0")]
    public class UsersController : CustomBaseController
    {
        private readonly ISmartTimeService _smartTimeService;

        public UsersController(ISmartTimeService smartTimeService)
        {
            _smartTimeService = smartTimeService;
        }

        [HttpGet("{userId}/Perfil")]
        public async Task<IActionResult> GetPerfil(int userId)
        {
            var perfil = await _smartTimeService.GetPerfilByUserId(userId);
            return Ok(perfil);
        }

        [HttpGet("{userId}/Departaments")]
        public async Task<IActionResult> GetDepartamentosByPerfilId([FromQuery] int perfilId)
        {
            var departamentos = await _smartTimeService.GetDepartmentsByPerfilId(perfilId);

            if (departamentos == null || departamentos.Count == 0)
            {
                return NotFound("Departamentos não encontrados para o utilizador");
            }

            return Ok(departamentos);
        }
    }
}
