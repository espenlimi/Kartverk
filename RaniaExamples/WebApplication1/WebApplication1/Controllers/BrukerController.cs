using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class BrukerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
