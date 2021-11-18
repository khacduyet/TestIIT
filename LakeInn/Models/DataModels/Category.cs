using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LakeInn.Models.DataModels
{
    [Table("Category")]
    public class Category
    {
        [DisplayName("Id"), Key]
        public int Id { get; set; }
        [DisplayName("Category Name"), Required(ErrorMessage = "This field not null!")]
        public string CateName { get; set; }
        public bool Status { get; set; }
        public DateTime Date_Created { get; set; }
        public DateTime Date_Updated { get; set; }
        public Article Article { get; set; }
    }
}