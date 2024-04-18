using SoftinnBookingSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.DynamicData;
using System.Web.Mvc;

namespace SoftinnBookingSystem.Controllers
{
    public class CompaniesController : Controller
    {
        // GET: Companies
        public ActionResult Index()
        {
            string sql = $@"Select * from Companies";
            DataTable dt = General.FetchData(sql);
            List<Companies> lst = General.ConvertDataTable<Companies>(dt);
            return View(lst);
        }
        public ActionResult Create()
        {
            Companies obj = new Companies();
            return View(obj);
        }
        [HttpPost]
        public ActionResult Create(Companies Com)
        {
            try
            {
                if(Com.CompanyID ==0)
                {
                    string Query = "Insert into Companies (CompanyName) ";
                    Query = Query + "Values ('" + Com.CompanyName + "')";
                    General.ExecuteNonQuery(Query);
                }
                else
                {

                    string Query = "";
                    Query = Query + "UPDATE [dbo].[Companies] ";
                    Query = Query + " SET    [CompanyName] ='" + Com.CompanyName + "' ";
                    Query = Query + "WHERE CompanyID=" + Com.CompanyID;
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
            DataTable dtCompany = General.FetchData("Select * from Companies where CompanyID=" + Id);
            List<Companies> lstCompany = General.ConvertDataTable<Companies>(dtCompany);
            if (lstCompany.Count > 0)
            {
                return View("Create", lstCompany[0]);
            }

            return View("Index");
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string SQL = "Delete From Companies where CompanyID=" + id;
            General.ExecuteNonQuery(SQL);
            return Json("true");

        }

    }
}