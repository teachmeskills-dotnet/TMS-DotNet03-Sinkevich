using FindHousingProject.BLL.Models;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace FindHousingProject.Web.ViewModels
{
    /// <summary>
    /// Housing view model.
    /// </summary>
    public class HousingViewModel
    {
        /// <summary>
        /// Housing Id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Housing name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Housing place.
        /// </summary>
        public string Place { get; set; }

        /// <summary>
        /// Price.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Scenery.
        /// </summary>
        public byte[] Scenery { get; set; }

        /// <summary>
        /// New scenery.
        /// </summary>
        [Display(Name = "Change scenery")]
        public IFormFile NewScenery { get; set; }

        /// <summary>
        /// Housing description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Housing address.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// User.
        /// </summary>
        public UserDto User { get; set; }
    }
}