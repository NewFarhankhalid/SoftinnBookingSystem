using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftinnBookingSystem.Models
{
    public class Brooker
    {
        public int BrookerID { get; set; }
        public string BrookerMC { get; set; }
        public string BrookerUsDot { get; }
        public string BrookerBusinessName { get; set; }
        public string BrookerEmail { get; set; }
        public string BrookerPhone { get; }
        public string BrookerFax { get; }
        public string BrookerAddress { get; }
    }
}