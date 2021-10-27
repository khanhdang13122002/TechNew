namespace Tech_News.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class altertablecommentaddforeignkeyarticle_id : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comments", "articles_id", "dbo.Articles");
            DropIndex("dbo.Comments", new[] { "articles_id" });
            DropColumn("dbo.Comments", "article_id");
            RenameColumn(table: "dbo.Comments", name: "articles_id", newName: "article_id");
            AlterColumn("dbo.Comments", "article_id", c => c.Long(nullable: false));
            CreateIndex("dbo.Comments", "article_id");
            AddForeignKey("dbo.Comments", "article_id", "dbo.Articles", "id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "article_id", "dbo.Articles");
            DropIndex("dbo.Comments", new[] { "article_id" });
            AlterColumn("dbo.Comments", "article_id", c => c.Long());
            RenameColumn(table: "dbo.Comments", name: "article_id", newName: "articles_id");
            AddColumn("dbo.Comments", "article_id", c => c.Long(nullable: false));
            CreateIndex("dbo.Comments", "articles_id");
            AddForeignKey("dbo.Comments", "articles_id", "dbo.Articles", "id");
        }
    }
}
