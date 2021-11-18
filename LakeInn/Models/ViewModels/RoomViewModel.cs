using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LakeInn.Models.ViewModels
{
    public class RoomViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MaxGuests { get; set; }
        public int RoomTId { get; set; }
        public int BedTId { get; set; }
        public string RoomFace { get; set; }
        public bool Wifi { get; set; }
        public bool Breakfast { get; set; }
        public bool RoomService { get; set; }
        public bool AirportPickup { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }
        public bool Status { get; set; }
        public string TypeName { get; set; }
        public string TypeBed { get; set; }
    }
}