using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LakeInn.Models.DataModels
{
    [Table("Team")]
    public class Team
    {
        [DisplayName("Id"), Key]
        public int Id { get; set; }
        [DisplayName("Full Name"), Required(ErrorMessage = "This field not null!")]
        public string FullName { get; set; }
        [DisplayName("Description"), Required(ErrorMessage = "This field not null!")]
        public string Description { get; set; }
        [DisplayName("Position"), Required(ErrorMessage = "This field not null!")]
        public string Position { get; set; }
        [DisplayName("Avatar")]
        public string Avatar { get; set; }
        public bool Status { get; set; }
        public DateTime Date_Created { get; set; }
        public DateTime Date_Updated { get; set; }
    }
}