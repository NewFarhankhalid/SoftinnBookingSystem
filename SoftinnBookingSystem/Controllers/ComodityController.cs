using SoftinnBookingSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoftinnBookingSystem.Controllers
{
    public class ComodityController : Controller
    {
     
            // GET: Comodity
            public ActionResult Index()
            {
                DataTable dt = new DataTable();
                dt = General.FetchData($@"Select * From Comodity");
                return View(dt);
            }

            public ActionResult Create()
            {
                Comodity Comodity = new Comodity();
                return View(Comodity);
            }

            [HttpPost]
            public ActionResult Create(Comodity objComodity)
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

            public ActionResult CheckStatus(string Status)
            {
                string sql = $@"Select * from Comodity where Name like '%{Status}%'";
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
                string sql = $@"Select * from Comodity Where ID ={Id}";

                DataTable dt = General.FetchData(sql);
                List<Comodity> lstbranch = DataTableToObject(dt);
                if (lstbranch.Count > 0)
                {
                    return View("Create", lstbranch[0]);
                }
                return Json("true");
            }

            List<Comodity> DataTableToObject(DataTable dt)
            {
                List<Comodity> lstbranch = new List<Comodity>();
                Comodity bi;
                foreach (DataRow dr in dt.Rows)
                {
                    bi = new Comodity();
                    if (dr["Id"] != DBNull.Value)
                    {
                        bi.Id = int.Parse(dr["Id"].ToString());
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
                string query = "delete from Comodity where Id=" + id;
                General.ExecuteNonQuery(query);

                return Json("true");
            }

        
    }
}