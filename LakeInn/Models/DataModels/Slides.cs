using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LakeInn.Models.DataModels
{
    [Table("Slides")]
    public class Slides
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "This field not null!")]
        public string Title { get; set; }
        [Required(ErrorMessage = "This field not null!")]
        public string Content { get; set; }
        public string Image { get; set; }
        public bool Status { get; set; }
    }
}