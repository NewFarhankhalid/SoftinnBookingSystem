using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftinnBookingSystem.Models
{
    public class Assignment
    {
        public int AssignmentId { get; set; }
        public int DriverId { get; set; }
        public int UserId { get; set; }
    }
}