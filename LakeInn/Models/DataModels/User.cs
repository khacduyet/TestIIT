using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LakeInn.Models.DataModels
{
    [Table("User")]
    public class User
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Full Name")]
        public string FullName { get; set; }
        [DisplayName("Email"), Required(ErrorMessage = "This field not null!"), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DisplayName("Password"), Required(ErrorMessage = "This field not null!")]
        public string Password { get; set; }
        [DisplayName("Phone")]
        public string Phone { get; set; }
        public string Avatar { get; set; }
        public bool Status { get; set; }
        public bool Forgot { get; set; }
        public DateTime Date_Created { get; set; }
        public DateTime Date_Updated { get; set; }
    }
}