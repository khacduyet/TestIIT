using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LakeInn.Models.DataModels
{
    [Table("Tags")]
    public class Tags
    {
        [DisplayName("Id"), Key]
        public int Id { get; set; }
        [DisplayName("TagName"), Required(ErrorMessage = "This field not null!")]
        public string TagName { get; set; }
        public string Slug { get; set; }
        public bool Status { get; set; }
        public DateTime Date_Created { get; set; }
        public DateTime Date_Updated { get; set; }
    }
}