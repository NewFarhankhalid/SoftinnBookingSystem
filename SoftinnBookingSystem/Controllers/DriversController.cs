using Microsoft.SqlServer.Server;
using SoftinnBookingSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
            DataTable dt  = General.FetchData($@"Select * from Driver");
            List<Drivers> obj = DataTableToObject(dt);
            return View(obj);
        }
        public ActionResult Create()
        {
            Drivers obj = new Drivers();
            return View(obj);
        }
        [HttpPost]
        public ActionResult Create(Drivers Drivers)
        {
            try
            {
                if (Drivers.DriverID == 0)
                {
                    string Query = "INSERT INTO Driver (DriverName, Carrier, DriverEmail, DriverPhone, DriverAddress)";
                    Query += "VALUES ('" + Drivers.DriverName + "','" + Drivers.Carrier + "','" + Drivers.DriverEmail + "','" + Drivers.DriverPhone + "','" + Drivers.DriverAddress + "')";
                    General.ExecuteNonQuery(Query);
                }

                else
                {
                    string Query = "UPDATE [dbo].[Driver] ";
                    Query += "SET [DriverName] = '" + Drivers.DriverName + "', ";
                    Query += "[Carrier] = '" + Drivers.Carrier + "', ";
                    Query += "[DriverEmail] = '" + Drivers.DriverEmail + "', ";
                    Query += "[DriverPhone] = '" + Drivers.DriverPhone + "', ";
                    Query += "[DriverAddress] = '" + Drivers.DriverAddress + "' ";
                    Query += "WHERE DriverID = " + Drivers.DriverID;
                    General.ExecuteNonQuery(Query);
                }
                return Json("true");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Edit(int Id)
        {
            DataTable dtCompany = General.FetchData("Select * from Driver where DriverID=" + Id);
            List<Drivers> lstCompany = DataTableToObject(dtCompany);
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


        // AssignDrivers action method
        public ActionResult AssignDrivers()
        {
            // Fetch data for drivers, users, and assignments from the database
            DataTable driverData = General.FetchData("SELECT * FROM Driver");
            DataTable userData = General.FetchData("SELECT * FROM Users");
            DataTable assignmentData = General.FetchData("SELECT * FROM AssignDriver");

            // Populate the drivers list
            List<Drivers> drivers = new List<Drivers>();
            foreach (DataRow row in driverData.Rows)
            {
                Drivers driver = new Drivers
                {
                    DriverID = Convert.ToInt32(row["DriverID"]),
                    DriverName = row["DriverName"].ToString()
                };
                drivers.Add(driver);
            }

            // Populate the users list
            List<Users> users = new List<Users>();
            foreach (DataRow row in userData.Rows)
            {
                Users user = new Users
                {
                    UserID = Convert.ToInt32(row["UserID"]),
                    UserName = row["UserName"].ToString()
                };
                users.Add(user);
            }

            // Populate the assignments list
            List<Assignment> assignments = new List<Assignment>();
            foreach (DataRow row in assignmentData.Rows)
            {
                Assignment assignment = new Assignment
                {
                   DriverId = Convert.ToInt32(row["DriverID"]),
                    UserId = Convert.ToInt32(row["UserID"])
                };
                assignments.Add(assignment);
            }

            // Create a ViewModel to combine all lists
            AssignDrivers viewModel = new AssignDrivers
            {
                Drivers = drivers,
                Users = users,
                Assignments = assignments
            };

            // Pass the ViewModel to the view
            return View(viewModel);
        }



        [HttpPost]
        public ActionResult AssignDrivers(List<int> driverIds, int userId)
        {
            try
            {
                // Delete existing assignments for the user
                DeleteExistingAssignments(userId);

                // Insert new assignments into the database
                foreach (int driverId in driverIds)
                {
                    InsertAssignment(driverId, userId);
                }

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message });
            }
        }


        private void DeleteExistingAssignments(int userId)
        {
            string deleteQuery = "DELETE FROM AssignDriver WHERE UserID = " + userId;

            General.ExecuteNonQuery(deleteQuery);
        }

        private void InsertAssignment(int driverId, int userId)
        {
            // Construct the SQL query with embedded values
            string insertQuery = $"INSERT INTO AssignDriver (DriverID, UserID) VALUES ({driverId}, {userId})";

            // Execute the insert query using General.ExecuteNonQuery method
            General.ExecuteNonQuery(insertQuery);
        }





        List<Drivers> DataTableToObject(DataTable dt)
        {
            List<Drivers> lstDriver = new List<Drivers>();
            Drivers bi;
            foreach (DataRow dr in dt.Rows)
            {
                bi = new Drivers();
                if (dr["DriverID"] != DBNull.Value)
                {
                    bi.DriverID = int.Parse(dr["DriverID"].ToString());
                }
                if (dr["DriverName"] != DBNull.Value)
                {
                    bi.DriverName = (dr["DriverName"].ToString());
                }
                if (dr["Carrier"] != DBNull.Value)
                {
                    bi.Carrier = (dr["Carrier"].ToString());
                }
                if (dr["DriverEmail"] != DBNull.Value)
                {
                    bi.DriverEmail = (dr["DriverEmail"].ToString());
                }
                if (dr["DriverPhone"] != DBNull.Value)
                {
                    bi.DriverPhone = (dr["DriverPhone"].ToString());
                }
                if (dr["DriverAddress"] != DBNull.Value)
                {
                    bi.DriverAddress = (dr["DriverAddress"].ToString());
                }
               
                lstDriver.Add(bi);
            }
            return lstDriver;
        }
    }
}