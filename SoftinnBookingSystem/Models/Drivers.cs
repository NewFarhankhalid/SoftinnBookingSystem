using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftinnBookingSystem.Models
{
    public class Drivers
    {
        public int DriverID { get; set; }
        public string DriverName { get; set; }
        public string Carrier { get; set; }
        public string DriverEmail { get; set; }
        public string DriverPhone { get; set; }
        public string Address { get; set; }

    }
}