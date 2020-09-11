using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ClearSky.Infrastructure.Services;
using ClearSky.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ClearSky.Web.Controllers
{
    public class LettingsController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly LettingsService _lettingsService;

        public LettingsController(ILogger<HomeController> logger, LettingsService lettingsService)
        {
            _logger = logger;
            _lettingsService = lettingsService;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var properties = userId == null
                ? _lettingsService.FetchPropertiesAsync(page)
                : _lettingsService.FetchPropertiesWithInterestsAsync(page);

            _lettingsService.FetchPropertiesAsync(page);
            var pageCount = await _lettingsService.PageCountAsync().ConfigureAwait(false);
            var propertyViewModels = properties.Select(x => new PropertyViewModel(x, userId));

            var viewModel = new PropertyCollectionViewModel(propertyViewModels, page, pageCount);
            return View(viewModel);
        }

        public async Task<IActionResult> ToggleInterest(string propertyId, int page=1)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                await _lettingsService.ToggleInterestAsync(propertyId, userId).ConfigureAwait(false);
            }
            catch
            {

            }
            return RedirectToAction("Index", new { page = page });
        }
    }
}
