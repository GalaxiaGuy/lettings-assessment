using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ClearSky.Infrastructure.Services;
using ClearSky.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ClearSky.Web.Controllers
{
    public class PropertyController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PropertyService _propertyService;

        public PropertyController(ILogger<HomeController> logger, PropertyService propertyService)
        {
            _logger = logger;
            _propertyService = propertyService;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var properties = userId == null
                ? _propertyService.FetchPropertiesAsync(page)
                : _propertyService.FetchPropertiesWithInterestsAsync(page);

            _propertyService.FetchPropertiesAsync(page);
            var pageCount = await _propertyService.PageCountAsync().ConfigureAwait(false);
            var propertyViewModels = properties.Select(x => new PropertyViewModel(x, userId));

            var viewModel = new PropertyCollectionViewModel(propertyViewModels, page, pageCount);
            return View(viewModel);
        }

        public async Task<IActionResult> ToggleInterest(string propertyId, int page=1)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var hasInterest = await _propertyService.ToggleInterestAsync(propertyId, userId).ConfigureAwait(false);
                TempData["Success"] = hasInterest
                    ? "Your interest has been noted"
                    : "Your interest has been removed";
            }
            catch
            {
                TempData["Error"] = "There was a problem setting your interest.";
            }
            return new RedirectResult(Url.Action("Index") + $"#{propertyId}");
        }
    }
}
