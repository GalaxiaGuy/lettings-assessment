using System.Linq;
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
            var properties = _lettingsService.FetchPropertiesAsync(page);
            var pageCount = await _lettingsService.PageCountAsync().ConfigureAwait(false);
            var propertyViewModels = properties.Select(x => new PropertyViewModel(x));

            var viewModel = new PropertyCollectionViewModel(propertyViewModels, page, pageCount);
            return View(viewModel);
        }
    }
}
