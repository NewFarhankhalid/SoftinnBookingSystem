using Microsoft.SqlServer.Server;
using SoftinnBookingSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace SoftinnBookingSystem.Controllers
{
    public class DriversController : BaseController
    {
        // GET: Drivers
        public ActionResult Index()
        {
            DataTable dt  = General.FetchData($@"
SELECT   d.*, c.CarrierId, c.CompanyName
FROM Driver d
INNER JOIN CarrierProfile c ON d.Carrier = c.CarrierId");
            List<Drivers> obj = DataTableToObject(dt);
            return View(obj);
        }
        public ActionResult Create()
        {
            Drivers obj = new Drivers();
            ViewBag.Accessory = new DropDown().GetAccessory();
            ViewBag.Vehicle = new DropDown().GetVehicle();
            ViewBag.CompanyName = new DropDown().GetCompanyName();
            return View(obj);
        }

        [HttpPost]
        public ActionResult Create(Drivers drivers, HttpPostedFileBase frontImage, HttpPostedFileBase backImage, string ExistingFrontImagePath, string ExistingBackImagePath)
        {
            try
            {
                // Handle image uploads
                if (frontImage != null && frontImage.ContentLength > 0)
                {
                    drivers.frontImage = SaveImage(frontImage);
                }
                else
                {
                    drivers.frontImage = !string.IsNullOrEmpty(ExistingFrontImagePath) ? Path.GetFileName(ExistingFrontImagePath) : null;
                }

                if (backImage != null && backImage.ContentLength > 0)
                {
                    drivers.backImage = SaveImage(backImage);
                }
                else
                {
                    drivers.backImage = !string.IsNullOrEmpty(ExistingBackImagePath) ? Path.GetFileName(ExistingBackImagePath) : null;
                }

                if (drivers.DriverID == 0)
                {
                    // Insert new driver and get the new DriverID
                    string query = "INSERT INTO Driver (AccessoryId, DriverName, Carrier, DriverEmail, DriverPhone, DriverAddress, frontImage, backImage) OUTPUT INSERTED.DriverID ";
                    query += "VALUES (" + drivers.AccessoryId + ", '" + drivers.DriverName + "', '" + drivers.Carrier + "', '" + drivers.DriverEmail + "', '" + drivers.DriverPhone + "', '" + drivers.DriverAddress + "', '" + drivers.frontImage + "', '" + drivers.backImage + "')";

                    DataTable dt = General.FetchData(query); // FetchData will execute the query and return the result
                    int newDriverId = Convert.ToInt32(dt.Rows[0]["DriverID"]);

                    // Insert into DriverVehicles for each selected VehicleID
                    foreach (var vehicleId in drivers.VehicleIDs)
                    {
                        string vehicleQuery = "INSERT INTO DriverVehicles (DriverID, VehicleID) VALUES (" + newDriverId + ", " + vehicleId + ")";
                        General.ExecuteNonQuery(vehicleQuery);
                    }

                    // Insert into DriversAccessories for each selected AccessoryID
                    foreach (var accessoryId in drivers.SelectedAccessoryIds)
                    {
                        string accessoryQuery = "INSERT INTO DriversAccessories (DriverID, id) VALUES (" + newDriverId + ", " + accessoryId + ")";
                        General.ExecuteNonQuery(accessoryQuery);
                    }
                }
                else
                {
                    // Remove existing vehicle associations
                    string deleteVehicleQuery = "DELETE FROM DriverVehicles WHERE DriverID = " + drivers.DriverID;
                    General.ExecuteNonQuery(deleteVehicleQuery);

                    // Remove existing accessory associations
                    string deleteAccessoryQuery = "DELETE FROM DriversAccessories WHERE DriverID = " + drivers.DriverID;
                    General.ExecuteNonQuery(deleteAccessoryQuery);

                    // Update existing driver
                    string query = "UPDATE Driver SET ";
                    query += "AccessoryId = " + drivers.AccessoryId + ", ";
                    query += "DriverName = '" + drivers.DriverName + "', ";
                    query += "Carrier = '" + drivers.Carrier + "', ";
                    query += "DriverEmail = '" + drivers.DriverEmail + "', ";
                    query += "DriverPhone = '" + drivers.DriverPhone + "', ";
                    query += "DriverAddress = '" + drivers.DriverAddress + "' ";

                    if (drivers.frontImage != null)
                    {
                        query += ", frontImage = '" + drivers.frontImage + "' ";
                    }

                    if (drivers.backImage != null)
                    {
                        query += ", backImage = '" + drivers.backImage + "' ";
                    }

                    query += "WHERE DriverID = " + drivers.DriverID;
                    General.ExecuteNonQuery(query);

                    // Insert into DriverVehicles for each selected VehicleID
                    foreach (var vehicleId in drivers.VehicleIDs)
                    {
                        string vehicleQuery = "INSERT INTO DriverVehicles (DriverID, VehicleID) VALUES (" + drivers.DriverID + ", " + vehicleId + ")";
                        General.ExecuteNonQuery(vehicleQuery);
                    }

                    // Insert into DriversAccessories for each selected AccessoryID
                    foreach (var accessoryId in drivers.SelectedAccessoryIds)
                    {
                        string accessoryQuery = "INSERT INTO DriversAccessories (DriverID, id) VALUES (" + drivers.DriverID + ", " + accessoryId + ")";
                        General.ExecuteNonQuery(accessoryQuery);
                    }
                }

                return Json("true");
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }


        private string SaveImage(HttpPostedFileBase imageFile)
        {
            if (imageFile != null && imageFile.ContentLength > 0)
            {
                string fileName = Path.GetFileName(imageFile.FileName);
                string uniqueFileName = Guid.NewGuid().ToString("N") + Path.GetExtension(fileName);
                string filePath = Path.Combine(Server.MapPath("~/Uploads"), uniqueFileName);
                imageFile.SaveAs(filePath);
                return uniqueFileName;
            }
            return null;
        }


        [HttpPost]
        public ActionResult AddVehicle(Vehicle objUser)
        {
            try
            {
                if (objUser.VehicleID == 0)
                {

                    string Query = "INSERT INTO Vehicle (VehicleName, Length, Width, Hieght, WeightCapacity) ";
                    Query += "VALUES ('" + objUser.VehicleName + "','" + objUser.Length + "','" + objUser.Width + "','" + objUser.Hieght + "','" + objUser.WeightCapacity + "')";
                    Query += "Select @@Identity as UserID";
                    General.ExecuteNonQuery(Query);
                }
                else
                {
                    string Query = "";
                    Query = Query + "UPDATE [dbo].[Vehicle] SET ";
                    Query = Query + "     [VehicleName] = '" + objUser.VehicleName + "' ";
                    Query = Query + "    ,[Length] = " + objUser.Length + " ";
                    Query = Query + "    ,[Width] = " + objUser.Width + " ";
                    Query = Query + "    ,[Hieght] = " + objUser.Hieght + " ";
                    Query = Query + "    ,[WeightCapacity] = " + objUser.WeightCapacity + " ";
                    Query = Query + "WHERE VehicleID=" + objUser.VehicleID;
                    General.ExecuteNonQuery(Query);
                }
                return Json("true");
            }
            catch
            {
                return View();
            }
        }
        [HttpGet]
        public ActionResult DriverRecommendation()
        {
            Drivers obj = new Drivers();
            ViewBag.Driver = new DropDown().GetDriver();
            return View(obj);
        }

        [HttpPost]
        public ActionResult DriverRecommendation(Drivers obj)
        {
            try
            {

                string Query = "UPDATE Driver SET Recommendation = '"
                               + obj.Recommendation.Replace("'", "''") + "' "
                               + "WHERE DriverID = " + obj.DriverID;

                General.ExecuteNonQuery(Query);

                return Json("true");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error: " + ex.Message);
                return View();
            }
        }
        [HttpGet]
        public ActionResult GetRecommendation(int driverID)
        {
            try
            {
                string query = "SELECT Recommendation FROM Driver WHERE DriverID = " + driverID;
                var dataTable = General.FetchData(query);

                string recommendation = null;
                if (dataTable.Rows.Count > 0)
                {
                    recommendation = dataTable.Rows[0]["Recommendation"].ToString();
                }

                // Return the recommendation as JSON
                return Json(recommendation, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error: " + ex.Message);
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }




        public ActionResult Edit(int Id)
        {
            DataTable dtDriver = General.FetchData("SELECT * FROM Driver WHERE DriverID = " + Id);
            List<Drivers> lstDriver = DataTableToObject(dtDriver);

            if (lstDriver.Count > 0)
            {
               
                ViewBag.Accessory = new DropDown().GetAccessory();
                ViewBag.CompanyName = new DropDown().GetCompanyName();
                ViewBag.Vehicle = new DropDown().GetVehicle();

                ViewBag.FrontImageUrl = Url.Content("~/Uploads/" + lstDriver[0].frontImage);
                ViewBag.BackImageUrl = Url.Content("~/Uploads/" + lstDriver[0].backImage);

               
                ViewBag.VehicleIDs = lstDriver[0].VehicleIDs;
                ViewBag.SelectedAccessoryIds = lstDriver[0].SelectedAccessoryIds;

                return View("Create", lstDriver[0]);
            }

            return View("Index");
        }




        [HttpPost]
        public ActionResult Delete(int id)
        {
            string sql1 = "Delete from DriversAccessories where DriverID=" + id;
            General.ExecuteNonQuery(sql1);
            sql1 = "";
            sql1 = "Delete from DriverVehicles where DriverID=" + id;
            General.ExecuteNonQuery(sql1);
            sql1 = "";
            sql1 = "Delete From Driver where DriverID=" + id;
            General.ExecuteNonQuery(sql1);
            return Json("true");
        }


        // AssignDrivers action method
        public ActionResult AssignDrivers()
        {
            // Fetch data for drivers, users, and assignments from the database
            DataTable driverData = General.FetchData("SELECT * FROM Driver");
            DataTable userData = General.FetchData("SELECT * FROM Users Where Roles like '%2%'");
            DataTable driverUserAssignmentData = General.FetchData("SELECT * FROM DriverUserAssignments");

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
            foreach (DataRow row in driverUserAssignmentData.Rows)
            {
                Assignment assignment = new Assignment
                {
                    DriverId = Convert.ToInt32(row["DriverId"]),
                    UserId = Convert.ToInt32(row["UserId"])
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
        public ActionResult AssignDrivers(List<AssignDrivers> assignments)
        {
            try
            {
                if (assignments != null && assignments.Count > 0)
                {
                    // Iterate through the assignments and clear existing assignments for each user
                    foreach (var assignment in assignments)
                    {
                        if (assignment != null)
                        {
                            ClearExistingAssignments(assignment.UserId);

                            //Insert new assignments
                            if (assignment.DriverIds != null)
                            {
                                foreach (var driverId in assignment.DriverIds)
                                {
                                    InsertAssignment(assignment.UserId, driverId);
                                }
                            }
                        }
                    }
                }

                // Redirect or return a view after successful saving
                return Json("true");
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"An error occurred while saving assignments: {ex.Message}");
                return Json(new { success = false, errorMessage = ex.Message });
            }
        }



        private void ClearExistingAssignments(int userId)
        {
            string deleteQuery = $"DELETE FROM DriverUserAssignments WHERE UserId = {userId}";
            General.ExecuteNonQuery(deleteQuery);
        }

        private void InsertAssignment(int userId, int driverId)
        {
            string insertQuery = $"INSERT INTO DriverUserAssignments (UserId, DriverId) VALUES ({userId}, {driverId})";
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
                if (dr["VehicleID"] != DBNull.Value)
                {
                    bi.VehicleID = int.Parse(dr["VehicleID"].ToString());
                }
                if (dr["AccessoryId"] != DBNull.Value)
                {
                    bi.AccessoryId = int.Parse(dr["AccessoryId"].ToString());
                }
                if (dr["DriverName"] != DBNull.Value)
                {
                    bi.DriverName = (dr["DriverName"].ToString());
                }
                if (dr["Carrier"] != DBNull.Value)
                {
                    bi.Carrier = int.Parse(dr["Carrier"].ToString());
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
                if (dr["frontImage"] != DBNull.Value)
                {
                    bi.frontImage = (dr["frontImage"].ToString());
                }
                if (dr["backImage"] != DBNull.Value)
                {
                    bi.backImage = (dr["backImage"].ToString());
                }

                bi.VehicleIDs = new List<int>();
                DataTable dtVehicleIds = General.FetchData("SELECT VehicleID FROM DriverVehicles WHERE DriverID = " + bi.DriverID);
                if (dtVehicleIds.Rows.Count > 0)
                {
                    foreach (DataRow vehicleRow in dtVehicleIds.Rows)
                    {
                        bi.VehicleIDs.Add(Convert.ToInt32(vehicleRow["VehicleID"]));
                    }
                }


                bi.SelectedAccessoryIds = new List<int>();
                // Fetch SelectedAccessoryIds for the driver
                DataTable dtAccessoryIds = General.FetchData("SELECT id FROM DriversAccessories WHERE DriverID = " + bi.DriverID);
                if (dtAccessoryIds.Rows.Count > 0)
                {
                    foreach (DataRow accessoryRow in dtAccessoryIds.Rows)
                    {
                        bi.SelectedAccessoryIds.Add(Convert.ToInt32(accessoryRow["id"]));
                    }
                }




                bi.lstCP = CarrierAssociation(bi.Carrier);
                lstDriver.Add(bi);
            }
            return lstDriver;
        }

        List<CarrierProfile> CarrierAssociation(int CarrierID)
        {
            List<CarrierProfile> lstCP = new List<CarrierProfile>();
            DataTable dtVariant = General.FetchData(@"Select * from CarrierProfile Where CarrierID=" + CarrierID);
            foreach (DataRow dr in dtVariant.Rows)
            {
                CarrierProfile pva = new CarrierProfile();
                pva.CarrierID = int.Parse(dr["CarrierID"].ToString());
                pva.CompanyName = dr["CompanyName"].ToString();
                lstCP.Add(pva);
            }
            return lstCP;
        }
    }
}