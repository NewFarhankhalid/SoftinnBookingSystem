using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SoftinnBookingSystem.Models
{
    public class CarrierFactoring
    {

        public int CarrierFactoringID { get; set; }
    
        public bool Factoring {  get; set; }
     
        public string FactoringCompanyName { get; set; }
        public string FactoringCompanyNumber { get; set; }
        public string FactoringCompanyPhone { get; set; }
        public string FactoringFax { get; set; }
        public string FactoringAddress { get; set; }
        public string AgentName { get; set; }
        public string AgentEmail { get; set; }
        public string AgentPhone { get; set; }


    }
}