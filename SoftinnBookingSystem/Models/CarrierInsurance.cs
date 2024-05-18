using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftinnBookingSystem.Models
{
    public class CarrierInsurance
    {
        public int CarierInsuranceID { get; set; }
        public string policyNumber { get; set; }
        public DateTime ExpiryOfInsurance { get; set; }
        public string CompanyName { get; set; }
        public string CompanyPhone { get; set; }
        public string CompanyFax { get; set; }
        public string CompanyAddress { get; set; }
        public string AgentName { get; set; }
        public string AgentEmail { get; set; }
        public string AgentPhone { get; set; }
    }
}