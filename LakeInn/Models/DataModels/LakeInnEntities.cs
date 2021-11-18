using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LakeInn.Models.DataModels
{
    public class LakeInnEntities : DbContext
    {
        public LakeInnEntities() : base("name=LakeInnConnection")
        {

        }
        public DbSet<Article> Articles { get; set; }
        public DbSet<BedType> BedTypes { get; set; }
        public DbSet<Comment_Article> Comment_Articles { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<Tags> Tags { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Art_Tags> Art_Tags { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Book> Book { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Slides> Slides { get; set; }
        public DbSet<User> Users { get; set; }
    }
}