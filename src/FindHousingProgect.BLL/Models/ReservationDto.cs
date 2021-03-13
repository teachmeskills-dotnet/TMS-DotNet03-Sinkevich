using System;
using System.Collections.Generic;
using System.Text;

namespace FindHousingProject.BLL.Models
{
   public class ReservationDto
    {
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public string Status { get; set; }
        

    }
}
