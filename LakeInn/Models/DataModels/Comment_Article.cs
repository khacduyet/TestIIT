using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LakeInn.Models.DataModels
{
    [Table("Comment_Article")]
    public class Comment_Article
    {
        [DisplayName("Id"), Key]
        public int Id { get; set; }
        [DisplayName("Name"), Required(ErrorMessage = "This field not null!")]
        public string Name { get; set; }
        [DisplayName("Email"), Required(ErrorMessage = "This field not null!"), DataType(DataType.EmailAddress, ErrorMessage = "Please fill email this field!")]
        public string Email { get; set; }
        [DisplayName("Comment"), Required(ErrorMessage = "This field not null!")]
        public string Comment { get; set; }
        public bool Status { get; set; }
        public DateTime Date_Created { get; set; }
        public DateTime Date_Updated { get; set; }
        [ForeignKey("Articles")]
        public int Article_Id { get; set; }
        public ICollection<Article> Articles { get; set; }
    }
}