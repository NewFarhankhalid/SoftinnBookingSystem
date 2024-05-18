using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftinnBookingSystem.Models
{
    public class CarrierAuthority
    {
        public int CarrierAuthorityID { get; set; }
       
        public string USDOT { get; set; }
        public string MC { get; set; }
        public DateTime MCAuthorityDate { get; }
    }
}