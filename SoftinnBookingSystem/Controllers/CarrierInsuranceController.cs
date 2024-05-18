using SoftinnBookingSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoftinnBookingSystem.Controllers
{
    public class CarrierInsuranceController : Controller
    {
        // GET: CarrierInsurance
        public ActionResult Index()
        {
            string Sql = $@"Select * from CarrierInsurance";
            DataTable dt = General.FetchData(Sql);
            return View();
        }

        public ActionResult Create()
        {
            CarrierInsurance obj = new CarrierInsurance();
            return View(obj);
        }

        [HttpPost]
        public ActionResult Create(CarrierInsurance objCarrierInsurance)
        {
            try
            {
                if (objCarrierInsurance.CarierInsuranceID == 0)
                {
                    string Query = "INSERT INTO CarrierInsurance (policyNumber, ExpiryOfInsurance, CompanyName, CompanyPhone, CompanyFax, CompanyAddress, AgentName,AgentEmail,AgentPhone) ";
                    Query += "VALUES ('" + objCarrierInsurance.policyNumber + "','" + objCarrierInsurance.ExpiryOfInsurance + "','" + objCarrierInsurance.CompanyName + "','" + objCarrierInsurance.CompanyPhone + "','" + objCarrierInsurance.CompanyFax + "','" + objCarrierInsurance.CompanyFax + "','" + objCarrierInsurance.AgentName + "','" + objCarrierInsurance.AgentEmail + "','" + objCarrierInsurance.AgentPhone + "')";
                    General.ExecuteNonQuery(Query);
                }

                else
                {
                    string Query = "UPDATE [dbo].[CarrierInsurance] ";
                    Query += "SET [policyNumer] = '" + objCarrierInsurance.policyNumber + "', ";
                    Query += "[ExpiryOfInsurance] = '" + objCarrierInsurance.ExpiryOfInsurance + "', ";
                    Query += "[CompanyName] = '" + objCarrierInsurance.CompanyName + "', ";
                    Query += "[CompanyPhone] = '" + objCarrierInsurance.CompanyPhone + "', ";
                    Query += "[CompanyFax] = '" + objCarrierInsurance.CompanyFax + "', ";
                    Query += "[CompanyAddress] = '" + objCarrierInsurance.CompanyAddress + "', ";
                    Query += "[AgentName] = '" + objCarrierInsurance.AgentName + "' ";
                    Query += "[AgentEmail] = '" + objCarrierInsurance.AgentEmail + "' ";
                    Query += "[AgentPhone] = '" + objCarrierInsurance.AgentPhone + "' ";
                    Query += "WHERE CarierInsuranceID = " + objCarrierInsurance.CarierInsuranceID;
                    General.ExecuteNonQuery(Query);
                }
                return Json("true");
            }
            catch
            {
                return View();
            }
        }                                                       

        public ActionResult Edit(int id)
        {
            DataTable dt = General.FetchData("Select * from CarrierInsurance Where CarierInsuranceID= " + id);
            List<CarrierInsurance> lstCarrierInsurance = General.ConvertDataTable<CarrierInsurance>(dt);
            if (lstCarrierInsurance.Count > 0)
            {
                return View("Create", lstCarrierInsurance[0]);
            }
            return View("Index");
        }

        public ActionResult Delete(int id)
        {
            string Sql = "Delete from CarrierInsurance where CarierInsuranceID=" + id;
            General.ExecuteNonQuery(Sql);
            return View("Index");
        }

    }
}