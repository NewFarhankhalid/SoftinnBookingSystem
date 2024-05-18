using SoftinnBookingSystem.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoftinnBookingSystem.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            DataTable dtUser = General.FetchData("Select * from Users");
            List<Users> obj = DataTableToObject(dtUser);
    ViewBag.Roles = new DropDown().GetRoles();
            return View(obj);
        }

        //public ActionResult AddUser(Users objUser)
        //{
        //    string Query = "INSERT INTO Users (UserName, UserAddres, UserEmail, Password, PhoneNumber, Alias, Roles) ";
        //    Query += "VALUES ('" + objUser.UserName + "','" + objUser.UserAddres + "','" + objUser.UserEmail + "','" + objUser.Password + "','" + objUser.PhoneNumber + "','" + objUser.Alias + "'," + objUser.Roles + ")";
        //    General.ExecuteNonQuery(Query);
        //    Query = "";
        //    return Json("true");
        //}

        [HttpPost]
        public ActionResult AddUser(Users objUser)
        {
            try
            {
                if (objUser.UserID == 0)
                {
                    
                    string Query = "INSERT INTO Users (UserName, UserAddres, UserEmail, Password, PhoneNumber, Alias, Roles) ";
                    Query += "VALUES ('" + objUser.UserName + "','" + objUser.UserAddres + "','" + objUser.UserEmail + "','" + objUser.Password + "','" + objUser.PhoneNumber + "','" + objUser.Alias + "'," + objUser.Roles + ")";
                    Query += "Select @@Identity as UserID";
                    General.ExecuteNonQuery(Query);
                }
            else
            {
                string Query = "";
                Query = Query + "UPDATE [dbo].[Users] SET ";
                Query = Query + "     [UserName] = '" + objUser.UserName + "' ";
                Query = Query + "    ,[UserAddres] = '" + objUser.UserAddres + "' ";
                Query = Query + "    ,[UserEmail] = '" + objUser.UserEmail + "' ";
                Query = Query + "    ,[Password] = '" + objUser.Password + "' ";
                Query = Query + "    ,[PhoneNumber] = '" + objUser.PhoneNumber + "' ";
                Query = Query + "    ,[Alias] = '" + objUser.Alias + "' ";
                Query = Query + "    ,[Roles] = '" + objUser.Roles + "' ";
                Query = Query + "WHERE UserID=" + objUser.UserID;
                General.ExecuteNonQuery(Query);
                }
                return Json("true");
            }
            catch
            {
                return View();
            }
        }



        public ActionResult GetUserInfo(int id)
        {
            string UserName = "0";
            string UserAddress = "0";
            string UserEmail = "0";
            string Password = "0";
            string PhoneNumber = "0";
            string Alias = "0";
            string Roles = "0";
            string UserID = "0";

            DataTable sql = General.FetchData("SELECT * from Users WHERE UserID = " + id);
            if (sql.Rows.Count > 0)
            {
                UserID = sql.Rows[0]["UserID"].ToString();
                UserName = sql.Rows[0]["UserName"].ToString();
                UserAddress = sql.Rows[0]["UserAddres"].ToString();
                UserEmail = sql.Rows[0]["UserEmail"].ToString();
                Password = sql.Rows[0]["Password"].ToString();
                PhoneNumber = sql.Rows[0]["PhoneNumber"].ToString();
                Alias = sql.Rows[0]["Alias"].ToString();
                Roles = sql.Rows[0]["Roles"].ToString();
            }

            // Create an anonymous object to hold all the data
            var responseData = new
            {
                UserID = UserID,
                UserName = UserName,
                UserAddress = UserAddress,
                UserEmail = UserEmail,
                Password = Password,
                PhoneNumber = PhoneNumber,
                Alias = Alias,
                Roles = Roles
            };

            // Return the JSON data
            return Json(responseData, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Edit(int id)
        {
            DataTable dtUser = General.FetchData("Select * from Users where UserID=" + id);
            List<Users> obj = DataTableToObject(dtUser);
            if (obj.Count > 0)
            {
                return View("Create", obj[0]);
            }
            return RedirectToAction("index");
        }

        //[HttpPost]
        //public ActionResult Edit(Users objUser)
        //{
        //    try
        //    {
        //        string Query = "";
        //        Query = Query + "UPDATE [dbo].[Users] SET ";
        //        Query = Query + "     [UserName] = '" + objUser.UserName + "' ";
        //        Query = Query + "    ,[UserAddres] = '" + objUser.UserAddres + "' ";
        //        Query = Query + "    ,[UserEmail] = '" + objUser.UserEmail + "' ";
        //        Query = Query + "    ,[Password] = '" + objUser.Password + "' ";
        //        Query = Query + "    ,[PhoneNumber] = '" + objUser.PhoneNumber + "' ";
        //        Query = Query + "    ,[Alias] = '" + objUser.Alias + "' ";
        //        Query = Query + "    ,[Roles] = '" + objUser.Roles + "' ";
        //        Query = Query + "WHERE UserID=" + objUser.UserID;
        //        General.ExecuteNonQuery(Query);
        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        

        public ActionResult GetUserData(int userID)
        {
            try
            {
                // Fetch user data from the database based on the provided user ID
                DataTable dtUser = General.FetchData("SELECT * FROM Users WHERE UserID = " + userID);
                if (dtUser.Rows.Count > 0)
                {
                    // Convert DataRow to Users object
                    Users user = DataRowToObject(dtUser.Rows[0]);
                    // Return JSON data containing user details
                    return Json(user, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    // If user with provided ID doesn't exist, return an empty response
                    return Json(null, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        private Users DataRowToObject(DataRow dr)
        {
            Users user = new Users();
            user.UserID = Convert.ToInt32(dr["UserID"]);
            user.UserName = Convert.ToString(dr["UserName"]);
            user.UserAddres = Convert.ToString(dr["UserAddres"]);
            user.UserEmail = Convert.ToString(dr["UserEmail"]);
            user.Password = Convert.ToString(dr["Password"]);
            user.PhoneNumber = Convert.ToString(dr["PhoneNumber"]);
            user.Alias = Convert.ToString(dr["Alias"]);
            user.Roles = Convert.ToInt32(dr["Roles"]);
            return user;
        }


        [HttpPost]
        public JsonResult Delete(int id)
        {
            string Query = "Delete from Users Where UserID=" + id;
            General.ExecuteNonQuery(Query);
            return Json("true");
        }
        List<Users> DataTableToObject(DataTable dt)
        {
            List<Users> lstUser = new List<Users>();
            Users bi;
            foreach (DataRow dr in dt.Rows)
            {
                bi = new Users();
                if (dr["UserID"] != DBNull.Value)
                {
                    bi.UserID = int.Parse(dr["UserID"].ToString());
                }
                if (dr["UserName"] != DBNull.Value)
                {
                    bi.UserName = (dr["UserName"].ToString());
                }
                if (dr["UserAddres"] != DBNull.Value)
                {
                    bi.UserAddres = (dr["UserAddres"].ToString());
                }
                if (dr["UserEmail"] != DBNull.Value)
                {
                    bi.UserEmail = (dr["UserEmail"].ToString());
                }
                if (dr["Password"] != DBNull.Value)
                {
                    bi.Password = (dr["Password"].ToString());
                }
                if (dr["PhoneNumber"] != DBNull.Value)
                {
                    bi.PhoneNumber = (dr["PhoneNumber"].ToString());
                }
                if (dr["Alias"] != DBNull.Value)
                {
                    bi.Alias = (dr["Alias"].ToString());
                }
              
                if (dr["Roles"] != DBNull.Value)
                {
                    bi.Roles = int.Parse(dr["Roles"].ToString());
                }
               
                lstUser.Add(bi);
            }
            return lstUser;
        }
    }
}