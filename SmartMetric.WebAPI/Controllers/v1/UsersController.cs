using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Enums;
using SmartMetric.Core.ServicesContracts;
using SmartMetric.Infrastructure.Models;

namespace SmartMetric.WebAPI.Controllers.v1
{
    [AllowAnonymous]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ISmartTimeService _smartTimeService;

        public UsersController(ISmartTimeService smartTimeService)
        {
            _smartTimeService = smartTimeService;
        }
        [HttpGet("Me")]
        public async Task<IActionResult> GetPerfil()
        {
            if (HttpContext.Items.TryGetValue("ApplicationUserType", out var userTypeObj) && userTypeObj is ApplicationUserType applicationUserType)
            {
                if(HttpContext.Items.TryGetValue("UserId", out var userIdObj) && userIdObj is int userId)
                {
                    var perfil = await _smartTimeService.GetProfileByUserId(applicationUserType, userId);
                    return Ok(perfil);
                }
            }

            throw new ArgumentException("Unidentified user", "Application User");
           
        } 
    }
}
