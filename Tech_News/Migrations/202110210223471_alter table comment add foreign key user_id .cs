namespace Tech_News.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class altertablecommentaddforeignkeyuser_id : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "user_id", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Comments", "user_id");
            AddForeignKey("dbo.Comments", "user_id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "user_id", "dbo.AspNetUsers");
            DropIndex("dbo.Comments", new[] { "user_id" });
            DropColumn("dbo.Comments", "user_id");
        }
    }
}
