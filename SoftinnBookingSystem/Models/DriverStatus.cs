using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftinnBookingSystem.Models
{
    public class DriverStatus
    {
        public int DriverStatusID { get; set; }
        public int DriverID { get; set; }
        public bool InActive { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Comments { get; set; }
        public bool LockDriver { get; set; }
        public string EmergencyComments { get; set; }
        public List<Drivers> lstDriver = new List<Drivers>();

    }
}