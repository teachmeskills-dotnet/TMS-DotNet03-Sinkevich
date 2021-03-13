using FindHousingProject.BLL.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FindHousingProject.Web.ViewModels
{
    public class ReservationsViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Place { get; set; }
        public string HousingId { get; set; }
        public DateTime CheckIn{ get; set; }
        public DateTime CheckOut { get; set; }
        public decimal PricePerDay { get; set; }
        public string Address { get; set; }

        public UserDto User { get; set; }
        public string Status { get; set; }
        public decimal Amount { get; set; }
    }
}
