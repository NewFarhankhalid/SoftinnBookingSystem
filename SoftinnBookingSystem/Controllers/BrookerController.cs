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
            string Sql = $@"Select * from Brooker";
            DataTable dt = General.FetchData(Sql);
            return View();
        }
        public ActionResult Create()
        {
           Brooker obj = new Brooker();
            return View(obj);
        }
        [HttpPost]
        public ActionResult Create(Brooker br)
        {
            try
            {
                if (br.BrookerID == 0)
                {
                    string Query = "Insert into Brookers (BrookerMC,BrookerUsDot,BrookerBusinessName,BrookerEmail,BrookerPhone,BrookerFax,BrookerAddress)";
                    Query = Query + "Values ('" + br.BrookerMC + "','" + br.BrookerUsDot + "','" + br.BrookerBusinessName + "','" + br.BrookerEmail + "','" + br.BrookerPhone + "'," +
                        "'" + br.BrookerFax + "','" + br.BrookerAddress + "',)";
                    General.ExecuteNonQuery(Query);
                }
                else
                {
                    string Query = "";
                    Query = Query + "Update [dbo].[Brooker]";
                    Query = Query + "Set [BrookerMC] = '" + br.BrookerMC + "' ";
                    Query = Query + "Set [BrookerUsDot] = '" + br.BrookerUsDot + "' ";
                    Query = Query + "Set [BrookerBusinessName] = '" + br.BrookerBusinessName + "' ";
                    Query = Query + "Set [BrookerEmail] = '" + br.BrookerEmail + "' ";
                    Query = Query + "Set [BrookerPhone] = '" + br.BrookerPhone + "' ";
                    Query = Query + "Set [BrookerFax] = '" + br.BrookerFax + "' ";
                    Query = Query + "Set [BrookerAddress] = '" + br.BrookerAddress + "' ";
                    Query = Query + "Where BrookerID = " + br.BrookerID ;
                    General.ExecuteNonQuery(Query);
                }
                return Json("true");
            }
            catch {
                return View();
            }            
        }

        public ActionResult Edit(int id)
        {
            DataTable dt = General.FetchData("Select * from Brooker Where BrookerID= " + id);
            List<Brooker> lstBrooker = General.ConvertDataTable<Brooker>(dt);
            if (lstBrooker.Count > 0)
            {
                return View("Create", lstBrooker[0]);
            }
            return View ("Index");
        }

        public ActionResult Delete(int id)
        {
            string Sql = "Delete from Brooker where BrookerID=" + id;
            General.ExecuteNonQuery(Sql);
            return View ("Index");
        }
    }
}