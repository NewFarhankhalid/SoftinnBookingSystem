using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftinnBookingSystem.Models
{

    public class CarrierDocuments
    {
     public int DocumentID { get; set; }
        
        public string MCCertificate { get; set; }
        public string W9Form {  get; set; }
        public string CertificateOfInsurance { get; set; }
    }
}