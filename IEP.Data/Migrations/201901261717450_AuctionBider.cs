namespace IEP.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AuctionBider : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Auctions", "AuctionBiderId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Auctions", "AuctionBiderId");
        }
    }
}
