namespace SelfHostApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class client : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Secret = c.String(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        ApplicationType = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                        RefreshTokenLifeTime = c.Int(nullable: false),
                        AllowedOrigin = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.RefreshTokens", "ClientId", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.RefreshTokens", "RefreshTokenLifeTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RefreshTokens", "RefreshTokenLifeTime", c => c.Int(nullable: false));
            DropColumn("dbo.RefreshTokens", "ClientId");
            DropTable("dbo.Clients");
        }
    }
}
