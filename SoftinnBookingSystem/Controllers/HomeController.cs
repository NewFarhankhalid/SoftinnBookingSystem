using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoftinnBookingSystem.Controllers
{
    public class HomeController : BaseController
    {
 
        public ActionResult Index()
        {
            if (Request.Cookies["UserID"] == null)
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


     
        public ActionResult Logout()
        {
            Response.Cookies["UserID"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["UserEmail"].Expires = DateTime.Now.AddDays(-1);
            //Response.Cookies["MembershipNo"].Expires = DateTime.Now.AddDays(-1);

            //AddLog.TakeLogs("Webportal - LogOut", General.userID, AddLog.GetClientMAC(AddLog.GetIPAddress()), "WebportalLogOut");
            return RedirectToAction("Login");
        }


        [HttpGet]
        public ActionResult Login()
        {
            Models.Login objlogin = new Models.Login();
            ViewBag.Message = "";
            return View("Login", objlogin);
        }

        [HttpPost]
        public ActionResult Login(Models.Login objlogin)
        {
            if (objlogin.UserEmail is null && objlogin.Password is null)
            {
                ViewBag.Message = "Please Enter Email and Password";
                return View("Login", objlogin);
            }
            if (objlogin.UserEmail is null)
            {
                ViewBag.Message = "Please Enter Email";
                return View("Login", objlogin);
            }
            if (objlogin.Password is null)
            {
                ViewBag.Message = "Please Enter Password";
                return View("Login", objlogin);
            }
            if (!IsValidSQLInput(objlogin.UserEmail) || !IsValidSQLInput(objlogin.Password))
            {
                ViewBag.Message = "Invalid input detected.";
                return View("Login", objlogin);
            }


            string SQL = @"SELECT * FROM Users WHERE UserEmail = '" + objlogin.UserEmail.Trim().Replace("'", "") + "' COLLATE SQL_Latin1_General_CP1_CS_AS AND Password = '" + objlogin.Password.Trim().Replace("'", "") + "' COLLATE SQL_Latin1_General_CP1_CS_AS";

            DataTable currentUser = General.FetchData(SQL);

            if (currentUser.Rows.Count > 0)
            {
                int userID = Convert.ToInt32(currentUser.Rows[0]["UserID"]);
                Response.Cookies["UserID"].Value = userID.ToString();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Message = "Invalid User Email or Password";
                return View("Login", objlogin);
            }
        }

        private bool IsValidSQLInput(string input)
        {
            // Check if the input is null or empty
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            // Check if the input contains any SQL keywords
            string[] sqlKeywords = { "SELECT", "INSERT", "UPDATE", "DELETE", "DROP", "CREATE", "ALTER", "TRUNCATE", "EXEC" };
            if (sqlKeywords.Any(keyword => input.ToUpper().Contains(keyword)))
            {
                return false;
            }

            // Check if the input contains any SQL operators
            string[] sqlOperators = { ";", "--", "/*", "*/" };
            if (sqlOperators.Any(op => input.Contains(op)))
            {
                return false;
            }

            // Check if the input contains any single quote (') characters
            if (input.Contains("'"))
            {
                return false;
            }

            // Check if the input contains any double dash (--) characters
            if (input.Contains("--"))
            {
                return false;
            }

            // Check if the input contains any SQL functions
            string[] sqlFunctions = { "UPDATEXML", "DBMS_XMLGEN", "DBMS_SQL", "ORACLEXMLTYPE", "ORDSYS.ORDIM",
                              "SYS_CONTEXT", "DBMS_LDAP", "LOAD_FILE", "MAKE_SET", "GROUP_CONCAT",
                              "EXECUTE", "EXEC", "SP_EXECUTESQL", "XP_CMDSHELL", "SQLCMD", "OPENQUERY",
                              "OPENDATASOURCE", "OPENROWSET", "OPENXML", "ROWSET", "SYSTEM_USER",
                              "CURRENT_USER", "SESSION_USER", "USER_NAME", "USER", "DATABASE", "IFNULL",
                              "NVL", "ISNULL", "CONVERT", "CAST", "DECLARE", "VARCHAR", "NVARCHAR",
                              "CHAR", "NCHAR", "TEXT", "NTEXT", "BINARY", "VARBINARY", "IMAGE",
                              "INTO", "HAVING", "GROUP BY", "ORDER BY", "WHERE", "JOIN", "INNER JOIN",
                              "LEFT JOIN", "RIGHT JOIN", "FULL JOIN", "UNION", "UNION ALL", "CREATE",
                              "DROP", "ALTER", "TRUNCATE", "RENAME", "COMMENT", "SET", "EXEC", "EXECUTE",
                              "USE", "SHOW", "DESC", "DESCRIBE", "LIMIT", "OFFSET", "TOP", "FETCH",
                              "NOCOUNT", "PRINT", "OUTPUT", "RAISERROR", "WAITFOR", "XP_", "SP_", "FN_"};
            if (sqlFunctions.Any(func => input.ToUpper().Contains(func)))
            {
                return false;
            }

            // Check if the input is a valid numeric value
            //if (!decimal.TryParse(input, out _))
            //{
            //    return false;
            //}

            // If none of the checks failed, consider the input as valid
            return true;
        }


    }
}