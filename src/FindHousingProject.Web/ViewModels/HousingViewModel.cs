using FindHousingProject.BLL.Models;
using FindHousingProject.DAL.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace FindHousingProject.Web.ViewModels
{
    public class HousingViewModel
    {
        public string Id { get; set; }
        public string  Name { get; set; }
        public string Place { get; set; }
        //public Country Place { get; set; } //https://www.jqueryscript.net/form/country-picker-flags.html
        public DateTime BookedFrom { get; set; }
        public DateTime BookedTo { get; set; }
        public decimal PricePerDay { get; set; }
        /// <summary>
        /// Scenery.
        /// </summary>
        public byte[] Scenery { get; set; }
        /// <summary>
        /// New scenery.
        /// </summary>
        [Display(Name = "Change scenery")]
        public IFormFile NewScenery { get; set; }
        //public List<SelectListItem> Roles { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public UserDto User { get; set; }
    }
}