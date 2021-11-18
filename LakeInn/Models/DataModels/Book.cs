using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LakeInn.Models.DataModels
{
    [Table("Book")]
    public class Book
    {
        [DisplayName("ID")]
        public int Id { get; set; }
        [DisplayName("Name")]
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Identity { get; set; }
        public string Address { get; set; }
        [DisplayName("Check-in Date")]
        public DateTime CID { get; set; }
        [DisplayName("Check-out Date")]
        public DateTime COD { get; set; }
        [DisplayName("Quantity Adult")]
        public int Qadult { get; set; }
        [DisplayName("Quantity Child")]
        public int Qchild { get; set; }
        [ForeignKey("RoomType")]
        public int IdTypeRoom { get; set; }
        [DisplayName("Quantity Room")]
        public int Qroom { get; set; }
        public RoomType RoomType { get; set; }
        public string Note { get; set; }
        public bool Status { get; set; }
        public DateTime Date_Created { get; set; }
        public DateTime Date_Updated { get; set; }
    }
}