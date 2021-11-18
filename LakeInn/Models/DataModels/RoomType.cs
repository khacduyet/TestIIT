using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LakeInn.Models.DataModels
{
    [Table("RoomType")]
    public class RoomType
    {
        [DisplayName("Id"), Key]
        public int Id { get; set; }
        [DisplayName("Type Name"), Required(ErrorMessage = "This field not null!")]
        public string TypeName { get; set; }
        [DisplayName("Description"), Required(ErrorMessage = "This field not null!")]
        public string Description { get; set; }
        public bool Status { get; set; }
        public DateTime Date_Created { get; set; }
        public DateTime Date_Updated { get; set; }
        public ICollection<Room> Room { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}