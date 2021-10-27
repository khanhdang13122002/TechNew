namespace Tech_News.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class altertablecommentaddparentcolumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "parent_id", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comments", "parent_id");
        }
    }
}
