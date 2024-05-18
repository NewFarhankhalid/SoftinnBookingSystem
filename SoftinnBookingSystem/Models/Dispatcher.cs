using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoftinnBookingSystem.Models
{
    public class Dispatcher
    {

        public int DispatcherID { get; set; }
        public int UserID { get; set; }
        public int BaseSalary { get; set; }
        public int  FuelAllowance { get; set;}
        public int CommissionPercentage { get; set;}
        public string DiscordTag { get; set;}
        public int BookingAgent { get; set; }

        public List<Users> lstUsers = new List<Users>();

    }
}