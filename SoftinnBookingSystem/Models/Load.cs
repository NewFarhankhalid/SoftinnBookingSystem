using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.UI.WebControls;

namespace SoftinnBookingSystem.Models
{
    public class Load
    {
        public int ID { get; set; }

        public DateTime DateCreated { get; set; }
        public int Driver { get; set; }
        public string MC { get; set; }
        public string Carrier { get; set; }
        [Required]
        public string LoadOrigin { get; set; }
        [Required]
        public string LoadDestination { get; set; }

        public int ChangeLoadStatus { get; set; }
        [Required]
        public int LoadWeight { get; set; }
        [Required]
        public int LoadLength { get; set; }
        [Required]
        public int LoadRate { get; set; }
        [Required]
        public int LoadDistance { get; set; }
        [Required]
        public int LoadType { get; set; }
        [Required]
        public int PaymentType { get; set; }
        [Required]
  
        public int LoadID { get; set; }
        public string Comments { get; set; }
        public int Comodity { get; set; }

        public int Shipper { get; set; }

        public string AgentName { get; set; }
        public string AgentPhone { get; set; }
        public DateTime LoadPickupDateTime { get; set; }
        public string PickupAddress { get; set; }
        public string PersonAtPickup { get; set; }
        public string ContactPhone { get; set; }
        public string PickupInstructions { get; set; }
        public string Consignee { get; set; }
        public DateTime LoadDropOffDateTime { get; set; }
        public string DropOffAddress { get; set; }
        public string PersonAtPickupAtDelivery { get; set; }
        public string ContactPhoneAtDelivery { get; set; }
        public string PickupInstructionsAtDelivery { get; set; }
        public bool InEmergency { get; set; }
        public string EmergencyRemarks { get; set; }
        public bool IsCancelled { get; set; }
        public string CancelledRemarks { get; set; }
        public string CompanyName { get; set; }
        public int CommissionPercentage { get; set; }

        public string DriverName { get; set; }
        public string OwnerName { get; set; }


        public List<Drivers> lstDriver = new List<Drivers>();
        public List<CarrierProfile> lstCP = new List<CarrierProfile>();
    }
}