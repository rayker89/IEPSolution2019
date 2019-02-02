namespace IEP.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AuctionCurrency : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Auctions", "TokenValue", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Auctions", "Currency", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Auctions", "Currency");
            DropColumn("dbo.Auctions", "TokenValue");
        }
    }
}
