using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using KartverketApiApp.Models;
using KartverketApiApp.Services;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly KommuneInfoService _kommuneInfoService;
    private readonly StedsnavnService _stedsnavnService;
   

    public HomeController(ILogger<HomeController> logger, KommuneInfoService kommuneInfoService, StedsnavnService stedsnavnService)
    {
        _logger = logger;
        _kommuneInfoService = kommuneInfoService;
        _stedsnavnService = stedsnavnService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> KommuneInfo(string kommuneNr)
    {
        // Kaller inn GetKommuneInfoAsync() metoden i KommuneInfoService klassen og beholder returnerte verdien i kommuneInfo variabelen
        var kommuneInfo = await _kommuneInfoService.GetKommuneInfoAsync(kommuneNr);


        if (kommuneInfo == null)
        {
            ViewData["Error"] = "Kommune not found.";
            return View("Index");
        }
        return View("KommuneInfo", kommuneInfo);
    }

    [HttpGet]
    public async Task<IActionResult> KommuneInfo()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Stedsnavn(string searchTerm)
    {
        var stedsnavnList = await _stedsnavnService.GetStedsnavnAsync(searchTerm);
        if (stedsnavnList == null)
        {
            ViewData["Error"] = "No place names found.";
            return View("Index");
        }
        return View("Stedsnavn", stedsnavnList);
    }

    [HttpGet]
    public async Task<IActionResult> Stedsnavn()
    {
        return View();
    }
}
