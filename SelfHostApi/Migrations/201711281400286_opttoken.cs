namespace SelfHostApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class opttoken : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "AccessToken", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "AccessToken", c => c.String(nullable: false));
        }
    }
}
