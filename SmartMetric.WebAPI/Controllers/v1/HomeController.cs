using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartMetric.Core.Enums;
using SmartMetric.Core.ServicesContracts;
using SmartMetric.WebAPI.Filters.ActionFilter;

namespace SmartMetric.WebAPI.Controllers.v1
{
    [ApiVersion("1.0")]
    public class HomeController : CustomBaseController
    {
        private readonly ISmartTimeService _smartTimeService;

        public HomeController(ISmartTimeService smartTimeService)
        {
            _smartTimeService = smartTimeService;
        }

        [PermissionRequired(WindowType.AdminSettings, PermissionType.Read)]
        [HttpGet("Windows")]
        public IActionResult GetAllWindows()
        {
            var response = _smartTimeService.GetAllWindows();

            return Ok(response);
        }
    }
}
