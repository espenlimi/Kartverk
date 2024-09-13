using Kartverk.Mvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace Kartverk.Mvc.Controllers;

public class MapCorrectionsController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Save(MapCorrectionModel model)
    {
        if (!ModelState.IsValid)
        {
            return View("Index",model);
        }
        return View("Index", model);
    }
}
