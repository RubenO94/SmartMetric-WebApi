using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartMetric.Core.DTO;
using SmartMetric.Core.Enums;
using SmartMetric.Core.ServicesContracts;
using SmartMetric.WebAPI.Filters.ActionFilter;

namespace SmartMetric.WebAPI.Controllers.v1
{
    /// <summary>
    /// Controlador responsável por operações relacionadas a perfis de utilizadores.
    /// </summary>
    [ApiVersion("1.0")]
    public class ProfilesController : CustomBaseController
    {
        private readonly ISmartTimeService _smartTimeService;

        /// <summary>
        /// Construtor do controlador Profiles.
        /// </summary>
        /// <param name="smartTimeService">Serviço SmartTime utilizado para operações relacionadas a perfis.</param>
        public ProfilesController(ISmartTimeService smartTimeService)
        {
            _smartTimeService = smartTimeService;
        }

        /// <summary>
        /// Obtém os departamentos associados a um perfil pelo ID do perfil.
        /// </summary>
        /// <param name="profileId">ID do perfil.</param>
        /// <param name="page">Número da página.</param>
        /// <param name="pageSize">Tamanho da página.</param>
        /// <returns>Um IActionResult representando os departamentos associados ao perfil.</returns>
        [HttpGet("{profileId}/Departments")]
        public async Task<IActionResult> GetDepartmentsByProfileId(int profileId, [FromQuery] int page = 1, int pageSize = 20)
        {
          
            var response = await _smartTimeService.GetDepartmentsByProfileId(profileId, page, pageSize);
            return Ok(response);
        }

        /// <summary>
        /// Obtém todos os perfis
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllProfiles()
        {
            var response = await _smartTimeService.GetAllProfiles();
            return Ok(response);
        }
    }

}
