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
    public class VehicleController : Controller
    {
        // GET: Vehicle
        public ActionResult Index()
        {
            DataTable dt = General.FetchData($@"Select * from Vehicle");
            List<Vehicle> obj = DataTableToObject(dt);
            return View(obj);
        }
        public ActionResult Create()
        {
            Vehicle obj = new Vehicle();
         
            return View(obj);
        }

        [HttpPost]
        public ActionResult Create(Vehicle VC)
        {
            try
            {
                if (VC.VehicleID == 0)
                {
                    string query = "INSERT INTO Vehicle (VehicleName,VehicleNo,TrailerNo,  Length, Width, Hieght, WeightCapacity) ";
                    query += "VALUES ('" + VC.VehicleName + "','" + VC.VehicleNo + "','" + VC.TrailerNo + "',  " + VC.Length + ", " + VC.Width + ", " + VC.Hieght + ", " + VC.WeightCapacity + ")";
                    General.ExecuteNonQuery(query);
                }
                else
                {
                    string query = "UPDATE [dbo].[Vehicle] ";
                    query += "SET [VehicleName] = '" + VC.VehicleName + "', ";
                    query += "[VehicleNo] = '" + VC.VehicleNo + "' ";
                    query += "[TrailerNo] = '" + VC.TrailerNo + "' ";
                    query += "[Length] = " + VC.Length + ", ";
                    query += "[Width] = '" + VC.Width + "', ";
                    query += "[Hieght] = '" + VC.Hieght + "', ";
                    query += "[WeightCapacity] = '" + VC.WeightCapacity + "' ";
                    query += "WHERE VehicleID = " + VC.VehicleID;
                    General.ExecuteNonQuery(query);
                }

                return Json("true");
            }
            catch (Exception ex)
            {
                // Handle exceptions
                return Json(ex.Message);
            }
        }


        public ActionResult Edit(int Id)
        {
            DataTable dtCompany = General.FetchData("Select * from Vehicle where VehicleID=" + Id);
            List<Vehicle> lstCompany = DataTableToObject(dtCompany);
            if (lstCompany.Count > 0)
            {

              
                return View("Create", lstCompany[0]);
            }

            return View("Index");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            string SQL = "Delete From Vehicle where VehicleID=" + id;
            General.ExecuteNonQuery(SQL);
            return Json("true");
        }
        List<Vehicle> DataTableToObject(DataTable dt)
        {
            List<Vehicle> lstVehicle = new List<Vehicle>();
            Vehicle bi;
            foreach (DataRow dr in dt.Rows)
            {
                bi = new Vehicle();
                if (dr["VehicleID"] != DBNull.Value)
                {
                    bi.VehicleID = int.Parse(dr["VehicleID"].ToString());
                }
                if (dr["VehicleName"] != DBNull.Value)
                {
                    bi.VehicleName = (dr["VehicleName"].ToString());
                }
                if (dr["VehicleNo"] != DBNull.Value)
                {
                    bi.VehicleNo = (dr["VehicleNo"].ToString());
                }
                if (dr["TrailerNo"] != DBNull.Value)
                {
                    bi.TrailerNo = (dr["TrailerNo"].ToString());
                }
                if (dr["Length"] != DBNull.Value)
                {
                    bi.Length = int.Parse(dr["Length"].ToString());
                }
                if (dr["Width"] != DBNull.Value)
                {
                    bi.Width = int.Parse(dr["Width"].ToString());
                }
                if (dr["Hieght"] != DBNull.Value)
                {
                    bi.Hieght = int.Parse(dr["Hieght"].ToString());
                }
                if (dr["WeightCapacity"] != DBNull.Value)
                {
                    bi.WeightCapacity = int.Parse(dr["WeightCapacity"].ToString());
                }

                lstVehicle.Add(bi);
            }
            return lstVehicle;
        }

    }
}