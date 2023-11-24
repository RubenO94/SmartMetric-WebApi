using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SmartMetric.WebAPI.Controllers
{
    [AllowAnonymous] // TODO: Retirar apos adicionar Autenticação de Utilizadores
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class CustomBaseController : ControllerBase
    {
    }
}
