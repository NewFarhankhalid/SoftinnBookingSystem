using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftinnBookingSystem.Models
{
    public class Vehicle
    {
        public int VehicleID { get; set; }

        public int VehicleIDDropDown { get; set; }
        public string VehicleName { get; set; }
        public string VehicleNo { get; set; }
        public string TrailerNo { get; set; }
        public int Length { get; set;}

        public int Width { get; set; }
        public int Hieght { get; set; }
        public int WeightCapacity { get; set; }
    }
}