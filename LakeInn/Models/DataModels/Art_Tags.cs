using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LakeInn.Models.DataModels
{
    [Table("ArtTags")]
    public class Art_Tags
    {
        [Key]
        public int Id { get; set; }
        public int Art_Id { get; set; }
        public string ListTag { get; set; }

        [ForeignKey("Art_Id")]
        public Article Article { get; set; }

        [NotMapped]
        public string[] selectedIdArray { get; set; }
    }
}