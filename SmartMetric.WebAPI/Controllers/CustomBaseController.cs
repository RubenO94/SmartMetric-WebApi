using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartMetric.WebAPI.Filters.ActionFilter;
using SmartMetric.WebAPI.Filters.ActionFilters;

namespace SmartMetric.WebAPI.Controllers
{
    [AllowAnonymous]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [RequestValidation]
    public class CustomBaseController : ControllerBase
    {
    }
}
