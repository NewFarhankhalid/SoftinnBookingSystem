using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoftinnBookingSystem
{
    public class DropDown
    {
        public SelectList GetRoles(int selectedvalue = 0)
        {
            List<SelectListItem> objsli = new List<SelectListItem>();
            objsli.Add(new SelectListItem() { Text = "Admin", Value = 1.ToString() });
            objsli.Add(new SelectListItem() { Text = "Dispatcher", Value = 2.ToString() });
            objsli.Add(new SelectListItem() { Text = "Driver", Value = 3.ToString() });
            objsli.Add(new SelectListItem() { Text = "Sales Person", Value = 4.ToString() });
            objsli.Add(new SelectListItem() { Text = "Booking Agent", Value = 5.ToString() });
            SelectList sl = new SelectList(objsli, "Value", "Text", selectedvalue.ToString());
            return sl;
        }


        public SelectList GetLoadStatus(int selectedvalue = 0)
        {
            List<SelectListItem> objsli = new List<SelectListItem>();
            objsli.Add(new SelectListItem() { Text = "Published", Value = 1.ToString() });
            objsli.Add(new SelectListItem() { Text = "Dispatced", Value = 2.ToString() });
            objsli.Add(new SelectListItem() { Text = "Delivered", Value = 3.ToString() });
            objsli.Add(new SelectListItem() { Text = "BOL Received", Value = 4.ToString() });
            objsli.Add(new SelectListItem() { Text = "Carrier Paid", Value = 5.ToString() });
            objsli.Add(new SelectListItem() { Text = "Invoice Issued", Value = 6.ToString() });
            objsli.Add(new SelectListItem() { Text = "Invoice Cleared", Value = 7.ToString() });
            objsli.Add(new SelectListItem() { Text = "Completed", Value = 8.ToString() });
            objsli.Add(new SelectListItem() { Text = "Cancelled", Value = 9.ToString() });
            SelectList sl = new SelectList(objsli, "Value", "Text", selectedvalue.ToString());
            return sl;
        }



        public SelectList GetInvoiceStatus(int selectedvalue = 0)
        {
            List<SelectListItem> objsli = new List<SelectListItem>();
            objsli.Add(new SelectListItem() { Text = "Created", Value = 1.ToString() });
            objsli.Add(new SelectListItem() { Text = "Sent", Value = 2.ToString() });
            objsli.Add(new SelectListItem() { Text = "Paid", Value = 3.ToString() });
            objsli.Add(new SelectListItem() { Text = "Cancelled", Value = 4.ToString() });
            SelectList sl = new SelectList(objsli, "Value", "Text", selectedvalue.ToString());
            return sl;
        }

        public SelectList PersonTaxClassification(int selectedvalue = 0)
        {
            List<SelectListItem> objsli = new List<SelectListItem>();
            objsli.Add(new SelectListItem() { Text = "Individual/Sole proprietor", Value = 1.ToString() });
            objsli.Add(new SelectListItem() { Text = "C Corporation", Value = 2.ToString() });
            objsli.Add(new SelectListItem() { Text = "S Corporation", Value = 3.ToString() });
            objsli.Add(new SelectListItem() { Text = "Partnership", Value = 4.ToString() });
            objsli.Add(new SelectListItem() { Text = "Trust/Estate", Value = 5.ToString() });
            SelectList sl = new SelectList(objsli, "Value", "Text", selectedvalue.ToString());
            return sl;
        }

        public SelectList LLCTaxClassification(int selectedvalue = 0)
        {
            List<SelectListItem> objsli = new List<SelectListItem>();
            objsli.Add(new SelectListItem() { Text = "C = C Corporation", Value = 1.ToString() });
            objsli.Add(new SelectListItem() { Text = "S = S Corporation", Value = 2.ToString() });
            objsli.Add(new SelectListItem() { Text = "P = Partnership", Value = 3.ToString() });
            SelectList sl = new SelectList(objsli, "Value", "Text", selectedvalue.ToString());
            return sl;
        }

        public SelectList GetLoadType(int selectedvalue = 0)
        {
            List<SelectListItem> objsli = new List<SelectListItem>();
            objsli.Add(new SelectListItem() { Text = "Full Load", Value = 1.ToString() });
            objsli.Add(new SelectListItem() { Text = "Partial Load", Value = 2.ToString() });
            SelectList sl = new SelectList(objsli, "Value", "Text", selectedvalue.ToString());
            return sl;
        }

        public SelectList GetPaymentType(int selectedvalue = 0)
        {
            List<SelectListItem> objsli = new List<SelectListItem>();
            objsli.Add(new SelectListItem() { Text = "Factoring", Value = 1.ToString() });
            objsli.Add(new SelectListItem() { Text = "Quick pay", Value = 2.ToString() });
            objsli.Add(new SelectListItem() { Text = "Cash on Delivery", Value = 3.ToString() });
            objsli.Add(new SelectListItem() { Text = "EFS Code", Value = 4.ToString() });
            SelectList sl = new SelectList(objsli, "Value", "Text", selectedvalue.ToString());
            return sl;
        }

        public SelectList GetDispatcher(string whereclause = "", int selectedvalue = 0)
        {
            DataTable dt = General.FetchData(@" select * from Users Inner Join Dispatcher
On Dispatcher.UserID= Users.UserID" + whereclause);
            List<SelectListItem> objsli = new List<SelectListItem>();
            SelectListItem si = new SelectListItem();
            foreach (DataRow dr in dt.Rows)
            {
                si = new SelectListItem();
                si.Value = dr["UserID"].ToString();
                si.Text = (dr["UserName"]).ToString();
                objsli.Add(si);
            }
            SelectList sl = new SelectList(objsli, "Value", "Text", selectedvalue);
            return sl;
        }

        public SelectList GetComodity(string whereclause = "", int selectedvalue = 0)
        {
            DataTable dt = General.FetchData(@" select * from Comodity" + whereclause);
            List<SelectListItem> objsli = new List<SelectListItem>();
            SelectListItem si = new SelectListItem();
            foreach (DataRow dr in dt.Rows)
            {
                si = new SelectListItem();
                si.Value = dr["Id"].ToString();
                si.Text = (dr["Name"]).ToString();
                objsli.Add(si);
            }
            SelectList sl = new SelectList(objsli, "Value", "Text", selectedvalue);
            return sl;
        }

        public SelectList GetAssignedBookingAgent(string whereclause = "", int selectedvalue = 0)
        {
            // Construct the query with the WHERE clause
            string query = @"SELECT * FROM Users WHERE Roles LIKE '%5%'";

            // If whereclause is provided, append it to the query
            if (!string.IsNullOrEmpty(whereclause))
            {
                query += " AND " + whereclause;
            }

            // Fetch data from the database
            DataTable dt = General.FetchData(query);

            // Process the retrieved data
            List<SelectListItem> objsli = new List<SelectListItem>();

            foreach (DataRow dr in dt.Rows)
            {
                SelectListItem si = new SelectListItem();
                si.Text = dr["UserName"].ToString();
                si.Value = dr["UserID"].ToString();
                objsli.Add(si);
            }

            // Create and return the SelectList object
            SelectList sl = new SelectList(objsli, "Value", "Text", selectedvalue);
            return sl;
        }


        public SelectList GetDispatcherInfo(string whereclause = "", int selectedvalue = 0)
        {
            // Construct the query with the WHERE clause
            string query = @"SELECT * FROM Users WHERE Roles LIKE '%2%'";

            // If whereclause is provided, append it to the query
            if (!string.IsNullOrEmpty(whereclause))
            {
                query += " AND " + whereclause;
            }

            // Fetch data from the database
            DataTable dt = General.FetchData(query);

            // Process the retrieved data
            List<SelectListItem> objsli = new List<SelectListItem>();

            foreach (DataRow dr in dt.Rows)
            {
                SelectListItem si = new SelectListItem();
                si.Text = dr["UserName"].ToString();
                si.Value = dr["UserID"].ToString();
                objsli.Add(si);
            }

            // Create and return the SelectList object
            SelectList sl = new SelectList(objsli, "Value", "Text", selectedvalue);
            return sl;
        }




        public SelectList GetBroker(string whereclause = "", int selectedvalue = 0)
        {
            DataTable dt = General.FetchData(@" select * from Brooker " + whereclause);
            List<SelectListItem> objsli = new List<SelectListItem>();
            SelectListItem si = new SelectListItem();
            foreach (DataRow dr in dt.Rows)
            {
                si = new SelectListItem();
                si.Value = (dr["BrookerID"]).ToString();
                si.Text = dr["BrookerBusinessName"].ToString();
                objsli.Add(si);
            }
            SelectList sl = new SelectList(objsli, "Value", "Text", selectedvalue);
            return sl;
        }

        public SelectList GetDriver(string whereclause = "", int selectedvalue = 0)
        {
            DataTable dt = General.FetchData(@" select * from Driver " + whereclause);
            List<SelectListItem> objsli = new List<SelectListItem>();
            SelectListItem si = new SelectListItem();
            foreach (DataRow dr in dt.Rows)
            {
                si = new SelectListItem();
                si.Value = (dr["DriverID"]).ToString();
                si.Text = dr["DriverName"].ToString();
                objsli.Add(si);
            }
            SelectList sl = new SelectList(objsli, "Value", "Text", selectedvalue);
            return sl;
        }

    }
}