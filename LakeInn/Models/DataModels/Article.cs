using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LakeInn.Models.DataModels
{
    [Table("Article")]
    public class Article
    {
        [DisplayName("Id"), Key]
        public int Id { get; set; }
        [DisplayName("Title"), Required(ErrorMessage = "This field not null!")]
        public string Title { get; set; }
        [DisplayName("Description"), Required(ErrorMessage = "This field not null!")]
        public string Description { get; set; }
        [DisplayName("Image")]
        public string Image { get; set; }
        public bool Status { get; set; }
        [ForeignKey("Categories")]
        public int CateId { get; set; }
        public ICollection<Category> Categories { get; set; }
        public ICollection<Art_Tags> Art_Tags { get; set; }
        public DateTime Date_Created { get; set; }
        public DateTime Date_Updated { get; set; }

        public Comment_Article Comment_Article { get; set; }
    }
}