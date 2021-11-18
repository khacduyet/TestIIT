namespace LakeInn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v3 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.User", "Remember");
        }
        
        public override void Down()
        {
            AddColumn("dbo.User", "Remember", c => c.Boolean(nullable: false));
        }
    }
}
