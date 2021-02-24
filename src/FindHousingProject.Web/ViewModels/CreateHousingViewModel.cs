using FindHousingProject.DAL.Entities;
using System;

namespace FindHousingProject.Web.ViewModels
{
    public class CreateHousingViewModel
    {
        public string  Name { get; set; }
        //public Country Place { get; set; } //https://www.jqueryscript.net/form/country-picker-flags.html
        public DateTime BookedFrom { get; set; }
        public DateTime BookedTo { get; set; }
        public decimal PricePerDay { get; set; }
        /// <summary>
        /// Scenery.
        /// </summary>
        public byte[] Scenery { get; set; }
        public string Description { get; set; }

        public string Address { get; set; }
    }
}