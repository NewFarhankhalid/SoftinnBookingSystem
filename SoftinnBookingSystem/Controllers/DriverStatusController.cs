using SoftinnBookingSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoftinnBookingSystem.Controllers
{
    public class DriverStatusController : Controller
    {
        // GET: DriverStatus
        public ActionResult Index()
        {
            DataTable dtDriverStatus = General.FetchData("select DriverStatus.*, Driver.DriverName from DriverStatus Inner Join Driver On Driver.DriverID = DriverStatus.DriverID ");
            List<DriverStatus> obj = DataTableToObject(dtDriverStatus);
            return View(obj);
        }

        public ActionResult Create()
        {
            DriverStatus obj = new DriverStatus();
            ViewBag.Driver = new DropDown().GetDriver();
            obj.FromDate = DateTime.Now;
            obj.ToDate = DateTime.Now;
            return View(obj);
        }

        [HttpPost]
        public ActionResult Create(DriverStatus driverStatus)
        {
            try
            {
                if (driverStatus.DriverStatusID == 0)
                {
                    string Query = "INSERT INTO DriverStatus (DriverID, InActive, FromDate, ToDate, Comments, LockDriver, EmergencyComments) ";
                    Query += "VALUES (" + driverStatus.DriverID + ", "
                        + (driverStatus.InActive ? "1" : "0") + ", '"
                        + driverStatus.FromDate.ToString("yyyy-MM-dd HH:mm:ss") + "', '"
                        + driverStatus.ToDate.ToString("yyyy-MM-dd HH:mm:ss") + "', '"
                        + driverStatus.Comments.Replace("'", "''") + "', "
                        + (driverStatus.LockDriver ? "1" : "0") + ", '"
                        + driverStatus.EmergencyComments.Replace("'", "''") + "')";
                    General.ExecuteNonQuery(Query);
                }
                else
                {
                    string Query = "UPDATE [dbo].[DriverStatus] ";
                    Query += "SET [DriverID] = " + driverStatus.DriverID + ", ";
                    Query += "[InActive] = " + (driverStatus.InActive ? "1" : "0") + ", ";
                    Query += "[FromDate] = '" + driverStatus.FromDate.ToString("yyyy-MM-dd HH:mm:ss") + "', ";
                    Query += "[ToDate] = '" + driverStatus.ToDate.ToString("yyyy-MM-dd HH:mm:ss") + "', ";
                    Query += "[Comments] = '" + driverStatus.Comments.Replace("'", "''") + "', ";
                    Query += "[LockDriver] = " + (driverStatus.LockDriver ? "1" : "0") + ", ";
                    Query += "[EmergencyComments] = '" + driverStatus.EmergencyComments.Replace("'", "''") + "' ";
                    Query += "WHERE [DriverStatusID] = " + driverStatus.DriverStatusID;
                    General.ExecuteNonQuery(Query);
                }
                return Json("true");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error: " + ex.Message);
                return View();
            }
        }
       

        public ActionResult Edit(int id)
        {
            DataTable dt = General.FetchData("Select * from DriverStatus Where DriverID= " + id);
            List<DriverStatus> lstDriverStatus = DataTableToObject(dt);
            if (lstDriverStatus.Count > 0)
            {
                ViewBag.Driver = new DropDown().GetDriver();
                return View("Create", lstDriverStatus[0]);
            }
            return View("Index");
        }

        public ActionResult Delete(int id)
        {
            string Sql = "Delete from DriverStatus where DriverID=" + id;
            General.ExecuteNonQuery(Sql);
            return View("Index");
        }

        List<DriverStatus> DataTableToObject(DataTable dt)
        {
            List<DriverStatus> lstDriverStatus = new List<DriverStatus>();
            DriverStatus bi;
            foreach (DataRow dr in dt.Rows)
            {
                bi = new DriverStatus();
                if (dr["DriverStatusID"] != DBNull.Value)
                {
                    bi.DriverStatusID = int.Parse(dr["DriverStatusID"].ToString());
                }
                if (dr["DriverID"] != DBNull.Value)
                {
                    bi.DriverID = int.Parse(dr["DriverID"].ToString());
                }
                if (dr["InActive"] != DBNull.Value)
                {
                    bi.InActive = bool.Parse(dr["InActive"].ToString());
                }
                if (dr["FromDate"] != DBNull.Value)
                {
                    bi.FromDate = DateTime.Parse(dr["FromDate"].ToString());
                }
                if (dr["ToDate"] != DBNull.Value)
                {
                    bi.ToDate = DateTime.Parse(dr["ToDate"].ToString());
                }
                if (dr["Comments"] != DBNull.Value)
                {
                    bi.Comments = (dr["Comments"].ToString());
                }

                if (dr["LockDriver"] != DBNull.Value)
                {
                    bi.LockDriver = bool.Parse(dr["LockDriver"].ToString());
                }
                if (dr["EmergencyComments"] != DBNull.Value)
                {
                    bi.EmergencyComments = (dr["EmergencyComments"].ToString());
                }
                bi.lstDriver = DriverAssociation(bi.DriverID);
                lstDriverStatus.Add(bi);
            }
            return lstDriverStatus;
        }

        List<Drivers> DriverAssociation(int DriverID)
        {
            List<Drivers> lstDriver = new List<Drivers>();
            DataTable dtVariant = General.FetchData(@"Select * from Driver Where DriverID=" + DriverID);
            foreach (DataRow dr in dtVariant.Rows)
            {
                Drivers pva = new Drivers();
                pva.DriverID = int.Parse(dr["DriverID"].ToString());
                pva.DriverName = dr["DriverName"].ToString();
                lstDriver.Add(pva);
            }
            return lstDriver;
        }

    }
}