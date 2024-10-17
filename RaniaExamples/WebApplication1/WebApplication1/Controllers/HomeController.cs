using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    { 
        private readonly ILogger<HomeController> _logger;
        // EF
        private readonly ApplicationDbContext _context;


        // EF
        private readonly ApplicationDbContext _context;

        // Definerer GeoChange tjeneste som bruker Dapper
        private readonly GeoChangeService _geoChangeService;

        // definerer en liste som en in-memory lagring 
        private static List<PositionModel> positions = new List<PositionModel>();
        private static List<AreaChange> changes = new List<AreaChange>();

<<<<<<< Updated upstream
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
=======

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, GeoChangeService geoChangeService)
        {
            _logger = logger;
            _context = context;
            _geoChangeService = geoChangeService;
>>>>>>> Stashed changes
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(positions);
        }

        // action metode som håndterer GET forespørsel og viser RegistrationForm.cshtml view
        [HttpGet]
        public ViewResult RegistrationForm()
        {
            return View();
        }

        // action metode som hånderer POST forespørsel og mottar data fra brukeren OGSÅ viser data oversikt
        [HttpPost]
        public ViewResult RegistrationForm(UserData userData)
        {
            return View("Overview", userData);
        }

        [HttpGet]
        public IActionResult CorrectMap()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CorrectMap(PositionModel model)
        {
            if (ModelState.IsValid)
            {
                // Legger ny posisjon til "positions" listen
                positions.Add(model);

                // viser oppsummering view etter data har blitt registrert og lagret i positions listen
                return View("CorrectionOverview", positions);
            }
            return View();
        }

        [HttpGet]
        public IActionResult CorrectionOverview()
        {
            return View(positions);
        }

        // Handle form submission to register a new change
        [HttpGet]
        public IActionResult RegisterAreaChange()
        {
            return View();
        }
        // Handle form submission to register a new change
       [HttpPost]
public IActionResult RegisterAreaChange(string geoJson, string description)
{
    // Without database connection
    //var newChange = new AreaChange
    //{
    //    Id = Guid.NewGuid().ToString(),
    //    GeoJson = geoJson,
    //    Description = description
    //};

    // Save the change in the static in-memory list
    //changes.Add(newChange);


    try
    {
        // Insert data using EF
        if (string.IsNullOrEmpty(geoJson) || string.IsNullOrEmpty(description))
        {
<<<<<<< Updated upstream
            return BadRequest("Invalid data.");
        }

        var newGeoChange = new GeoChange
        {
            GeoJson = geoJson,
            Description = description
        };

        // Save to the database using EF
        _context.GeoChanges.Add(newGeoChange);
        _context.SaveChanges();
        
            // Redirect to the overview of changes
            return RedirectToAction("AreaChangeOverview");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}, Inner Exception: {ex.InnerException?.Message}");
        throw;
    }
}

// Display the overview of registered changes
[HttpGet]
public IActionResult AreaChangeOverview()
{
    // Minne
    //return View(changes);
=======
            // Without database connection
            //var newChange = new AreaChange
            //{
            //    Id = Guid.NewGuid().ToString(),
            //    GeoJson = geoJson,
            //    Description = description
            //};

            // Save the change in the static in-memory list
            //changes.Add(newChange);


            try
            {
                // Insert data using EF
                if (string.IsNullOrEmpty(geoJson) || string.IsNullOrEmpty(description))
                {
                    return BadRequest("Invalid data.");
                }

                var newGeoChange = new GeoChange
                {
                    GeoJson = geoJson,
                    Description = description
                };

                // Save to the database using EF
                // _context.GeoChanges.Add(newGeoChange);
                // _context.SaveChanges();

                // Insert data using Dapper
                if (!string.IsNullOrEmpty(description) && !string.IsNullOrEmpty(geoJson))
                {
                    // Add a new geo change entry into the database using Dapper
                    _geoChangeService.AddGeoChange(description, geoJson);
                }
                    // Redirect to the overview of changes
                    return RedirectToAction("AreaChangeOverview");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}, Inner Exception: {ex.InnerException?.Message}");
                throw;
            }
        }

        // Display the overview of registered changes
        [HttpGet]
        public IActionResult AreaChangeOverview()
        {
            // Minne
            //return View(changes);

            // EF
            //var changes_db = _context.GeoChanges.ToList();
            //return View(changes_db);

            // Fetch all geo changes from the database using Dapper
            var dapper_changes = _geoChangeService.GetAllGeoChanges();
            return View(dapper_changes);
        }





        // Get the Edit form
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var geoChange = _geoChangeService.GetGeoChangeById(id);
            if (geoChange == null)
            {
                return NotFound();
            }
            return View(geoChange);
        }

        // Update the GeoChange
        [HttpPost]
        public IActionResult Edit(int geoChangeId, string description, string geoJsonData)
        {
            // Validate input
            if (geoChangeId > 0 && !string.IsNullOrEmpty(description) && !string.IsNullOrEmpty(geoJsonData))
            {
                // Update the record
                _geoChangeService.UpdateGeoChange(geoChangeId, description, geoJsonData);
                return RedirectToAction("UpdateOverview");
            }

            // In case of error, return the form with the current data
            var geoChange = _geoChangeService.GetGeoChangeById(geoChangeId);
            return View(geoChange);
        }

        // View summary of the updates
        public IActionResult UpdateOverview()
        {
            var allChanges = _geoChangeService.GetAllGeoChanges();
            return View(allChanges);  // Assuming you're passing all changes to the Overview view
        }

        // Get the Delete confirmation page
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var geoChange = _geoChangeService.GetGeoChangeById(id);
            if (geoChange == null)
            {
                return NotFound();
            }
            return View(geoChange);
        }

        // Delete the GeoChange
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            _geoChangeService.DeleteGeoChange(id);
            return RedirectToAction("UpdateOverview");
        }
>>>>>>> Stashed changes

    // EF
    var changes_db = _context.GeoChanges.ToList();
    return View(changes_db);

   
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
}


