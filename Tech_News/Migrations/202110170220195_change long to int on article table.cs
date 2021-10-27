namespace Tech_News.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changelongtointonarticletable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Articles", "category_id", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Articles", "category_id", c => c.Long(nullable: false));
        }
    }
}
