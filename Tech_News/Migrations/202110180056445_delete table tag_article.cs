namespace Tech_News.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deletetabletag_article : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tags_Articles", "articles_id", "dbo.Articles");
            DropForeignKey("dbo.Tags_Articles", "tags_id", "dbo.Tags");
            DropIndex("dbo.Tags_Articles", new[] { "articles_id" });
            DropIndex("dbo.Tags_Articles", new[] { "tags_id" });
            AddColumn("dbo.Articles", "tag_id", c => c.Long());
            CreateIndex("dbo.Articles", "tag_id");
            AddForeignKey("dbo.Articles", "tag_id", "dbo.Tags", "id");
            DropTable("dbo.Tags_Articles");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Tags_Articles",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        article_id = c.Long(nullable: false),
                        tag_id = c.Long(nullable: false),
                        articles_id = c.Long(),
                        tags_id = c.Long(),
                    })
                .PrimaryKey(t => t.id);
            
            DropForeignKey("dbo.Articles", "tag_id", "dbo.Tags");
            DropIndex("dbo.Articles", new[] { "tag_id" });
            DropColumn("dbo.Articles", "tag_id");
            CreateIndex("dbo.Tags_Articles", "tags_id");
            CreateIndex("dbo.Tags_Articles", "articles_id");
            AddForeignKey("dbo.Tags_Articles", "tags_id", "dbo.Tags", "id");
            AddForeignKey("dbo.Tags_Articles", "articles_id", "dbo.Articles", "id");
        }
    }
}
