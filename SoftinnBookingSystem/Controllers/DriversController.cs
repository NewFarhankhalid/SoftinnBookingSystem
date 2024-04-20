using SoftinnBookingSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoftinnBookingSystem.Controllers
{
    public class DriversController : BaseController
    {
        // GET: Drivers
        public ActionResult Index()
        {
            string sql = $@"Select * from Drivers";
            DataTable dt = General.FetchData(sql);
            return View();
        }
        public ActionResult Create()
        {
            Drivers obj = new Drivers();
            return View(obj);
        }
        [HttpPost]
        public ActionResult Create(Drivers Com)
        {
            try
            {
                if (Com.DriverID == 0)
                {
                    string Query = "Insert into Drivers (DriverName) ";
                    Query = Query + "Values ('" + Com.DriverName + "')";
                    General.ExecuteNonQuery(Query);
                }
                else
                {

                    string Query = "";
                    Query = Query + "UPDATE [dbo].[Drivers] ";
                    Query = Query + " SET    [DriverName] ='" + Com.DriverName + "' ";
                    Query = Query + "WHERE DriverID=" + Com.DriverID;
                    General.ExecuteNonQuery(Query);
                }
                // TODO: Add insert logic here
                return Json("true");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Edit(int Id)
        {
            DataTable dtCompany = General.FetchData("Select * from Drivers where DriverID=" + Id);
            List<Drivers> lstCompany = General.ConvertDataTable<Drivers>(dtCompany);
            if (lstCompany.Count > 0)
            {
                return View("Create", lstCompany[0]);
            }

            return View("Index");
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string SQL = "Delete From Drivers where DriverID=" + id;
            General.ExecuteNonQuery(SQL);
            return Json("true");

        }
    }
}