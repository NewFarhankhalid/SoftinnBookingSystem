using SoftinnBookingSystem.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

namespace SoftinnBookingSystem.Controllers
{
    public class LoadController : Controller
    {
        // GET: Load
        public ActionResult Index(string preset)
        {
            string query = "";

            switch (preset)
            {
                case "today":
                    query = "SELECT * FROM Load WHERE CONVERT(date, DateCreated) = CONVERT(date, GETDATE())";
                    break;
                case "yesterday":
                    query = "SELECT * FROM Load WHERE CONVERT(date, DateCreated) = CONVERT(date, DATEADD(day, -1, GETDATE()))";
                    break;
                case "last_7_days":
                    query = "SELECT * FROM Load WHERE DateCreated >= DATEADD(day, -7, GETDATE())";
                    break;
                case "current_month":
                    query = "SELECT * FROM Load WHERE YEAR(DateCreated) = YEAR(GETDATE()) AND MONTH(DateCreated) = MONTH(GETDATE())";
                    break;
                default:
                    // Fetch all data if no preset is selected
                    query = "SELECT ID,LoadID,LoadPickupDateTime,PaymentType,OwnerName,CompanyName, DriverName,LoadRate,CarrierProfile.CommissionPercentage,(LoadRate/LoadDistance)RPM,LoadDropOffDateTime,LoadOrigin,LoadDestination,ChangeLoadStatus,DateCreated , Load.*  FROM Load inner join Driver  on Load.Driver = Driver.DriverID inner join CarrierProfile on Driver.Carrier = CarrierProfile.CarrierID";
                    break;
            }
        
            DataTable dt = General.FetchData(query);
            List<Load> obj = DataTableToObject(dt);

            ViewBag.Load = new DropDown().GetLoadStatus();
            ViewBag.Invoice = new DropDown().GetInvoiceStatus();
            ViewBag.Dispatcher = new DropDown().GetDispatcher();
            ViewBag.Broker = new DropDown().GetBroker();
            ViewBag.Driver = new DropDown().GetDriver();
            ViewBag.Comodity = new DropDown().GetComodity();
            ViewBag.LoadPickupDateTime = DateTime.Now.ToString("yyyy-MM-dd");
            ViewBag.LoadDropOffDateTime = DateTime.Now.ToString("yyyy-MM-dd");
            if(preset.IsEmpty())
            {
                return View(obj);
            }
            else
            {
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Create()
        {
            Load obj = new Load();
            ViewBag.LoadType = new DropDown().GetLoadType();
            ViewBag.PaymentType = new DropDown().GetPaymentType();
            ViewBag.BookingAgent = new DropDown().GetAssignedBookingAgent();
            ViewBag.Broker = new DropDown().GetBroker();
            ViewBag.Driver = new DropDown().GetDriver();
            ViewBag.Comodity = new DropDown().GetComodity();
            obj.LoadDropOffDateTime = DateTime.Now;
            obj.LoadPickupDateTime = DateTime.Now;
            return View(obj);
        }

        [HttpPost]
        public ActionResult Create(Load objLoad)
        {
            try
            {
                if (objLoad.ID == 0)
                {
                    string Query = "INSERT INTO Load (DateCreated,Driver ,Carrier, MC ,LoadOrigin, LoadDestination, ChangeLoadStatus ,LoadWeight, LoadLength, LoadRate, LoadDistance, LoadType, PaymentType, LoadID, Comodity , Comments, Shipper, AgentName ,AgentPhone, LoadPickupDateTime, PickupAddress, PersonAtPickup, ContactPhone, PickupInstructions, Consignee, LoadDropOffDateTime, DropOffAddress, PersonAtPickupAtDelivery, ContactPhoneAtDelivery, PickupInstructionsAtDelivery) ";
                    Query += "VALUES (GetDate()," + objLoad.Driver + ",'" + objLoad.Carrier + "','" + objLoad.MC + "','" + objLoad.LoadOrigin + "','" + objLoad.LoadDestination + "'," + objLoad.ChangeLoadStatus + "," + objLoad.LoadWeight + "," + objLoad.LoadLength + "," + objLoad.LoadRate + "," + objLoad.LoadDistance + "," + objLoad.LoadType + "," + objLoad.PaymentType + " ," + objLoad.LoadID + ", " + objLoad.Comodity + ",'" + objLoad.Comments + "'," + objLoad.Shipper + ",'" + objLoad.AgentName + "','" + objLoad.AgentPhone + "','" + objLoad.LoadPickupDateTime + "','" + objLoad.PickupAddress + "','" + objLoad.PersonAtPickup + "','" + objLoad.ContactPhone + "','" + objLoad.PickupInstructions + "','" + objLoad.Consignee + "','" + objLoad.LoadDropOffDateTime + "','" + objLoad.DropOffAddress + "','" + objLoad.PersonAtPickupAtDelivery + "','" + objLoad.ContactPhoneAtDelivery + "','" + objLoad.PickupInstructionsAtDelivery + "')";
                    Query = Query + @" Select @@IDENTITY as ID";
                    General.ExecuteNonQuery(Query);
                }
                else
                {
                    string Query = "UPDATE [dbo].[Load] ";
                    Query += "SET [Driver] = " + objLoad.Driver + ", ";
                    Query += "[Carrier] = '" + objLoad.Carrier + "', ";
                    Query += "[MC] = '" + objLoad.MC + "', ";
                    Query += "[LoadOrigin] = '" + objLoad.LoadOrigin + "', ";
                    Query += "[LoadDestination] = '" + objLoad.LoadDestination + "', ";
                    Query += "[ChangeLoadStatus] = " + objLoad.ChangeLoadStatus + ", ";
                    Query += "[LoadWeight] = " + objLoad.LoadWeight + ", ";
                    Query += "[LoadLength] = " + objLoad.LoadLength + ", ";
                    Query += "[LoadRate] = " + objLoad.LoadRate + ", ";
                    Query += "[LoadDistance] = " + objLoad.LoadDistance + " ";
                    Query += "[LoadType] = " + objLoad.LoadType + " ";
                    Query += "[PaymentType] = " + objLoad.PaymentType + " ";
                    Query += "[LoadID] = " + objLoad.LoadID + " ";
                    Query += "[Comodity] = " + objLoad.Comodity + " ";
                    Query += "[Comments] = '" + objLoad.Comments + "' ";
                    Query += "[Shipper] = " + objLoad.Shipper + ", ";
                    Query += "[AgentName] = '" + objLoad.AgentName + "' ";
                    Query += "[AgentPhone] = '" + objLoad.AgentPhone + "' ";
                    Query += "[LoadPickupDateTime] = '" + objLoad.LoadPickupDateTime + "', ";
                    Query += "[PickupAddress] = '" + objLoad.PickupAddress + "', ";
                    Query += "[PersonAtPickup] = '" + objLoad.PersonAtPickup + "', ";
                    Query += "[ContactPhone] = '" + objLoad.ContactPhone + "', ";
                    Query += "[PickupInstructions] = '" + objLoad.PickupInstructions + "', ";
                    Query += "[Consignee] = '" + objLoad.Consignee + "', ";
                    Query += "[LoadDropOffDateTime] = '" + objLoad.LoadDropOffDateTime + "', ";
                    Query += "[DropOffAddress] = '" + objLoad.DropOffAddress + "', ";
                    Query += "[PersonAtPickupAtDelivery] = '" + objLoad.PersonAtPickupAtDelivery + "', ";
                    Query += "[ContactPhoneAtDelivery] = '" + objLoad.ContactPhoneAtDelivery + "', ";
                    Query += "[PickupInstructionsAtDelivery] = '" + objLoad.PickupInstructionsAtDelivery + "', ";
                    Query += "WHERE ID = " + objLoad.ID;
                    General.ExecuteNonQuery(Query);
                }
                return Json("true");
            }
            catch
            {
                return View();
            }
        }



        [HttpPost]
        public ActionResult AddComodity(Comodity objComodity)
        {
            try
            {

                if (objComodity.Id == 0)
                {
                    string Query = "Insert into Comodity (Name) ";
                    Query = Query + "Values ('" + objComodity.Name + "')";
                    //General.ExecuteNonQuery(Query);
                    //Query = "";
                    Query = Query + " Select @@IDENTITY as Id";
                    objComodity.Id = int.Parse(General.FetchData(Query).Rows[0]["Id"].ToString());

                }
                else
                {
                    string Query = "";
                    Query = Query + "UPDATE [dbo].[Comodity] ";
                    Query = Query + " SET    [Name] ='" + objComodity.Name + "' ";
                    Query = Query + " WHERE Id=" + objComodity.Id;
                    General.FetchData(Query);


                }
                return Json("true," + objComodity.Id);
            }
            catch
            {
                return Json("error");
            }
        }


        [HttpPost]
        public ActionResult AddBroker(Brooker Brooker)
        {
            try
            {
                if (Brooker.BrookerID == 0)
                {
                    string Query = "INSERT INTO Brooker (BrookerMC, BrookerUSDot, BrookerBusinessName, BrookerEmail, BrookerPhone,  BrookerAddress) ";
                    Query += "VALUES ('" + Brooker.BrookerMC + "','" + Brooker.BrookerUsDot + "','" + Brooker.BrookerBusinessName + "','" + Brooker.BrookerEmail + "','" + Brooker.BrookerPhone + "','" + Brooker.BrookerAddress + "')";
                    General.ExecuteNonQuery(Query);
                }

                else
                {
                    string Query = "UPDATE [dbo].[Brooker] ";
                    Query += "SET [BrookerMC] = '" + Brooker.BrookerMC + "', ";
                    Query += "[BrookerUsDot] = '" + Brooker.BrookerUsDot + "', ";
                    Query += "[BrookerBusinessName] = '" + Brooker.BrookerBusinessName + "', ";
                    Query += "[BrookerEmail] = '" + Brooker.BrookerEmail + "', ";
                    Query += "[BrookerPhone] = '" + Brooker.BrookerPhone + "', ";
                    Query += "[BrookerAddress] = '" + Brooker.BrookerAddress + "' ";
                    Query += "WHERE BrookerID = " + Brooker.BrookerID;
                    General.ExecuteNonQuery(Query);
                }
                return Json("true");
            }
            catch
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult LoadStatus(int ID, int ChangeLoadStatus)
        {
            try
            {
                string query = "UPDATE Load SET ChangeLoadStatus = " + ChangeLoadStatus + " WHERE ID = " + ID;
                General.ExecuteNonQuery(query);
                return Json(new { success = true }); // Return JSON object with success property
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error: " + ex.Message);
                return Json(new { success = false }); // Return JSON object with success property
            }
        }

        [HttpPost]
        public ActionResult IsCancelledOrEmergency(Load obj1)
        {
            try
            {
                string Query = "UPDATE [dbo].[Load] ";
                Query += "SET [InEmergency] = " + (obj1.InEmergency ? "1" : "0") + ", ";
                Query += "[EmergencyRemarks] = '" + obj1.EmergencyRemarks.Replace("'", "''") + "', "; 
                Query += "[IsCancelled] = " + (obj1.IsCancelled ? "1" : "0") + ", ";
                Query += "[CancelledRemarks] = '" + obj1.CancelledRemarks.Replace("'", "''") + "' ";
                Query += "WHERE [ID] = " + obj1.ID;
                General.ExecuteNonQuery(Query);
                return Json(new { success = true }); 
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error: " + ex.Message);
                return Json(new { success = false });
            }
        }

        public ActionResult LoadDetails(int id)
        {
            string  query = "SELECT ID,LoadID,LoadPickupDateTime,PaymentType,OwnerName,CompanyName, DriverName,LoadRate,CarrierProfile.CommissionPercentage,(LoadRate/LoadDistance)RPM,LoadDropOffDateTime,LoadOrigin,LoadDestination,DateCreated, Load.* FROM Load inner join Driver  on Load.Driver = Driver.DriverID inner join CarrierProfile on Driver.Carrier = CarrierProfile.CarrierID WHERE Load.ID= " + id;
            DataTable dt = General.FetchData(query);
            List<Load> obj = DataTableToObject(dt);
            ViewBag.LoadStatus = new DropDown().GetLoadStatus();
            return View(obj[0]);
        }


        public ActionResult Edit(int id)
        {
            DataTable dt = General.FetchData("select * from Load Inner Join Driver On Driver.DriverID= Load.ID Inner Join CarrierProfile on CarrierProfile.CarrierID = Load.ID= " + id);
            List<Load> lstLoad = General.ConvertDataTable<Load>(dt);
            if (lstLoad.Count > 0)
            {
                return View("Create", lstLoad[0]);
            }
            ViewBag.BookingAgent = new DropDown().GetAssignedBookingAgent();
            ViewBag.Broker = new DropDown().GetBroker();
            ViewBag.Driver = new DropDown().GetDriver();
            ViewBag.Comodity = new DropDown().GetComodity();
            return View("Index");
        }

        public ActionResult GetCompanyName(int driverId)
        {
            var result = new { CompanyName = "", CarrierID = 0 };
            try
            {
                DataTable dtdata = General.FetchData($"SELECT cp.CompanyName, cp.CarrierId FROM Driver d INNER JOIN CarrierProfile cp ON d.Carrier = cp.CarrierId WHERE d.DriverId = " + driverId);

                if (dtdata.Rows.Count > 0)
                {
                    result = new
                    {
                        CompanyName = dtdata.Rows[0]["CompanyName"].ToString(),
                        CarrierID = Convert.ToInt32(dtdata.Rows[0]["CarrierId"])
                    };
                }
            }
            catch (Exception ex)
            {
                return Json(new { CompanyName = "Error fetching company name", CarrierID = 0, Error = ex.Message }, JsonRequestBehavior.AllowGet);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }



        public ActionResult GetMC(int carrierID)
        {
            string MC = "0";
            try
            {
                DataTable dtdata = General.FetchData($"SELECT CarrierAuthority.MC FROM CarrierProfile INNER JOIN CarrierAuthority ON CarrierProfile.CarrierID = CarrierAuthority.CarrierAuthorityID WHERE CarrierProfile.CarrierID = " + carrierID);

                if (dtdata.Rows.Count > 0)
                {
                    MC = dtdata.Rows[0]["MC"].ToString();
                }
            }
            catch (Exception ex)
            {
                return Json("Error fetching MC number: " + ex.Message, JsonRequestBehavior.AllowGet);
            }

            return Json(MC, JsonRequestBehavior.AllowGet);
        }


        List<Load> DataTableToObject(DataTable dt)
        {
            List<Load> lstLoad = new List<Load>();
            Load bi;
            foreach (DataRow dr in dt.Rows)
            {
                bi = new Load();
                if (dr["ID"] != DBNull.Value)
                {
                    bi.ID = int.Parse(dr["ID"].ToString());
                }
                if (dr["ChangeLoadStatus"] != DBNull.Value)
                {
                    bi.ChangeLoadStatus = int.Parse(dr["ChangeLoadStatus"].ToString());
                }
                if (dr["Driver"] != DBNull.Value)
                {
                    bi.Driver = int.Parse(dr["Driver"].ToString());
                }
                if (dr["Carrier"] != DBNull.Value)
                {
                    bi.Carrier = (dr["Carrier"].ToString());
                }
                if (dr["MC"] != DBNull.Value)
                {
                    bi.MC = (dr["MC"].ToString());
                }
                if (dr["DateCreated"] != DBNull.Value)
                {
                    bi.DateCreated = DateTime.Parse(dr["DateCreated"].ToString());
                }
                if (dr["LoadOrigin"] != DBNull.Value)
                {
                    bi.LoadOrigin = (dr["LoadOrigin"].ToString());
                }
                if (dr["LoadDestination"] != DBNull.Value)
                {
                    bi.LoadDestination = (dr["LoadDestination"].ToString());
                }
                if (dr["LoadWeight"] != DBNull.Value)
                {
                    bi.LoadWeight = int.Parse(dr["LoadWeight"].ToString());
                }
                if (dr["LoadLength"] != DBNull.Value)
                {
                    bi.LoadLength = int.Parse(dr["LoadLength"].ToString());
                }
                if (dr["LoadRate"] != DBNull.Value)
                {
                    bi.LoadRate = int.Parse(dr["LoadRate"].ToString());
                }
                if (dr["LoadDistance"] != DBNull.Value)
                {
                    bi.LoadDistance = int.Parse(dr["LoadDistance"].ToString());
                }
                if (dr["LoadType"] != DBNull.Value)
                {
                    bi.LoadType = int.Parse(dr["LoadType"].ToString());
                }
                if (dr["PaymentType"] != DBNull.Value)
                {
                    bi.PaymentType = int.Parse(dr["PaymentType"].ToString());
                }
                if (dr["LoadID"] != DBNull.Value)
                {
                    bi.LoadID = int.Parse(dr["LoadID"].ToString());
                }
                if (dr["Comodity"] != DBNull.Value)
                {
                    bi.Comodity = int.Parse(dr["Comodity"].ToString());
                }
                if (dr["Comments"] != DBNull.Value)
                {
                    bi.Comments = (dr["Comments"].ToString());
                }
                if (dr["Shipper"] != DBNull.Value)
                {
                    bi.Shipper = int.Parse(dr["Shipper"].ToString());
                }
                if (dr["AgentName"] != DBNull.Value)
                {
                    bi.AgentName = (dr["AgentName"].ToString());
                }
                if (dr["AgentPhone"] != DBNull.Value)
                {
                    bi.AgentPhone = (dr["AgentPhone"].ToString());
                }
                if (dr["LoadPickupDateTime"] != DBNull.Value)
                {
                    bi.LoadPickupDateTime = DateTime.Parse(dr["LoadPickupDateTime"].ToString());
                }
                if (dr["PickupAddress"] != DBNull.Value)
                {
                    bi.PickupAddress = (dr["PickupAddress"].ToString());
                }
                if (dr["PersonAtPickup"] != DBNull.Value)
                {
                    bi.PersonAtPickup = (dr["PersonAtPickup"].ToString());
                }
                if (dr["ContactPhone"] != DBNull.Value)
                {
                    bi.ContactPhone = (dr["ContactPhone"].ToString());
                }
                if (dr["PickupInstructions"] != DBNull.Value)
                {
                    bi.PickupInstructions = (dr["PickupInstructions"].ToString());
                }
                if (dr["Consignee"] != DBNull.Value)
                {
                    bi.Consignee = (dr["Consignee"].ToString());
                }
                if (dr["LoadDropOffDateTime"] != DBNull.Value)
                {
                    bi.LoadDropOffDateTime = DateTime.Parse(dr["LoadDropOffDateTime"].ToString());
                }
                if (dr["DropOffAddress"] != DBNull.Value)
                {
                    bi.DropOffAddress = (dr["DropOffAddress"].ToString());
                }
                if (dr["PersonAtPickupAtDelivery"] != DBNull.Value)
                {
                    bi.PersonAtPickupAtDelivery = (dr["PersonAtPickupAtDelivery"].ToString());
                }
                if (dr["ContactPhoneAtDelivery"] != DBNull.Value)
                {
                    bi.ContactPhoneAtDelivery = (dr["ContactPhoneAtDelivery"].ToString());
                }
                if (dr["PickupInstructionsAtDelivery"] != DBNull.Value)
                {
                    bi.PickupInstructionsAtDelivery = (dr["PickupInstructionsAtDelivery"].ToString());
                }
                if (dr["CompanyName"] != DBNull.Value)
                {
                    bi.CompanyName = (dr["CompanyName"].ToString());
                }
                if (dr["CommissionPercentage"] != DBNull.Value)
                {
                    bi.CommissionPercentage = int.Parse(dr["CommissionPercentage"].ToString());
                }
                if (dr["DriverName"] != DBNull.Value)
                {
                    bi.DriverName = (dr["DriverName"].ToString());
                }
                if (dr["OwnerName"] != DBNull.Value)
                {
                    bi.OwnerName = (dr["OwnerName"].ToString());
                }

                if (dr["InEmergency"] != DBNull.Value)
                {
                    bi.InEmergency = bool.Parse(dr["InEmergency"].ToString());
                }
                if (dr["EmergencyRemarks"] != DBNull.Value)
                {
                    bi.EmergencyRemarks = (dr["EmergencyRemarks"].ToString());
                }
                if (dr["IsCancelled"] != DBNull.Value)
                {
                    bi.IsCancelled = bool.Parse(dr["IsCancelled"].ToString());
                }
                if (dr["CancelledRemarks"] != DBNull.Value)
                {
                    bi.CancelledRemarks = (dr["CancelledRemarks"].ToString());
                }
                bi.lstDriver = DriverAssociation(bi.Driver);
                bi.lstCP = CarrierAssociation(bi.ID);
                lstLoad.Add(bi);
            }
            return lstLoad;
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

        List<CarrierProfile> CarrierAssociation(int CarrierID)
        {
            List<CarrierProfile> lstCP = new List<CarrierProfile>();
            DataTable dtVariant = General.FetchData(@"Select * from CarrierProfile Where CarrierID=" + CarrierID);
            foreach (DataRow dr in dtVariant.Rows)
            {
                CarrierProfile pva = new CarrierProfile();
                pva.CarrierID = int.Parse(dr["CarrierID"].ToString());
                pva.OwnerName = dr["OwnerName"].ToString();
                pva.CompanyName = dr["CompanyName"].ToString();
                pva.CommissionPercentage = int.Parse(dr["CommissionPercentage"].ToString());
                lstCP.Add(pva);
            }
            return lstCP;
        }

    }
}