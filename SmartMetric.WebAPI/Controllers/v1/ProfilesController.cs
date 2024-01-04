﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartMetric.Core.DTO;
using SmartMetric.Core.ServicesContracts;

namespace SmartMetric.WebAPI.Controllers.v1
{
    [ApiVersion("1.0")]
    public class ProfilesController : CustomBaseController
    {
        private readonly ISmartTimeService _smartTimeService;

        public ProfilesController(ISmartTimeService smartTimeService)
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
        public async Task<IActionResult> AddPermissions(int profileId, [FromBody] List<int> permissionIds)
        {
            var response = await _smartTimeService.AddWindowPermissionsToProfile(profileId, permissionIds);

            return Ok(response);
        }

    }
}
