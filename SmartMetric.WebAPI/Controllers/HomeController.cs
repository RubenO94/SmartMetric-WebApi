using Microsoft.AspNetCore.Mvc;

namespace SmartMetric.WebAPI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
