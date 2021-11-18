using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LakeInn.Models.DataModels
{
    [Table("Room")]
    public class Room
    {
        [DisplayName("Id"), Key]
        public int Id { get; set; }
        [DisplayName("Name"),Required(ErrorMessage = "Not null this fields!")]
        public string Name { get; set; }
        [DisplayName("MaxGuests"), Required(ErrorMessage = "Not null this fields!")]
        public int MaxGuests { get; set; }
        [DisplayName("RoomTId"), Required(ErrorMessage = "Not null this fields!")]
        public int RoomTId { get; set; }
        [DisplayName("BedTId"), Required(ErrorMessage = "Not null this fields!")]
        public int BedTId { get; set; }
        [DisplayName("RoomFace"), Required(ErrorMessage = "Not null this fields!")]
        public string RoomFace { get; set; }
        public bool Wifi { get; set; }
        public bool Breakfast { get; set; }
        public bool RoomService { get; set; }
        public bool AirportPickup { get; set; }
        [DisplayName("Description"), Required(ErrorMessage = "Not null this fields!")]
        public string Description { get; set; }
        [DisplayName("Price"), Required(ErrorMessage = "Not null this fields!")]
        public double Price { get; set; }
        public string Image { get; set; }
        public bool Status { get; set; }
        public DateTime Date_Created { get; set; }
        public DateTime Date_Updated { get; set; }
        [ForeignKey("RoomTId")]
        public RoomType RoomTypes { get; set; }
        [ForeignKey("BedTId")]
        public BedType BedTypes { get; set; }

    }
}