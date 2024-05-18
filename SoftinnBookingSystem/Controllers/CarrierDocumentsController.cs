using SoftinnBookingSystem.Models;
using System;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace SoftinnBookingSystem.Controllers
{
    public class CarrierDocumentsController : Controller
    {
        // GET: CarrierDocuments
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            return View(file);
        }

        public ActionResult Create()
        {
            CarrierDocuments obj = new CarrierDocuments();
            return View(obj);
        }

        [HttpPost]
        public ActionResult Create(CarrierDocuments obj, HttpPostedFileBase MCFile, HttpPostedFileBase W9Forms, HttpPostedFileBase CarrierInsurance)
        {
            try
            {
                // Save files to the server folder
                if (MCFile != null)
                {
                    string MCFilePath = Path.Combine(Server.MapPath("~/Files"), Path.GetFileName(MCFile.FileName));
                    MCFile.SaveAs(MCFilePath);
                    obj.MCCertificate = MCFilePath;
                }
                if (W9Forms != null)
                {
                    string W9FormsFilePath = Path.Combine(Server.MapPath("~/Files"), Path.GetFileName(W9Forms.FileName));
                    W9Forms.SaveAs(W9FormsFilePath);
                    obj.W9Form = W9FormsFilePath;
                }
                if (CarrierInsurance != null)
                {
                    string CarrierInsuranceFilePath = Path.Combine(Server.MapPath("~/Files"), Path.GetFileName(CarrierInsurance.FileName));
                    CarrierInsurance.SaveAs(CarrierInsuranceFilePath);
                    obj.CertificateOfInsurance = CarrierInsuranceFilePath;
                }

                // Save file paths to the database
                string Query = "INSERT INTO CarrierDocuments (MCCertificate, W9Form, CertificateOfInsurance) ";
                Query += "VALUES ('" + obj.MCCertificate + "','" + obj.W9Form + "','" + obj.CertificateOfInsurance + "')";
                Query += "; SELECT @@IDENTITY as DocumentID";
                DataTable dt = General.FetchData(Query);
                obj.DocumentID = Convert.ToInt32(dt.Rows[0]["DocumentID"]);

                return Json(new { success = true, documentID = obj.DocumentID });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}
