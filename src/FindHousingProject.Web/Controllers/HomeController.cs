using FindHousingProject.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;

namespace FindHousingProject.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly SimpleViewModel _vm;

        public HomeController(ILogger<HomeController> logger)
        {
            var rnd = new Random();

            _vm = new SimpleViewModel
            {
                Value = rnd.Next(minValue: 0, maxValue: 100)
            };

            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public IActionResult Index()
        {
            return View(_vm);
        }

        public IActionResult Another()
        {
            return View(_vm);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ViewModels.ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
