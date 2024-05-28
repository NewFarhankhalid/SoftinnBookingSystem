using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftinnBookingSystem.Models
{
    public class Drivers
    {
        public int DriverID { get; set; }
        public int VehicleID { get; set; }
        public int AccessoryId { get; set; }      

        public string DriverName { get; set; }
        public int Carrier { get; set; }
        public string DriverEmail { get; set; }
        public string DriverPhone { get; set; }
        public string DriverAddress { get; set; }

        public string frontImage { get; set; }
        public string backImage { get; set; }

        public string Recommendation { get; set; }
        public List<int> SelectedAccessoryIds { get; set; }
        public List<int> VehicleIDs { get; set; }

        public List<CarrierProfile> lstCP = new List<CarrierProfile>();
    }
}