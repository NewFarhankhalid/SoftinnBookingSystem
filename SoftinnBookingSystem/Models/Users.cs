using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SoftinnBookingSystem.Models
{
    public class Users
    {
       
        public int UserID { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string UserName { get; set; }
        [Required]
        [Display(Name = "Address")]
        public string UserAddres { get; set; }
        [Required]
        [Display(Name = "Email")]
        public string UserEmail { get; set; }
        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        public string Alias { get; set; }
        public string Status { get; set; }
        public int Roles { get; set; }
    }
}