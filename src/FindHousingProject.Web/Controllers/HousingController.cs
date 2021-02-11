using FindHousingProject.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FindHousingProject.BLL.Models;

namespace FindHousingProject.Web.Controllers
{
    public class HousingController : Controller
    {
        private readonly ApplicationContext _context;

        public HousingController(ApplicationContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            return View();
        }

    }
}
