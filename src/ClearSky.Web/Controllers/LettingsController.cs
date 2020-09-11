using System.Linq;
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

        public IActionResult Index()
        {
            var properties = _lettingsService.FetchPropertiesAsync();
            var viewModels = properties.Select(x => new PropertyViewModel(x));
            return View(viewModels);
        }
    }
}
