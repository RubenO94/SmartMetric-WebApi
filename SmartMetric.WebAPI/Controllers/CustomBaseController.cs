using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartMetric.WebAPI.Filters.ActionFilter;
using SmartMetric.WebAPI.Filters.ActionFilters;

namespace SmartMetric.WebAPI.Controllers
{
    //[AllowAnonymous] // TODO: Retirar apos adicionar Autenticação de Utilizadores
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    //[SkipTokenValidation]
    //[RequestValidation]
    public class CustomBaseController : ControllerBase
    {
    }
}
