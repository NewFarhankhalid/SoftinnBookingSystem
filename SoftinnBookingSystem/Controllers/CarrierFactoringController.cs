using SoftinnBookingSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoftinnBookingSystem.Controllers
{
    public class CarrierFactoringController : Controller
    {
        // GET: CarrierFactoring
        public ActionResult Index()
        {
            string Sql = $@"Select * from CarrierFactoring";
            DataTable dt = General.FetchData(Sql);
            return View();
        }
        public ActionResult Create()
        {
            CarrierFactoring obj = new CarrierFactoring();
            return View(obj);
        }


        [HttpPost]
        public ActionResult Create(CarrierFactoring objCarrierFactoring)
        {
            try
            {
                if (objCarrierFactoring.CarrierFactoringID == 0)
                {
                    string Query = "INSERT INTO CarrierFactoring (FactoringCompanyName, FactoringCompanyNumber, FactoringCompanyPhone, FactoringFax, FactoringAddress, AgentName, AgentEmail,AgentPhone) ";
                    Query += "VALUES ('" + objCarrierFactoring.FactoringCompanyName + "','" + objCarrierFactoring.FactoringCompanyNumber + "','" + objCarrierFactoring.FactoringCompanyPhone + "','" + objCarrierFactoring.FactoringFax + "','" + objCarrierFactoring.FactoringAddress + "','" + objCarrierFactoring.AgentName + "','" + objCarrierFactoring.AgentEmail + "','" + objCarrierFactoring.AgentPhone + "')";
                    General.ExecuteNonQuery(Query);
                }

                else
                {
                    string Query = "UPDATE [dbo].[CarrierFactoring] ";
                    Query += "SET [FactoringCompanyName] = '" + objCarrierFactoring.FactoringCompanyName + "', ";
                    Query += "[FactoringCompanyNumber] = '" + objCarrierFactoring.FactoringCompanyNumber + "', ";
                    Query += "[FactoringCompanyPhone] = '" + objCarrierFactoring.FactoringCompanyPhone + "', ";
                    Query += "[FactoringFax] = '" + objCarrierFactoring.FactoringFax + "', ";
                    Query += "[FactoringAddress] = '" + objCarrierFactoring.FactoringAddress + "', ";
                    Query += "[AgentName] = '" + objCarrierFactoring.AgentName + "', ";
                    Query += "[AgentEmail] = '" + objCarrierFactoring.AgentEmail + "' ";
                    Query += "[AgentPhone] = '" + objCarrierFactoring.AgentPhone + "' ";
                    Query += "WHERE CarierInsuranceID = " + objCarrierFactoring.CarrierFactoringID;
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
            DataTable dt = General.FetchData("Select * from CarrierFactoring Where CarrierFactoringID= " + id);
            List<CarrierFactoring> lstCarrierFactoring = General.ConvertDataTable<CarrierFactoring>(dt);
            if (lstCarrierFactoring.Count > 0)
            {
                return View("Create", lstCarrierFactoring[0]);
            }
            return View("Index");
        }
        public ActionResult Delete(int id)
        {
            string Sql = "Delete from CarrierFactoring where CarrierFactoringID=" + id;
            General.ExecuteNonQuery(Sql);
            return View("Index");
        }
    }
}