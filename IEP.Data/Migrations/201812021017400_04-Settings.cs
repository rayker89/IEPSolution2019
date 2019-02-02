namespace IEP.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _04Settings : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicationSettings",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        AuctionItems = c.Int(nullable: false),
                        AuctionDuration = c.Int(nullable: false),
                        SilverPackageTokens = c.Int(nullable: false),
                        GoldPackageTokens = c.Int(nullable: false),
                        PlatinumPackageTokens = c.Int(nullable: false),
                        TokenValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CurrencyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Currencies", t => t.CurrencyId, cascadeDelete: true)
                .Index(t => t.CurrencyId);
            
            CreateTable(
                "dbo.Currencies",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApplicationSettings", "CurrencyId", "dbo.Currencies");
            DropIndex("dbo.ApplicationSettings", new[] { "CurrencyId" });
            DropTable("dbo.Currencies");
            DropTable("dbo.ApplicationSettings");
        }
    }
}
