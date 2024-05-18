using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftinnBookingSystem.Models
{
    public class W9Form
    {
        public int W9FormID { get; set; }
       
        public string Name { get; set; }
        public string BusinessName { get; set; }
        public string PersonTaxClassification { get; set; }
        public string LLCTaxClassification { get; set; }
        public string SocialSecurityNumber{ get; set; }
        public string EmployerIdentificationNumber { get; set; }
        public DateTime W9FormDate { get; set; }
  
    }
}