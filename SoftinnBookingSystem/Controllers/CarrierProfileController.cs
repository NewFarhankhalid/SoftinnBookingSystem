using SoftinnBookingSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoftinnBookingSystem.Controllers
{
 
    public class CarrierProfileController : Controller
    {
        // GET: CarrierProfile
        public ActionResult Index()
        {

            DataTable dtCarrierProfile = General.FetchData("SELECT * FROM CarrierProfile INNER JOIN CarrierAuthority ON CarrierProfile.CarrierID = CarrierAuthority.CarrierID");
            List<CarrierProfile> obj = DataTableToObjectIndex(dtCarrierProfile);
            return View(obj);
        }

        public ActionResult Create()
        {
            CarrierProfile obj = new CarrierProfile();
            ViewBag.PersonTaxClassification = new DropDown().PersonTaxClassification();
            ViewBag.LLCTaxClassification = new DropDown().LLCTaxClassification();
            obj.MCAuthorityDate= DateTime.Now;
            obj.ExpiryOfInsurance= DateTime.Now;
            obj.W9FormDate= DateTime.Now;

            return View(obj);
        }
        [HttpPost]
        public ActionResult Create(CarrierProfile CP, HttpPostedFileBase MCCertificate, HttpPostedFileBase W9Form, HttpPostedFileBase CertificateOfInsurance)
        {
            try
            {
                if (CP.CarrierID == 0)
                {
                    string Query = "INSERT INTO CarrierProfile (OwnerName, CompanyName, CommissionPercentage, Phone, Email, Address, City, State, ZIPCode, EnableWeeklyPaymentsReportings) ";
                    Query += "VALUES ('" + CP.OwnerName + "','" + CP.CompanyName + "'," + CP.CommissionPercentage + ",'" + CP.Phone + "','" + CP.Email + "','" + CP.Address + "','" + CP.City + "','" + CP.State + "','" + CP.ZipCode + "' ," + (CP.EnableWeeklyPaymentsReportings == true ? "1" : "0") + ")";
                    Query += "; SELECT @@IDENTITY as CarrierID";
                    DataTable dt = General.FetchData(Query);
                    CP.CarrierID = int.Parse(dt.Rows[0]["CarrierID"].ToString());

                    Query = "INSERT INTO CarrierAuthority (CarrierID, USDOT, MC, MCAuthorityDate) ";
                    Query += "VALUES (" + CP.CarrierID + ",'" + CP.USDOT + "','" + CP.MC + "','" + CP.MCAuthorityDate + "')";
                    if (Query != "")
                    {
                        General.ExecuteNonQuery(Query);
                    }

                    Query = "INSERT INTO CarrierW9Form (CarrierID, Name, BusinessName, PersonTaxClassification, LLCTaxClassification, SocialSecurityNumber, EmployerIdentificationNumber, W9FormDate) ";
                    Query += "VALUES (" + CP.CarrierID + ",'" + CP.Name + "','" + CP.BusinessName + "'," + CP.PersonTaxClassification + "," + CP.LLCTaxClassification + ",'" + CP.SocialSecurityNumber + "','" + CP.EmployerIdentificationNumber + "','" + CP.W9FormDate + "')";

                    if (Query != "")
                    {
                        General.ExecuteNonQuery(Query);
                    }

                    Query = "INSERT INTO CarrierInsurance (CarrierID, PolicyNumber, ExpiryOfInsurance, CompanyName, CompanyPhone, CompanyFax, CompanyAddress, AgentName, AgentEmail, AgentPhone) ";
                    Query += "VALUES (" + CP.CarrierID + ",'" + CP.InsurancepolicyNumber + "','" + CP.ExpiryOfInsurance + "','" + CP.CompanyName + "','" + CP.CompanyPhone + "','" + CP.CompanyFax + "','" + CP.CompanyAddress + "','" + CP.AgentName + "','" + CP.AgentEmail + "','" + CP.AgentPhone + "')";
                    if (Query != "")
                    {
                        General.ExecuteNonQuery(Query);
                    }

                    Query = "INSERT INTO CarrierFactoring (CarrierID, FactoringCompanyName, FactoringCompanyNumber, FactoringCompanyPhone, FactoringFax, FactoringAddress, FactoringAgentName, FactoringAgentEmail, FactoringAgentPhone) ";
                    Query += "VALUES (" + CP.CarrierID + ",'" + CP.FactoringCompanyName + "','" + CP.FactoringCompanyNumber + "','" + CP.FactoringCompanyPhone + "','" + CP.FactoringFax + "','" + CP.FactoringAddress + "','" + CP.FactoringAgentName + "','" + CP.FactoringAgentEmail + "','" + CP.FactoringAgentPhone + "')";
                    if (Query != "")
                    {
                        General.ExecuteNonQuery(Query);
                    }
                    if (MCCertificate != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(MCCertificate.FileName);
                        string fileExtension = Path.GetExtension(MCCertificate.FileName);
                        string uniqueFileName = fileName + "_" + Guid.NewGuid().ToString("N") + fileExtension;
                        CP.MCCertificate = Path.Combine(Server.MapPath("~/Files"), uniqueFileName);
                        MCCertificate.SaveAs(CP.MCCertificate);

                        //string MCFilePath = Path.Combine(Server.MapPath("~/Files"), Path.GetFileName(MCFile.FileName));
                        //MCFile.SaveAs(MCFilePath);
                        //CP.MCCertificate = MCFilePath;
                    }
                    if (W9Form != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(W9Form.FileName);
                        string fileExtension = Path.GetExtension(W9Form.FileName);
                        string uniqueFileName = fileName + "_" + Guid.NewGuid().ToString("N") + fileExtension;
                        CP.W9Form = Path.Combine(Server.MapPath("~/Files"), uniqueFileName);
                        W9Form.SaveAs(CP.W9Form);


                        //string W9FormsFilePath = Path.Combine(Server.MapPath("~/Files"), Path.GetFileName(W9Forms.FileName));
                        //W9Forms.SaveAs(W9FormsFilePath);
                        //CP.W9Form = W9FormsFilePath;
                    }
                    if (CertificateOfInsurance != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(CertificateOfInsurance.FileName);
                        string fileExtension = Path.GetExtension(CertificateOfInsurance.FileName);
                        string uniqueFileName = fileName + "_" + Guid.NewGuid().ToString("N") + fileExtension;
                        CP.CertificateOfInsurance = Path.Combine(Server.MapPath("~/Files"), uniqueFileName);
                        CertificateOfInsurance.SaveAs(CP.CertificateOfInsurance);

                        //string CarrierInsuranceFilePath = Path.Combine(Server.MapPath("~/Files"), Path.GetFileName(CarrierInsurance.FileName));
                        //CarrierInsurance.SaveAs(CarrierInsuranceFilePath);
                        //CP.CertificateOfInsurance = CarrierInsuranceFilePath;
                    }

                    // Save file paths to the database
                    Query = "INSERT INTO CarrierDocuments (CarrierID, MCCertificate, W9Form, CertificateOfInsurance) ";
                    Query += "VALUES ("+CP.CarrierID+",'" + CP.MCCertificate + "','" + CP.W9Form + "','" + CP.CertificateOfInsurance + "')";
                    if (Query != "")
                    {
                        General.ExecuteNonQuery(Query);
                    }

                    //    return Json(new { success = true, documentID = CP.DocumentID });
                    //}
                    //catch (Exception ex)
                    //{
                    //    return Json(new { success = false, message = ex.Message });
                    //}



                    return Json("true");
                }
                else
                {
                    string Query = "UPDATE [dbo].[CarrierProfile] ";
                    Query += "SET [OwnerName] = '" + CP.OwnerName + "', ";
                    Query += "[CompanyName] = '" + CP.CompanyName + "', ";
                    Query += "[CommissionPercentage] = '" + CP.CommissionPercentage + "', ";
                    Query += "[Phone] = '" + CP.Phone + "', ";
                    Query += "[Email] = '" + CP.Email + "', ";
                    Query += "[Address] = '" + CP.Address + "', ";
                    Query += "[City] = '" + CP.City + "', ";
                    Query += "[State] = '" + CP.State + "', ";
                    Query += "[ZIPCode] = '" + CP.ZipCode + "', ";
                    Query += "[EnableWeeklyPaymentsReportings] = " + (CP.EnableWeeklyPaymentsReportings == true ? "1" : "0") + " ";
                    Query += "WHERE CarrierID = " + CP.CarrierID;
                    General.ExecuteNonQuery(Query);

                    Query = "";
                    Query = "UPDATE [dbo].[CarrierAuthority] ";
                    Query += "SET [USDOT] = '" + CP.USDOT + "', ";
                    Query += "[MC] = '" + CP.MC + "', ";
                    Query += "[MCAuthorityDate] = '" + CP.MCAuthorityDate + "' ";
                    Query += "WHERE CarrierID = " + CP.CarrierID;
                    General.ExecuteNonQuery(Query);

                    Query = "";
                    Query = "UPDATE [dbo].[CarrierW9Form] ";
                    Query += "SET [Name] = '" + CP.Name + "', ";
                    Query += "[BusinessName] = '" + CP.BusinessName + "', ";
                    Query += "[PersonTaxClassification] = " + CP.PersonTaxClassification + ", ";
                    Query += "[LLCTaxClassification] = " + CP.LLCTaxClassification + ", ";
                    Query += "[SocialSecurityNumber] = '" + CP.SocialSecurityNumber + "', ";
                    Query += "[EmployerIdentificationNumber] = '" + CP.EmployerIdentificationNumber + "', ";
                    Query += "[W9FormDate] = '" + CP.W9FormDate + "' ";
                    Query += "WHERE CarrierID = " + CP.CarrierID;
                    General.ExecuteNonQuery(Query);

                    Query = "";
                    Query = "UPDATE [dbo].[CarrierInsurance] ";
                    Query += "SET [policyNumber] = '" + CP.InsurancepolicyNumber + "', ";
                    Query += "[ExpiryOfInsurance] = '" + CP.ExpiryOfInsurance + "', ";
                    Query += "[CompanyName] = '" + CP.InsuranceCompanyName + "', ";
                    Query += "[CompanyPhone] = '" + CP.CompanyPhone + "', ";
                    Query += "[CompanyFax] = '" + CP.CompanyFax + "', ";
                    Query += "[CompanyAddress] = '" + CP.CompanyAddress + "', ";
                    Query += "[AgentName] = '" + CP.AgentName + "', ";
                    Query += "[AgentEmail] = '" + CP.AgentEmail + "', ";
                    Query += "[AgentPhone] = '" + CP.AgentPhone + "' ";
                    Query += "WHERE CarrierID = " + CP.CarrierID;
                    General.ExecuteNonQuery(Query);

                    Query = "";
                    Query = "UPDATE [dbo].[CarrierFactoring] ";
                    Query += "SET [FactoringCompanyName] = '" + CP.FactoringCompanyName + "', ";
                    Query += "[FactoringCompanyNumber] = '" + CP.FactoringCompanyNumber + "', ";
                    Query += "[FactoringCompanyPhone] = '" + CP.FactoringCompanyPhone + "', ";
                    Query += "[FactoringFax] = '" + CP.FactoringFax + "', ";
                    Query += "[FactoringAddress] = '" + CP.FactoringAddress + "', ";
                    Query += "[FactoringAgentName] = '" + CP.FactoringAgentName + "', ";
                    Query += "[FactoringAgentEmail] = '" + CP.FactoringAgentEmail + "', ";
                    Query += "[FactoringAgentPhone] = '" + CP.FactoringAgentPhone + "' ";
                    Query += "WHERE CarrierID = " + CP.CarrierID;
                    General.ExecuteNonQuery(Query);


                    if (MCCertificate != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(MCCertificate.FileName);
                        string fileExtension = Path.GetExtension(MCCertificate.FileName);
                        string uniqueFileName = fileName + "_" + Guid.NewGuid().ToString("N") + fileExtension;
                        CP.MCCertificate = Path.Combine(Server.MapPath("~/Files"), uniqueFileName);
                        MCCertificate.SaveAs(CP.MCCertificate);

                        //string MCFilePath = Path.Combine(Server.MapPath("~/Files"), Path.GetFileName(MCFile.FileName));
                        //MCFile.SaveAs(MCFilePath);
                        //CP.MCCertificate = MCFilePath;
                    }
                    if (W9Form != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(W9Form.FileName);
                        string fileExtension = Path.GetExtension(W9Form.FileName);
                        string uniqueFileName = fileName + "_" + Guid.NewGuid().ToString("N") + fileExtension;
                        CP.W9Form = Path.Combine(Server.MapPath("~/Files"), uniqueFileName);
                        W9Form.SaveAs(CP.W9Form);


                        //string W9FormsFilePath = Path.Combine(Server.MapPath("~/Files"), Path.GetFileName(W9Forms.FileName));
                        //W9Forms.SaveAs(W9FormsFilePath);
                        //CP.W9Form = W9FormsFilePath;
                    }
                    if (CertificateOfInsurance != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(CertificateOfInsurance.FileName);
                        string fileExtension = Path.GetExtension(CertificateOfInsurance.FileName);
                        string uniqueFileName = fileName + "_" + Guid.NewGuid().ToString("N") + fileExtension;
                        CP.CertificateOfInsurance = Path.Combine(Server.MapPath("~/Files"), uniqueFileName);
                        CertificateOfInsurance.SaveAs(CP.CertificateOfInsurance);

                        //string CarrierInsuranceFilePath = Path.Combine(Server.MapPath("~/Files"), Path.GetFileName(CarrierInsurance.FileName));
                        //CarrierInsurance.SaveAs(CarrierInsuranceFilePath);
                        //CP.CertificateOfInsurance = CarrierInsuranceFilePath;
                    }

                    Query = "";
                    if(MCCertificate != null|| W9Form != null||CertificateOfInsurance!=null)
                    {
                        Query = "UPDATE [dbo].[CarrierDocuments] SET";
                        if (CP.MCCertificate != null)
                        {
                            Query += " [MCCertificate] = '" + CP.MCCertificate + "', ";
                        }
                        if (CP.W9Form != null)
                        {
                            Query += "[W9Form] = '" + CP.W9Form + "', ";
                        }
                        if (CP.CertificateOfInsurance != null)
                        {
                            Query += "[CertificateOfInsurance] = '" + CP.CertificateOfInsurance + "' ";
                        }
                        //Query += "[CertificateOfInsurance] = '" + CP.CertificateOfInsurance + "' ";
                        Query += "WHERE CarrierID = " + CP.CarrierID;
                        General.ExecuteNonQuery(Query);
                    }

                    // Save file paths to the database
                    //Query = "INSERT INTO CarrierDocuments (CarrierID, MCCertificate, W9Form, CertificateOfInsurance) ";
                    //Query += "VALUES (" + CP.CarrierID + ",'" + CP.MCCertificate + "','" + CP.W9Form + "','" + CP.CertificateOfInsurance + "')";
                    //if (Query != "")
                    //{
                    //    General.ExecuteNonQuery(Query);
                    //}



                    return Json("true");
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        private string SaveFileAndGetPath(HttpPostedFileBase file)
        {
            string fileName = Path.GetFileName(file.FileName);
            string filePath = Path.Combine(Server.MapPath("~/Files"), fileName);
            file.SaveAs(filePath);
            return filePath;
        }

        public ActionResult Edit(int id)
        {
            DataTable dt = General.FetchData("SELECT * FROM CarrierProfile INNER JOIN CarrierAuthority ON CarrierProfile.CarrierID = CarrierAuthority.CarrierID Inner join CarrierW9Form ON CarrierProfile.CarrierID = CarrierW9Form.CarrierID Inner join CarrierInsurance ON CarrierProfile.CarrierID = CarrierInsurance.CarrierID Inner join CarrierFactoring ON CarrierProfile.CarrierID = CarrierFactoring.CarrierID  Inner Join CarrierDocuments ON CarrierProfile.CarrierID = CarrierDocuments.CarrierID Where CarrierProfile.CarrierID= " + id);
            List<CarrierProfile> lstCP = DataTableToObject(dt);
            if (lstCP.Count > 0)
            {
                ViewBag.PersonTaxClassification = new DropDown().PersonTaxClassification(selectedvalue: lstCP[0].PersonTaxClassification);
                ViewBag.LLCTaxClassification = new DropDown().LLCTaxClassification(selectedvalue: lstCP[0].LLCTaxClassification);

                return View("Create", lstCP[0]);
               
            }
            return View("Index");
        }


        [HttpPost]
        public ActionResult Delete(int id)
        {
            string Sql = "Delete from CarrierProfile where CarrierID=" + id;
            General.ExecuteNonQuery(Sql);
            return View("Index");
        }

        List<CarrierProfile> DataTableToObjectIndex(DataTable dt)
        {
            List<CarrierProfile> lstCarrierProfile = new List<CarrierProfile>();

            foreach (DataRow dr in dt.Rows)
            {
                CarrierProfile bi = new CarrierProfile();

                if (dr["CarrierID"] != DBNull.Value)
                {
                    bi.CarrierID = int.Parse(dr["CarrierID"].ToString());
                }
                if (dr["USDOT"] != DBNull.Value)
                {
                    bi.USDOT = dr["USDOT"].ToString();
                }
                if (dr["MC"] != DBNull.Value)
                {
                    bi.MC = dr["MC"].ToString();
                }
                if (dr["CompanyName"] != DBNull.Value)
                {
                    bi.CompanyName = dr["CompanyName"].ToString();
                }

                lstCarrierProfile.Add(bi); 
            }

            return lstCarrierProfile;
        }



        List<CarrierProfile> DataTableToObject(DataTable dt)
        {
            List<CarrierProfile> lstCarrierProfile = new List<CarrierProfile>();
            CarrierProfile bi;
            foreach (DataRow dr in dt.Rows)
            {
                bi = new CarrierProfile();
                if (dr["CarrierID"] != DBNull.Value)
                {
                    bi.CarrierID = int.Parse(dr["CarrierID"].ToString());
                }
                if (dr["OwnerName"] != DBNull.Value)
                {
                    bi.OwnerName = (dr["OwnerName"].ToString());
                }
                if (dr["CompanyName"] != DBNull.Value)
                {
                    bi.CompanyName = (dr["CompanyName"].ToString());
                }
                if (dr["CommissionPercentage"] != DBNull.Value)
                {
                    bi.CommissionPercentage = int.Parse(dr["CommissionPercentage"].ToString());
                }
                if (dr["Phone"] != DBNull.Value)
                {
                    bi.Phone = (dr["Phone"].ToString());
                }
                if (dr["Email"] != DBNull.Value)
                {
                    bi.Email = (dr["Email"].ToString());
                }
                if (dr["Address"] != DBNull.Value)
                {
                    bi.Address = (dr["Address"].ToString());
                }
                if (dr["City"] != DBNull.Value)
                {
                    bi.City = (dr["City"].ToString());
                }
                if (dr["State"] != DBNull.Value)
                {
                    bi.State = (dr["State"].ToString());
                }
                if (dr["ZipCode"] != DBNull.Value)
                {
                    bi.ZipCode = (dr["ZipCode"].ToString());
                }
                if (dr["EnableWeeklyPaymentsReportings"] != DBNull.Value)
                {
                    bi.EnableWeeklyPaymentsReportings = bool.Parse(dr["EnableWeeklyPaymentsReportings"].ToString());
                }
                if (dr["USDOT"] != DBNull.Value)
                {
                    bi.USDOT = (dr["USDOT"].ToString());
                }
                if (dr["MC"] != DBNull.Value)
                {
                    bi.MC = (dr["MC"].ToString());
                }
                if (dr["MCAuthorityDate"] != DBNull.Value)
                {
                    bi.MCAuthorityDate = DateTime.Parse(dr["MCAuthorityDate"].ToString());
                }
                if (dr["Name"] != DBNull.Value)
                {
                    bi.Name = (dr["Name"].ToString());
                }
                if (dr["BusinessName"] != DBNull.Value)
                {
                    bi.BusinessName = (dr["BusinessName"].ToString());
                }
                if (dr["PersonTaxClassification"] != DBNull.Value)
                {
                    bi.PersonTaxClassification = int.Parse(dr["PersonTaxClassification"].ToString());
                }
                if (dr["LLCTaxClassification"] != DBNull.Value)
                {
                    bi.LLCTaxClassification = int.Parse(dr["LLCTaxClassification"].ToString());
                }
                if (dr["SocialSecurityNumber"] != DBNull.Value)
                {
                    bi.SocialSecurityNumber = (dr["SocialSecurityNumber"].ToString());
                }
                if (dr["EmployerIdentificationNumber"] != DBNull.Value)
                {
                    bi.EmployerIdentificationNumber = (dr["EmployerIdentificationNumber"].ToString());
                }
                if (dr["W9FormDate"] != DBNull.Value)
                {
                    bi.W9FormDate = DateTime.Parse(dr["W9FormDate"].ToString());
                }
                if (dr["policyNumber"] != DBNull.Value)
                {
                    bi.InsurancepolicyNumber = (dr["policyNumber"].ToString());
                }
                if (dr["ExpiryOfInsurance"] != DBNull.Value)
                {
                    bi.ExpiryOfInsurance = DateTime.Parse(dr["ExpiryOfInsurance"].ToString());
                }
                if (dr["CompanyName"] != DBNull.Value)
                {
                    bi.InsuranceCompanyName = (dr["CompanyName"].ToString());
                }
                if (dr["CompanyPhone"] != DBNull.Value)
                {
                    bi.CompanyPhone = (dr["CompanyPhone"].ToString());
                }
                if (dr["CompanyFax"] != DBNull.Value)
                {
                    bi.CompanyFax = (dr["CompanyFax"].ToString());
                }
                if (dr["CompanyAddress"] != DBNull.Value)
                {
                    bi.CompanyAddress = (dr["CompanyAddress"].ToString());
                }
                if (dr["AgentName"] != DBNull.Value)
                {
                    bi.AgentName = (dr["AgentName"].ToString());
                }
                if (dr["AgentEmail"] != DBNull.Value)
                {
                    bi.AgentEmail = (dr["AgentEmail"].ToString());
                }
                if (dr["AgentPhone"] != DBNull.Value)
                {
                    bi.AgentPhone = (dr["AgentPhone"].ToString());
                }
                if (dr["CompanyName"] != DBNull.Value)
                {
                    bi.FactoringCompanyName = (dr["CompanyName"].ToString());
                }

                if (dr["FactoringCompanyNumber"] != DBNull.Value)
                {
                    bi.FactoringCompanyNumber = (dr["FactoringCompanyNumber"].ToString());
                }
                if (dr["FactoringCompanyPhone"] != DBNull.Value)
                {
                    bi.FactoringCompanyPhone = (dr["FactoringCompanyPhone"].ToString());
                }
                if (dr["FactoringFax"] != DBNull.Value)
                {
                    bi.FactoringFax = (dr["FactoringFax"].ToString());
                }
                if (dr["FactoringAddress"] != DBNull.Value)
                {
                    bi.FactoringAddress = (dr["FactoringAddress"].ToString());
                }
                if (dr["FactoringAgentName"] != DBNull.Value)
                {
                    bi.FactoringAgentName = (dr["FactoringAgentName"].ToString());
                }
                if (dr["FactoringAgentEmail"] != DBNull.Value)
                {
                    bi.FactoringAgentEmail = (dr["FactoringAgentEmail"].ToString());
                }
                if (dr["FactoringAgentPhone"] != DBNull.Value)
                {
                    bi.FactoringAgentPhone = (dr["FactoringAgentPhone"].ToString());
                }
                if (dr["MCCertificate"] != DBNull.Value)
                {
                    bi.MCCertificate = (dr["MCCertificate"].ToString());
                }
                if (dr["W9Form"] != DBNull.Value)
                {
                    bi.W9Form = (dr["W9Form"].ToString());
                }
                if (dr["CertificateOfInsurance"] != DBNull.Value)
                {
                    bi.CertificateOfInsurance = (dr["CertificateOfInsurance"].ToString());
                }

                lstCarrierProfile.Add(bi);
            }
            return lstCarrierProfile;
        }

    }
}