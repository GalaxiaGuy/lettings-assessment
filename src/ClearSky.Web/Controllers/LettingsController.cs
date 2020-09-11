using System.Diagnostics;
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

        public IActionResult Test()
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
