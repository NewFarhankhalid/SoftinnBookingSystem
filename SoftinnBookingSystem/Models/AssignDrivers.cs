﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftinnBookingSystem.Models
{
    public class AssignDrivers
    {

        public int Id { get; set; }
        public int UserId { get; set; }
        public List<int> DriverIds { get; set; }
        public List<Drivers> Drivers { get; set; }
        public List<Users> Users { get; set; }
        public List<Assignment> Assignments { get; set; }
    }
}