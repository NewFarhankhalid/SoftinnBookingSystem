using SoftinnBookingSystem.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
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
                    query = "SELECT * FROM Load";
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
                    string Query = "INSERT INTO Load (DateCreated,Driver ,BookingAgent, LoadOrigin, LoadDestination, LoadWeight, LoadLength, LoadRate, LoadDistance, LoadType, PaymentType, LoadID, Comodity , Comments, Shipper, LoadPickupDateTime, PickupAddress, PersonAtPickup, ContactPhone, PickupInstructions, Consignee, LoadDropOffDateTime, DropOffAddress, PersonAtPickupAtDelivery, ContactPhoneAtDelivery, PickupInstructionsAtDelivery) ";
                    Query += "VALUES (GetDate()," + objLoad.BookingAgent + "," + objLoad.Driver + ",'" + objLoad.LoadOrigin + "','" + objLoad.LoadDestination + "'," + objLoad.LoadWeight + "," + objLoad.LoadLength + "," + objLoad.LoadRate + "," + objLoad.LoadDistance + "," + objLoad.LoadType + "," + objLoad.PaymentType + " ," + objLoad.LoadID + ", " + objLoad.Comodity + ",'" + objLoad.Comments + "'," + objLoad.Shipper + ",'" + objLoad.LoadPickupDateTime + "','" + objLoad.PickupAddress + "','" + objLoad.PersonAtPickup + "','" + objLoad.ContactPhone + "','" + objLoad.PickupInstructions + "','" + objLoad.Consignee + "','" + objLoad.LoadDropOffDateTime + "','" + objLoad.DropOffAddress + "','" + objLoad.PersonAtPickupAtDelivery + "','" + objLoad.ContactPhoneAtDelivery + "','" + objLoad.PickupInstructionsAtDelivery + "')";
                    Query = Query + @" Select @@IDENTITY as ID";
                    General.ExecuteNonQuery(Query);
                }
                else
                {
                    string Query = "UPDATE [dbo].[Load] ";
                    Query += "SET [BookingAgent] = " + objLoad.BookingAgent + ", ";
                    Query += "[Driver] = " + objLoad.Driver + ", ";
                    Query += "[LoadOrigin] = '" + objLoad.LoadOrigin + "', ";
                    Query += "[LoadDestination] = '" + objLoad.LoadDestination + "', ";
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


        public ActionResult LoadDetails(int id)
        
        {
            
            string  query = "SELECT * FROM Load where ID= " + id;
            DataTable dt = General.FetchData(query);
            List<Load> obj = DataTableToObject(dt);
            ViewBag.LoadStatus = new DropDown().GetLoadStatus();
            return View(obj[0]);
        }


        public ActionResult Edit(int id)
        {
            DataTable dt = General.FetchData("Select * from Load Where ID= " + id);
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
                lstLoad.Add(bi);
            }
            return lstLoad;
        }

    }
}