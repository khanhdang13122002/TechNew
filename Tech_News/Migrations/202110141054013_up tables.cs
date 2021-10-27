namespace Tech_News.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class uptables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        article_title = c.String(nullable: false),
                        article_thumbnail = c.String(nullable: false),
                        article_content = c.String(nullable: false),
                        author_id = c.Long(nullable: false),
                        category_id = c.Long(nullable: false),
                        view_id = c.Long(nullable: false),
                        author_id1 = c.Long(),
                        category_id1 = c.Int(),
                        view_id1 = c.Long(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Authors", t => t.author_id1)
                .ForeignKey("dbo.Categories", t => t.category_id1)
                .ForeignKey("dbo.Views", t => t.view_id1)
                .Index(t => t.author_id1)
                .Index(t => t.category_id1)
                .Index(t => t.view_id1);
            
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        author_name = c.String(nullable: false),
                        author_facebook = c.String(),
                        author_instagram = c.String(),
                        author_twitter = c.String(),
                        user_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.AspNetUsers", t => t.user_Id)
                .Index(t => t.user_Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        category_name = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        article_id = c.Long(nullable: false),
                        comment_content = c.String(nullable: false),
                        articles_id = c.Long(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Articles", t => t.articles_id)
                .Index(t => t.articles_id);
            
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
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Articles", t => t.articles_id)
                .ForeignKey("dbo.Tags", t => t.tags_id)
                .Index(t => t.articles_id)
                .Index(t => t.tags_id);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        tag_name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Views",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        total_view = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            AddColumn("dbo.AspNetUsers", "avatar", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Articles", "view_id1", "dbo.Views");
            DropForeignKey("dbo.Tags_Articles", "tags_id", "dbo.Tags");
            DropForeignKey("dbo.Tags_Articles", "articles_id", "dbo.Articles");
            DropForeignKey("dbo.Comments", "articles_id", "dbo.Articles");
            DropForeignKey("dbo.Articles", "category_id1", "dbo.Categories");
            DropForeignKey("dbo.Authors", "user_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Articles", "author_id1", "dbo.Authors");
            DropIndex("dbo.Tags_Articles", new[] { "tags_id" });
            DropIndex("dbo.Tags_Articles", new[] { "articles_id" });
            DropIndex("dbo.Comments", new[] { "articles_id" });
            DropIndex("dbo.Authors", new[] { "user_Id" });
            DropIndex("dbo.Articles", new[] { "view_id1" });
            DropIndex("dbo.Articles", new[] { "category_id1" });
            DropIndex("dbo.Articles", new[] { "author_id1" });
            DropColumn("dbo.AspNetUsers", "avatar");
            DropTable("dbo.Views");
            DropTable("dbo.Tags");
            DropTable("dbo.Tags_Articles");
            DropTable("dbo.Comments");
            DropTable("dbo.Categories");
            DropTable("dbo.Authors");
            DropTable("dbo.Articles");
        }
    }
}
