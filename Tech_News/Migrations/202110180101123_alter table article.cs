namespace Tech_News.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class altertablearticle : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Articles", new[] { "author_id1" });
            DropIndex("dbo.Articles", new[] { "category_id1" });
            DropIndex("dbo.Articles", new[] { "view_id1" });
            DropColumn("dbo.Articles", "author_id");
            DropColumn("dbo.Articles", "category_id");
            DropColumn("dbo.Articles", "view_id");
            RenameColumn(table: "dbo.Articles", name: "author_id1", newName: "author_id");
            RenameColumn(table: "dbo.Articles", name: "category_id1", newName: "category_id");
            RenameColumn(table: "dbo.Articles", name: "view_id1", newName: "view_id");
            AlterColumn("dbo.Articles", "author_id", c => c.Long());
            AlterColumn("dbo.Articles", "category_id", c => c.Int());
            AlterColumn("dbo.Articles", "view_id", c => c.Long());
            CreateIndex("dbo.Articles", "author_id");
            CreateIndex("dbo.Articles", "category_id");
            CreateIndex("dbo.Articles", "view_id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Articles", new[] { "view_id" });
            DropIndex("dbo.Articles", new[] { "category_id" });
            DropIndex("dbo.Articles", new[] { "author_id" });
            AlterColumn("dbo.Articles", "view_id", c => c.Long(nullable: false));
            AlterColumn("dbo.Articles", "category_id", c => c.Int(nullable: false));
            AlterColumn("dbo.Articles", "author_id", c => c.Long(nullable: false));
            RenameColumn(table: "dbo.Articles", name: "view_id", newName: "view_id1");
            RenameColumn(table: "dbo.Articles", name: "category_id", newName: "category_id1");
            RenameColumn(table: "dbo.Articles", name: "author_id", newName: "author_id1");
            AddColumn("dbo.Articles", "view_id", c => c.Long(nullable: false));
            AddColumn("dbo.Articles", "category_id", c => c.Int(nullable: false));
            AddColumn("dbo.Articles", "author_id", c => c.Long(nullable: false));
            CreateIndex("dbo.Articles", "view_id1");
            CreateIndex("dbo.Articles", "category_id1");
            CreateIndex("dbo.Articles", "author_id1");
        }
    }
}
