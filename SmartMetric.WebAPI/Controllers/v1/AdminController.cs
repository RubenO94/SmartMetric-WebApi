using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.DTO;
using SmartMetric.Core.Enums;
using SmartMetric.Core.Helpers;
using SmartMetric.Core.ServicesContracts;
using SmartMetric.WebAPI.Filters.ActionFilter;

namespace SmartMetric.WebAPI.Controllers.v1
{
    /// <summary>
    /// Controlador responsável por operações administrativas.
    /// </summary>
    [ApiVersion("1.0")]
    public class AdminController : CustomBaseController
    {
        private readonly ISmartTimeService _smartTimeService;

        /// <summary>
        /// Construtor do controlador Admin.
        /// </summary>
        /// <param name="smartTimeService">Serviço SmartTime para operações administrativas.</param>
        public AdminController(ISmartTimeService smartTimeService)
        {
            _smartTimeService = smartTimeService;
        }

        /// <summary>
        /// Obtém as permissões para um perfil específico
        /// </summary>
        /// <param name="profileId"></param>
        /// <returns></returns>
        [HttpGet("{profileId}/Permissions")]
        public async Task<IActionResult> GetPermissions(int profileId)
        {
            var response = await _smartTimeService.GetWindowPermissionsToProfile(profileId);
            return Ok(response);
        }

        /// <summary>
        /// Define as permissões para um perfil específico.
        /// </summary>
        /// <param name="profileId">O ID do perfil.</param>
        /// <param name="permissionIds">Lista de IDs das permissões a serem atribuídas ao perfil.</param>
        /// <returns>Um IActionResult representando o resultado da operação.</returns>
        [HttpPost("{profileId}/Permissions")]
        public async Task<IActionResult> SetPermissions(int profileId, [FromBody] List<int> permissionIds)
        {
            // Atualiza as permissões do perfil
            var response = await _smartTimeService.UpdateWindowPermissionsToProfile(profileId, permissionIds);

            // Retorna uma resposta bem-sucedida com os detalhes da operação
            return Ok(response);
        }

        /// <summary>
        /// Obtém as permissões de leitura para todas as janelas da aplicação.
        /// </summary>
        /// <returns>Um IActionResult representando as permissões de leitura para todas as janelas.</returns>
        [HttpGet("Windows")]
        public IActionResult GetAllWindowsPermissions()
        {
            var response = new ApiResponse<List<WindowPermissionDTO>>()
            {
                StatusCode = 200,
                Message = "Success",
                Data = WindowPermissionHelper.GetAllWindows(),
            };

            return Ok(response);
        }
    }

}
