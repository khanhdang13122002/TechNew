namespace Tech_News.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addcreate_atandupdate_atfile : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "create_at", c => c.DateTime(nullable: false));
            AddColumn("dbo.Articles", "update_at", c => c.DateTime(nullable: false));
            AddColumn("dbo.Authors", "create_at", c => c.DateTime(nullable: false));
            AddColumn("dbo.Authors", "update_at", c => c.DateTime(nullable: false));
            AddColumn("dbo.Categories", "create_at", c => c.DateTime(nullable: false));
            AddColumn("dbo.Categories", "update_at", c => c.DateTime(nullable: false));
            AddColumn("dbo.Comments", "create_at", c => c.DateTime(nullable: false));
            AddColumn("dbo.Comments", "update_at", c => c.DateTime(nullable: false));
            AddColumn("dbo.Tags", "create_at", c => c.DateTime(nullable: false));
            AddColumn("dbo.Tags", "update_at", c => c.DateTime(nullable: false));
            AddColumn("dbo.Views", "create_at", c => c.DateTime(nullable: false));
            AddColumn("dbo.Views", "update_at", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Views", "update_at");
            DropColumn("dbo.Views", "create_at");
            DropColumn("dbo.Tags", "update_at");
            DropColumn("dbo.Tags", "create_at");
            DropColumn("dbo.Comments", "update_at");
            DropColumn("dbo.Comments", "create_at");
            DropColumn("dbo.Categories", "update_at");
            DropColumn("dbo.Categories", "create_at");
            DropColumn("dbo.Authors", "update_at");
            DropColumn("dbo.Authors", "create_at");
            DropColumn("dbo.Articles", "update_at");
            DropColumn("dbo.Articles", "create_at");
        }
    }
}
