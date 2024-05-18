using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SoftinnBookingSystem.Models
{
    public class Load
    {
        public int ID { get; set; }

        public DateTime DateCreated { get; set; }
        public int BookingAgent { get; set; }
        public int Driver { get; set; }
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

        public List<Drivers> lstDriver = new List<Drivers>();
        public List<Brooker> lstBroker = new List<Brooker>();
    }
}