using SoftinnBookingSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;

namespace SoftinnBookingSystem.Controllers
{
    public class BrookerController : Controller
    {
        // GET: Brooker
        public ActionResult Index()
        {
            DataTable dtBrooker = General.FetchData("Select * from Brooker");
            List<Brooker> obj = DataTableToObject(dtBrooker);
            return View(obj);
        }
        public ActionResult Create()
        {
            Brooker obj = new Brooker();
            return View(obj);
        }
        [HttpPost]
        public ActionResult Create(Brooker Brooker)
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

        public ActionResult Edit(int id)
        {
            DataTable dt = General.FetchData("Select * from Brooker Where BrookerID= " + id);
            List<Brooker> lstBrooker = DataTableToObject(dt);
            if (lstBrooker.Count > 0)
            {
                return View("Create", lstBrooker[0]);
            }
            return View("Index");
        }

        public ActionResult Delete(int id)
        {
            string Sql = "Delete from Brooker where BrookerID=" + id;
            General.ExecuteNonQuery(Sql);
            return View("Index");
        }

        List<Brooker> DataTableToObject(DataTable dt)
        {
            List<Brooker> lstBrooker = new List<Brooker>();
            Brooker bi;
            foreach (DataRow dr in dt.Rows)
            {
                bi = new Brooker();
                if (dr["BrookerID"] != DBNull.Value)
                {
                    bi.BrookerID = int.Parse(dr["BrookerID"].ToString());
                }
                if (dr["BrookerMC"] != DBNull.Value)
                {
                    bi.BrookerMC = (dr["BrookerMC"].ToString());
                }
                if (dr["BrookerUsDot"] != DBNull.Value)
                {
                    bi.BrookerUsDot = (dr["BrookerUsDot"].ToString());
                }
                if (dr["BrookerBusinessName"] != DBNull.Value)
                {
                    bi.BrookerBusinessName = (dr["BrookerBusinessName"].ToString());
                }
                if (dr["BrookerEmail"] != DBNull.Value)
                {
                    bi.BrookerEmail = (dr["BrookerEmail"].ToString());
                }
                if (dr["BrookerPhone"] != DBNull.Value)
                {
                    bi.BrookerPhone = (dr["BrookerPhone"].ToString());
                }

                if (dr["BrookerAddress"] != DBNull.Value)
                {
                    bi.BrookerAddress = (dr["BrookerAddress"].ToString());
                }
                lstBrooker.Add(bi);
            }
            return lstBrooker;
        }
    }
}