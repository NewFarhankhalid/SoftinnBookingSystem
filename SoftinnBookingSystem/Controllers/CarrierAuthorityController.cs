using SoftinnBookingSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoftinnBookingSystem.Controllers
{
    public class CarrierAuthorityController : Controller
    {
        // GET: CarrierAuthority
        public ActionResult Index()
        {
            string Sql = $@"Select * from CarrierAuthority";
            DataTable dt = General.FetchData(Sql);
            return View();
        }

        public ActionResult Create()
        {
            CarrierAuthority obj = new CarrierAuthority();
            return View(obj);
        }

        [HttpPost]
        public ActionResult Create(CarrierAuthority CA)
        {
            try
            {
                if (CA.CarrierAuthorityID == 0)
                {
                    string Query = "INSERT INTO CarrierAuthority (USDOT, MC, MCAuthorityDate) ";
                    Query += "VALUES ('" + CA.USDOT + "','" + CA.MC + "','" + CA.MCAuthorityDate + "')";
                    Query = Query + @" Select @@IDENTITY as CarrierAuthorityID";
                    General.ExecuteNonQuery(Query);
                }

                else
                {
                    string Query = "UPDATE [dbo].[CarrierAuthority] ";
                    Query += "SET [USDOT] = '" + CA.USDOT + "', ";
                    Query += "[MC] = '" + CA.MC + "', ";
                    Query += "[MCAuthorityDate] = '" + CA.MCAuthorityDate + "', ";
                    Query += "WHERE CarrierAuthorityID = " + CA.CarrierAuthorityID;
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
            DataTable dt = General.FetchData("Select * from CarrierAuthority Where CarrierAuthorityID= " + id);
            List<CarrierAuthority> lstCA = General.ConvertDataTable<CarrierAuthority>(dt);
            if (lstCA.Count > 0)
            {
                return View("Create", lstCA[0]);
            }
            return View("Index");
        }

        public ActionResult Delete(int id)
        {
            string Sql = "Delete from CarrierAuthority where CarrierAuthorityID=" + id;
            General.ExecuteNonQuery(Sql);
            return View("Index");
        }

    }
}