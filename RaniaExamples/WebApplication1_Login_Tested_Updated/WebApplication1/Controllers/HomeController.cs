using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly GeoChangeService _geoChangeService;
        private readonly UserManager<IdentityUser> _userManager;

        // In-memory storage (if still needed)
        private static List<PositionModel> positions = new List<PositionModel>();
        private static List<AreaChange> changes = new List<AreaChange>();

        public HomeController(
            ILogger<HomeController> logger,
            GeoChangeService geoChangeService,
            UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _geoChangeService = geoChangeService;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(positions);
        }

        // Restrict access to logged-in users
        [Authorize]
        [HttpGet]
        public IActionResult RegisterAreaChange()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> RegisterAreaChange(string geoJson, string description)
        {
            try
            {
                if (string.IsNullOrEmpty(geoJson) || string.IsNullOrEmpty(description))
                {
                    return BadRequest("Invalid data.");
                }

                var user = await _userManager.GetUserAsync(User);
                var userId = user.Id;

                // Save to the database using Dapper
                _geoChangeService.AddGeoChange(description, geoJson, userId);

                // Redirect to the overview of changes
                return RedirectToAction("AreaChangeOverview");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while registering area change.");
                throw;
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> AreaChangeOverview()
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;

            var changes = _geoChangeService.GetAllGeoChanges(userId);
            return View(changes);
        }

        // New action methods for UpdateOverview feature
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> UpdateOverview()
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                var userId = user.Id;

                var allChanges = _geoChangeService.GetAllGeoChanges(userId);
                return View(allChanges);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving GeoChanges in UpdateOverview.");
                return View("Error");
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            _logger.LogInformation($"Edit GET action called with id={id}");

            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;

            var geoChange = _geoChangeService.GetGeoChangeById(id, userId);
            if (geoChange == null)
            {
                _logger.LogWarning($"GeoChange with id={id} not found for userId={userId}");
                return NotFound();
            }

            return View(geoChange);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(GeoChange model)
        {
            // Remove validation for UserId since it is set programmatically
            ModelState.Remove("UserId");

            // Get the current logged-in user
            var user = await _userManager.GetUserAsync(User);

            // Set the UserId programmatically
            model.UserId = user.Id;

            if (ModelState.IsValid)
            {
                _logger.LogInformation("ModelState is valid. Updating GeoChange.");

                // Proceed with updating the geo change
                _geoChangeService.UpdateGeoChange(model.Id, model.Description, model.GeoJson, user.Id);
                return RedirectToAction("UpdateOverview");
            }
            else
            {
                _logger.LogWarning("ModelState is invalid.");
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        _logger.LogWarning(error.ErrorMessage);
                    }
                }
            }

            return View(model);
        }




        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;

            var geoChange = _geoChangeService.GetGeoChangeById(id, userId);
            if (geoChange == null)
            {
                return NotFound();
            }
            return View(geoChange);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;

            _geoChangeService.DeleteGeoChange(id, userId);
            return RedirectToAction("UpdateOverview");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}
