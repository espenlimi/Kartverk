using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Kartverk.Mvc.Models;

namespace Kartverk.Mvc.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> logger;

    public HomeController(ILogger<HomeController> logger)
    {
        this.logger = logger;
    }

    public IActionResult Index()
    {
        var model = new HomeViewModel();
        model.Message = "Det tar en time";

        return View("Index", model);
    }
    
    [HttpPost]
    public IActionResult Index(HomeViewModel model)
    {
        if(!ModelState.IsValid)
            return View("Index", model);    

        model.Message = model.NewMessage;
        model.NewMessage = null;
        return View("Index", model);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
