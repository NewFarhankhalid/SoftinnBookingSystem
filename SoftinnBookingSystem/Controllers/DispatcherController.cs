using SoftinnBookingSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoftinnBookingSystem.Controllers
{
    public class DispatcherController : Controller
    {
        // GET: Dispatcher

        
            public ActionResult Index()
            {
                DataTable dispatcherObj = General.FetchData("Select * from Dispatcher");
                List<Dispatcher> dispatcherList = DataTableToObject(dispatcherObj);
                ViewBag.Users = new DropDown().GetDispatcherInfo();
            ViewBag.BookingAgent = new DropDown().GetAssignedBookingAgent();
            // Set UserID ViewData here
            ViewBag.UserID = "UserID"; // You can set it to any value you want
                return View(dispatcherList);
            }

        [HttpPost]
        public ActionResult Save(Dispatcher objdispatcher, int UserID)
        {
            try
            {
                string query = string.Empty;
                DataTable existingRecord = General.FetchData("SELECT * FROM Dispatcher WHERE UserID = " + UserID);

                if (existingRecord.Rows.Count > 0)
                {
                    query = "UPDATE Dispatcher SET BaseSalary = " + objdispatcher.BaseSalary +
                            ", FuelAllowance = " + objdispatcher.FuelAllowance +
                            ", CommissionPercentage = " + objdispatcher.CommissionPercentage +
                            ", DiscordTag = '" + objdispatcher.DiscordTag +
                            "', BookingAgent = " + objdispatcher.BookingAgent +
                            " WHERE UserID = " + UserID;
                            General.ExecuteNonQuery(query);
                }
                else
                {
                    query = "INSERT INTO Dispatcher (UserID, BaseSalary, FuelAllowance, CommissionPercentage, DiscordTag, BookingAgent) " +
                            "VALUES (" + UserID +
                            ", " + objdispatcher.BaseSalary +
                            ", " + objdispatcher.FuelAllowance +
                            ", " + objdispatcher.CommissionPercentage +
                            ", '" + objdispatcher.DiscordTag +
                            "', " + objdispatcher.BookingAgent + ")";

                    General.ExecuteNonQuery(query);
                }

                ViewBag.Users = new DropDown().GetDispatcherInfo();
                ViewBag.BookingAgent = new DropDown().GetAssignedBookingAgent();
                return Json("true");
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }





        public ActionResult GetDispatcherInfo(int id)
        {
            int BaseSalary = 0;
            int FuelAllowance = 0;
            int CommissionPercentage = 0;
            string DiscordTag = "0";
           int BookingAgent = 0;
            DataTable sql = General.FetchData("SELECT u.*, d.* FROM Users u Inner JOIN Dispatcher d ON u.UserID = d.UserID WHERE u.UserID = " + id);
            if (sql.Rows.Count != 0)
            {
                BaseSalary = Convert.ToInt32(sql.Rows[0]["BaseSalary"]);
                FuelAllowance = Convert.ToInt32(sql.Rows[0]["FuelAllowance"]);
                CommissionPercentage = Convert.ToInt32(sql.Rows[0]["CommissionPercentage"]);
                DiscordTag = sql.Rows[0]["DiscordTag"].ToString();

                // Check for NULL value before converting to int
                object bookingAgentValue = sql.Rows[0]["BookingAgent"];
                if (bookingAgentValue != DBNull.Value)
                {
                    BookingAgent = Convert.ToInt32(bookingAgentValue);
                }
                else
                {
                    // Handle NULL case, for example, set BookingAgent to a default value
                    BookingAgent = 0; // or any other default value you prefer
                }
            }

            ViewBag.Users = new DropDown().GetDispatcherInfo();

            // Create an anonymous object or a dictionary to hold all the data
            var responseData = new
            {
                BaseSalary = BaseSalary,
                FuelAllowance = FuelAllowance,
                CommissionPercentage = CommissionPercentage,
                DiscordTag = DiscordTag,
                BookingAgent = BookingAgent
            };
            ViewBag.Users = new DropDown().GetDispatcherInfo();
            ViewBag.BookingAgent = new DropDown().GetAssignedBookingAgent();
            // Return the JSON data
            return Json(responseData, JsonRequestBehavior.AllowGet);
        }

        List<Dispatcher> DataTableToObject(DataTable dt)
        {
            List<Dispatcher> lstDispatcher = new List<Dispatcher>();
            Dispatcher bi;
            foreach (DataRow dr in dt.Rows)
            {
                bi = new Dispatcher();
                if (dr["DispatcherID"] != DBNull.Value)
                {
                    bi.DispatcherID = int.Parse(dr["DispatcherID"].ToString());
                }
                if (dr["UserID"] != DBNull.Value)
                {
                    bi.UserID = int.Parse(dr["UserID"].ToString());
                }
                if (dr["BaseSalary"] != DBNull.Value)
                {
                    bi.BaseSalary = int.Parse(dr["BaseSalary"].ToString());
                }
                if (dr["FuelAllowance"] != DBNull.Value)
                {
                    bi.FuelAllowance = int.Parse(dr["FuelAllowance"].ToString());
                }
                if (dr["CommissionPercentage"] != DBNull.Value)
                {
                    bi.CommissionPercentage = int.Parse((dr["CommissionPercentage"].ToString()));
                }
                if (dr["DiscordTag"] != DBNull.Value)
                {
                    bi.DiscordTag = (dr["DiscordTag"].ToString());
                }
                if (dr["BookingAgent"] != DBNull.Value)
                {
                    bi.BookingAgent = int.Parse(dr["BookingAgent"].ToString());
                }

                lstDispatcher.Add(bi);
            }
            return lstDispatcher;
        }


    }
}