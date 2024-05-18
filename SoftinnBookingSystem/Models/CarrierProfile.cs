using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftinnBookingSystem.Models
{
    public class CarrierProfile
    {
        public int CarrierID { get; set; }
        public string OwnerName { get; set; }
        public string CompanyName{ get; set; }
        public int CommissionPercentage { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
         public bool EnableWeeklyPaymentsReportings { get; set; }
        public int CarrierAuthorityID { get; set; }

        public string USDOT { get; set; }
        public string MC { get; set; }
        public DateTime MCAuthorityDate { get; set; }

        public int W9FormID { get; set; }

        public string Name { get; set; }
        public string BusinessName { get; set; }
        public int PersonTaxClassification { get; set; }
        public int LLCTaxClassification { get; set; }
        public string SocialSecurityNumber { get; set; }
        public string EmployerIdentificationNumber { get; set; }
        public DateTime W9FormDate { get; set; }

        public int CarierInsuranceID { get; set; }
        public string InsurancepolicyNumber { get; set; }
        public DateTime ExpiryOfInsurance { get; set; }
        public string InsuranceCompanyName { get; set; }
        public string CompanyPhone { get; set; }
        public string CompanyFax { get; set; }
        public string CompanyAddress { get; set; }
        public string AgentName { get; set; }
        public string AgentEmail { get; set; }
        public string AgentPhone { get; set; }
        public int CarrierFactoringID { get; set; }

        public bool Factoring { get; set; }

        public string FactoringCompanyName { get; set; }
        public string FactoringCompanyNumber { get; set; }
        public string FactoringCompanyPhone { get; set; }
        public string FactoringFax { get; set; }
        public string FactoringAddress { get; set; }
        public string FactoringAgentName { get; set; }
        public string FactoringAgentEmail { get; set; }
        public string FactoringAgentPhone { get; set; }

        public int DocumentID { get; set; }

        public string MCCertificate { get; set; }
        public string W9Form { get; set; }
        public string CertificateOfInsurance { get; set; }


    }
}