using SoftinnBookingSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoftinnBookingSystem.Controllers
{
    public class AccessoriesController : Controller
    {
        // GET: Accessories
        public ActionResult Index()
        {
            DataTable dt = new DataTable();
            dt = General.FetchData($@"Select * From Accessories");
            return View(dt);
        }

        public ActionResult Create()
        {
            Accessories accessories = new Accessories();
            return View(accessories);
        }

        [HttpPost]
        public ActionResult Create(Accessories objaccessories)
        {
            try
            {

                if (objaccessories.id == 0)
                {
                    string Query = "Insert into Accessories (Name) ";
                    Query = Query + "Values ('" + objaccessories.Name + "')";
                    //General.ExecuteNonQuery(Query);
                    //Query = "";
                    Query = Query + " Select @@IDENTITY as id";
                    objaccessories.id = int.Parse(General.FetchData(Query).Rows[0]["id"].ToString());



                }
                else
                {
                    string Query = "";
                    Query = Query + "UPDATE [dbo].[Accessories] ";
                    Query = Query + " SET    [Name] ='" + objaccessories.Name + "' ";
                    Query = Query + " WHERE id=" + objaccessories.id;
                    General.FetchData(Query);


                }
                return Json("true," + objaccessories.id);
            }
            catch
            {
                return Json("error");
            }
        }

        public ActionResult CheckStatus(string Status)
        {
            string sql = $@"Select * from Accessories where Name like '%{Status}%'";
            DataTable dt = General.FetchData(sql);
            if (dt.Rows.Count > 0)
            {
                return Json("true," + dt.Rows[0]["Name"].ToString());
            }
            else
            {
                return Json("false,");
            }
        }


        public ActionResult Edit(int Id)
        {
            string sql = $@"Select * from Accessories Where id ={Id}";

            DataTable dt = General.FetchData(sql);
            List<Accessories> lstbranch = DataTableToObject(dt);
            if (lstbranch.Count > 0)
            {
                return View("Create", lstbranch[0]);
            }
            return Json("true");
        }

        List<Accessories> DataTableToObject(DataTable dt)
        {
            List<Accessories> lstbranch = new List<Accessories>();
            Accessories bi;
            foreach (DataRow dr in dt.Rows)
            {
                bi = new Accessories();
                if (dr["id"] != DBNull.Value)
                {
                    bi.id = int.Parse(dr["id"].ToString());
                }
                if (dr["Name"] != DBNull.Value)
                {
                    bi.Name = (dr["Name"].ToString());
                }

                lstbranch.Add(bi);
            }
            return lstbranch;
        }

        public ActionResult Delete(int id)
        {
            //string CompanyTitle = General.FetchData("Select Name from WorkStatus Where CustomerID=" + id).Rows[0]["Name"].ToString();
            string query = "delete from Accessories where id=" + id;
            General.ExecuteNonQuery(query);

            return Json("true");
        }
    }
}