namespace Tech_News.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class altertablearticleagain : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Articles", "author_id", "dbo.Authors");
            DropForeignKey("dbo.Articles", "category_id", "dbo.Categories");
            DropForeignKey("dbo.Articles", "view_id", "dbo.Views");
            DropIndex("dbo.Articles", new[] { "author_id" });
            DropIndex("dbo.Articles", new[] { "category_id" });
            DropIndex("dbo.Articles", new[] { "view_id" });
            AlterColumn("dbo.Articles", "author_id", c => c.Long(nullable: false));
            AlterColumn("dbo.Articles", "category_id", c => c.Int(nullable: false));
            AlterColumn("dbo.Articles", "view_id", c => c.Long(nullable: false));
            CreateIndex("dbo.Articles", "author_id");
            CreateIndex("dbo.Articles", "category_id");
            CreateIndex("dbo.Articles", "view_id");
            AddForeignKey("dbo.Articles", "author_id", "dbo.Authors", "id", cascadeDelete: true);
            AddForeignKey("dbo.Articles", "category_id", "dbo.Categories", "id", cascadeDelete: true);
            AddForeignKey("dbo.Articles", "view_id", "dbo.Views", "id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Articles", "view_id", "dbo.Views");
            DropForeignKey("dbo.Articles", "category_id", "dbo.Categories");
            DropForeignKey("dbo.Articles", "author_id", "dbo.Authors");
            DropIndex("dbo.Articles", new[] { "view_id" });
            DropIndex("dbo.Articles", new[] { "category_id" });
            DropIndex("dbo.Articles", new[] { "author_id" });
            AlterColumn("dbo.Articles", "view_id", c => c.Long());
            AlterColumn("dbo.Articles", "category_id", c => c.Int());
            AlterColumn("dbo.Articles", "author_id", c => c.Long());
            CreateIndex("dbo.Articles", "view_id");
            CreateIndex("dbo.Articles", "category_id");
            CreateIndex("dbo.Articles", "author_id");
            AddForeignKey("dbo.Articles", "view_id", "dbo.Views", "id");
            AddForeignKey("dbo.Articles", "category_id", "dbo.Categories", "id");
            AddForeignKey("dbo.Articles", "author_id", "dbo.Authors", "id");
        }
    }
}
