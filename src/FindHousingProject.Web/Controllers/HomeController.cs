using FindHousingProject.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FindHousingProject.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly SimpleViewModel _vm;
        public HomeController()
        {
            var rnd = new Random();

            _vm = new SimpleViewModel
            {
                Value = rnd.Next(minValue: 0, maxValue: 100)
            };
        }
        public IActionResult Index()
        {

            return View(_vm);
        }

        public IActionResult Another()
        {
            return View(_vm);
        }
    }
}
