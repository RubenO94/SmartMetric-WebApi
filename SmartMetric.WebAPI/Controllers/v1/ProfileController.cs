using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartMetric.Core.DTO;
using SmartMetric.Core.ServicesContracts;

namespace SmartMetric.WebAPI.Controllers.v1
{
    [ApiVersion("1.0")]
    public class ProfileController : CustomBaseController
    {
        private readonly ISmartTimeService _smartTimeService;

        public ProfileController(ISmartTimeService smartTimeService)
        {
            _smartTimeService = smartTimeService;
        }

        [HttpGet("{profileId}/Departments")]
        public async Task<IActionResult> GetDepartmentsByProfileId(int profileId, [FromQuery] int page = 1, int pageSize = 20)
        {
            var response = await _smartTimeService.GetDepartmentsByProfileId(profileId, page, pageSize);
            return Ok(response);
        }

        [HttpPost("{profileId}/Permissions")]
        public async Task<IActionResult> AddPermisssion(int profileId, [FromBody] int permissionId)
        {
            var response = await _smartTimeService.AddWindowPermissionToProfile(profileId, permissionId);
            return Ok(response);
        }
    }
}
